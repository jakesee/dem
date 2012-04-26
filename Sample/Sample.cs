using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using USGS.DEM;

namespace Sample
{
    public partial class Sample : Form
    {
        DemDocument _mDem = null;

        public Sample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Demostrate how to get the elevation points and create a bitmap with color coded heights
        /// </summary>
        private void DrawImage()
        {
            if (_mDem != null)
            {
                var bitmap = new Bitmap(_mDem.ARecord.northings_rows, _mDem.ARecord.eastings_cols);
                
                for (int col = 0; col < _mDem.ARecord.eastings_cols; col++)
                {
                    for (int row = 0; row < _mDem.ARecord.northings_rows; row++)
                    {
                        double height = _mDem.BRecord.elevations[col, row] * _mDem.ARecord.xyz_resolution[2];
                        var min = _mDem.ARecord.elevation_min;
                        var max = _mDem.ARecord.elevation_max;
                        if (height >= min)
                        {
                            int ratio = (int)(((height - min) / (max - min)) * 255f);
                            bitmap.SetPixel(row, col, Color.FromArgb(128, ratio, 128));

                            // Or this, as suggested by thanaphan4 for fixing bitmap x/y orientation
                            // bitmap.SetPixel(col, _mDem.ARecord.northings_rows - row - 1, Color.FromArgb(128, 128, ratio));
                        }
                    }
                }

                _mPictureBox.Image = bitmap;
            }
        }

        /// <summary>
        /// Read the file and display header information. For full explanation of the headers,
        /// please refer to the DEM specification included in the repository.
        /// To get DEM files for testing, visit http://www.geobase.ca/geobase/en/data/cded/index.html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (System.IO.File.Exists(dialog.FileName))
                {
                    _mDem = new DemDocument();
                    _mDem.Read(dialog.FileName);
                    txtOutput.Text = string.Empty;
                    txtOutput.Text += "DEM Name: " + new string(_mDem.ARecord.file_name) + Environment.NewLine;
                    txtOutput.Text += "SE Coord: " + new string(_mDem.ARecord.SE_geographic_corner_S) + ", " + new string(_mDem.ARecord.SE_geographic_corner_E) + Environment.NewLine;
                    txtOutput.Text += "DEM Level Code: " + _mDem.ARecord.dem_level_code + Environment.NewLine;
                    txtOutput.Text += "Ground Reference System: " + (GROUND_REF_SYSTEM)_mDem.ARecord.ground_ref_system + Environment.NewLine;
                    txtOutput.Text += "Ground Reference Zone: " + _mDem.ARecord.ground_ref_zone + Environment.NewLine;
                    txtOutput.Text += "Ground Unit: " + (GROUND_UNIT)_mDem.ARecord.ground_unit + Environment.NewLine;
                    txtOutput.Text += "Elevation Unit: " + (ELEVATION_UNIT)_mDem.ARecord.elevation_unit + Environment.NewLine;
                    txtOutput.Text += "Ground Resolution (lat, lng, elev): " + _mDem.ARecord.xyz_resolution[0] + ", " + _mDem.ARecord.xyz_resolution[1] + ", " + _mDem.ARecord.xyz_resolution[2] + Environment.NewLine;
                    txtOutput.Text += "Elavation Array Szie: " + _mDem.ARecord.northings_rows + " x " + _mDem.ARecord.eastings_cols + Environment.NewLine;
                    txtOutput.Text += "Percentage void: " + _mDem.ARecord.percent_void + Environment.NewLine;
                    txtOutput.Text += "SW Coord: " + _mDem.ARecord.sw_coord[0] + ", " + _mDem.ARecord.sw_coord[1] + Environment.NewLine;
                    txtOutput.Text += "NW Coord: " + _mDem.ARecord.nw_coord[0] + ", " + _mDem.ARecord.nw_coord[1] + Environment.NewLine;
                    txtOutput.Text += "NE Coord: " + _mDem.ARecord.ne_coord[0] + ", " + _mDem.ARecord.ne_coord[1] + Environment.NewLine;
                    txtOutput.Text += "SE Coord: " + _mDem.ARecord.se_coord[0] + ", " + _mDem.ARecord.se_coord[1] + Environment.NewLine;

                    DrawImage();
                }
            }
        }
    }
}
