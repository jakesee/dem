# USGS DEM File Reader .NET C# library
This is a .NET C# library to read USGS DEM files

## Getting Started
The project is written with Microsoft Visual Studio 2017, simply download the latest source code from the master branch and build the library with the IDE.

### Prerequisites
No pre-requisites other than the .NET Framework.

### Installing
The project builds into a class library with example applications.

## Sample Application
A sample application is included to demostrate the usage and use the DEM data to draw a color coded height map.

![](https://github.com/jakesee/dem/blob/master/docs/resources/sample_exe.png)

### Basic Usage

    dem = new DemDocument();
    dem.read("022k13_0300_deme.dem");

### Sample DEM files
To get DEM files for testing, please visit https://dds.cr.usgs.gov/pub/data/DEM/250/.
Please also see wikipedia entry for [USGS DEM](https://en.wikipedia.org/wiki/USGS_DEM) for alternative sources if the above does not work.

