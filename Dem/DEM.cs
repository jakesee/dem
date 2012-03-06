// Author: Jake See
// Implementation of USGS DEM file format
// http://en.wikipedia.org/wiki/USGS_DEM
// ----------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace USGS.DEM
{
    public enum GROUND_REF_SYSTEM { Geographic = 0, UTM, State_Plane };
    public enum GROUND_UNIT { Radian = 0, feet, meters, arc_seconds };
    public enum ELEVATION_UNIT { feet = 1, meters };

    public class DemDocument
    {
        private ARecord _mARecord;
        private BRecord _mBRecord;

        public DemDocument()
        {
            _mARecord = null;
            _mBRecord = null;
        }

        /// <summary>
        /// Get the A-Record
        /// </summary>
        public ARecord ARecord { get { return _mARecord; } }
        /// <summary>
        /// Get the B-Record
        /// </summary>
        public BRecord BRecord { get { return _mBRecord; } }

        /// <summary>
        /// Read a *.dem file
        /// </summary>
        /// <param name="filename"></param>
        public void Read(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            char[] buffer = new char[1024]; // size of A Record
            reader.Read(buffer, 0, 1024);

            _mARecord = new ARecord();
            _mARecord.name = ParseString(buffer, 0, 40).ToCharArray();
            _mARecord.SE_geographic_corner_S = ParseString(buffer, 109, 13).ToCharArray();
            _mARecord.SE_geographic_corner_E = ParseString(buffer, 122, 13).ToCharArray();
            _mARecord.process_code = buffer[135];
            _mARecord.origin_code = ParseString(buffer, 140, 4).ToCharArray();
            _mARecord.dem_level_code = ParseInt(buffer, 144);
            _mARecord.elevation_pattern = ParseInt(buffer, 150);
            _mARecord.ground_ref_system = ParseInt(buffer, 156);
            _mARecord.ground_ref_zone = ParseInt(buffer, 162);
            for (int i = 0; i < 15; i++)
                _mARecord.projection[0] = ParseDouble(buffer, 168 + i * 24);
            _mARecord.ground_unit = ParseInt(buffer, 528);
            _mARecord.elevation_unit = ParseInt(buffer, 534);
            _mARecord.side_count = ParseInt(buffer, 540);
            _mARecord.sw_coord[0] = (float)ParseDouble(buffer, 546); // UTM grid (measured in meters)
            _mARecord.sw_coord[1] = (float)ParseDouble(buffer, 570);
            _mARecord.nw_coord[0] = (float)ParseDouble(buffer, 594);
            _mARecord.nw_coord[1] = (float)ParseDouble(buffer, 618);
            _mARecord.ne_coord[0] = (float)ParseDouble(buffer, 642);
            _mARecord.ne_coord[1] = (float)ParseDouble(buffer, 666);
            _mARecord.se_coord[0] = (float)ParseDouble(buffer, 690);
            _mARecord.se_coord[1] = (float)ParseDouble(buffer, 714);
            _mARecord.elevation_min = ParseDouble(buffer, 738);
            _mARecord.elevation_max = ParseDouble(buffer, 762);
            _mARecord.ccw_angle = ParseDouble(buffer, 786);
            _mARecord.elevation_accuracy = ParseInt(buffer, 810);
            _mARecord.xyz_resolution[0] = ParseFloat(buffer, 816);
            _mARecord.xyz_resolution[1] = ParseFloat(buffer, 828);
            _mARecord.xyz_resolution[2] = ParseFloat(buffer, 840);
            _mARecord.eastings_cols = ParseInt(buffer, 858);
            _mARecord.northings_rows = ParseInt(buffer, 858); // WARNING. SHOULD NOT USE THIS VALUE. BUT WE ASSUME IS THE SAME WITH eastings SINCE IT IS ALWAYS THE CASE.
            _mARecord.suspect_void = ParseInt(buffer, 886, 2);
            _mARecord.percent_void = ParseInt(buffer, 896);

            // read the rest of the DEM
            StreamTokenizer tokenizer = new StreamTokenizer(reader, new char[] { ' ' });

            _mBRecord = null;
            for (int col = 0; col < _mARecord.eastings_cols; col++)
            {
                tokenizer.Next(); // row id
                tokenizer.Next(); // col id
                _mARecord.northings_rows = ToInt(tokenizer.Next());

                if (_mBRecord == null)
                    _mBRecord = new BRecord(_mARecord.eastings_cols, _mARecord.northings_rows);

                tokenizer.Next(); // skip next six fields
                tokenizer.Next();
                tokenizer.Next();
                tokenizer.Next();
                tokenizer.Next();
                tokenizer.Next();

                // for (int row = record.northings_rows - 1; row >= 0; row--)
                for (int row = 0; row < _mARecord.northings_rows; row++)
                {
                    _mBRecord.elevations[col, row] = ToShort(tokenizer.Next());
                }
            }

            reader.Close();
        }

        #region " Helper Functions "
        string ParseString(char[] buffer, int start, int count)
        {
            String s = new string(buffer, start, count);
            s = s.Trim();
            return s;
        }
        int ParseInt(char[] buffer, int start)
        {
            return ParseInt(buffer, start, 6);
        }
        int ParseInt(char[] buffer, int start, int count)
        {
            string s = new string(buffer, start, count).Replace('D', 'E');
            int i = 0;
            if(!int.TryParse(s.Trim(), out i)) i = -1;
            return i;
        }
        int ToInt(string s)
        {
            int i;
            int.TryParse(s, out i);
            return i;
        }
        short ToShort(string s)
        {
            short i;
            short.TryParse(s, out i);
            return i;
        }
        float ParseFloat(char[] buffer, int start)
        {
            return ParseFloat(buffer, start, 12);
        }
        float ParseFloat(char[] buffer, int start, int count)
        {
            String s = new string(buffer, start, count).Replace('D', 'E');
            float f = 0;
            if (!float.TryParse(s.Trim(), out f)) f = -1;
            return f;
        }
        double ParseDouble(char[] buffer, int start)
        {
            return ParseDouble(buffer, start, 24);
        }
        double ParseDouble(char[] buffer, int start, int count)
        {
            String s = new string(buffer, start, count).Replace('D', 'E');
            double d = 0;
            if (!double.TryParse(s.Trim(), out d)) d = -1;
            return d;

        }
        #endregion

        class StreamTokenizer
        {
            string[] _mTokens;
            int current;

            public StreamTokenizer(TextReader reader, char[] delimiter)
            {
                string s = reader.ReadToEnd().Replace("-32767", " -100");
                _mTokens = s.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                current = 0;
            }

            public string Next()
            {
                if (current < _mTokens.Length)
                    return _mTokens[current++];

                throw new Exception("No More Data!");
            }
        }
    }

    public class ARecord
    {
        /// <summary>
        /// Description Name of the represented area
        /// </summary>
        public char[] name = new char[40];
        /// <summary>
        /// Southing of the Southeast geographic corner
        /// </summary>
        public char[] SE_geographic_corner_S = new char[13];
        /// <summary>
        /// Easting of the southeast geographic corner
        /// </summary>
        public char[] SE_geographic_corner_E = new char[13];
        public char process_code;
        public char[] origin_code = new char[4];
        public int dem_level_code;
        public int elevation_pattern;
        public int ground_ref_system;
        public int ground_ref_zone;
        public double[] projection = new double[15];
        public int ground_unit;
        public int elevation_unit;
        public int side_count;
        // pairs of easting-northings
        public float[] sw_coord = new float[2];
        public float[] nw_coord = new float[2];
        public float[] ne_coord = new float[2];
        public float[] se_coord = new float[2];
        public double ccw_angle;
        public double elevation_min;
        public double elevation_max;
        public int elevation_accuracy;
        public float[] xyz_resolution = new float[3];
        public int northings_rows;
        public int eastings_cols;
        public int suspect_void;
        public int percent_void;
    }

    public class BRecord
    {
        public short[,] elevations;

        public BRecord(int cols, int rows)
        {
            elevations = new short[cols, rows];
        }
    }
}
