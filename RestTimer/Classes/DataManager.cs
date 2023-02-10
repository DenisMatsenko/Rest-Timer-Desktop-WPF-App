using System;
using System.IO;
using System.Windows.Media;

namespace RestTimer
{
    class DataManager
    {
        public static int WorkSessionTimeSec { get; set; }
        public static Brush AppColorTheme { get; set; }

        static DataManager()
        {
            DataUpdate();
        }

        /// <summary>
        /// Get user settings from "Settings.txt".
        /// </summary>
        static public void DataUpdate()
        {
            BrushConverter bc = new BrushConverter();
            WorkSessionTimeSec = Int32.Parse(File.ReadAllLines("Settings.txt")[1].Split(':')[1]);
            AppColorTheme = (Brush)bc.ConvertFrom(File.ReadAllLines("Settings.txt")[2].Split(':')[1]);
        }

        /// <summary>
        /// Return current application theme color in Brush type.
        /// </summary>
        /// <returns>Brush current app thene color</returns>
        public Brush GetAppThemeColor()
        {
            return AppColorTheme;
        }
        /// <summary>
        /// Return current work session time in int type.
        /// </summary>
        /// <returns>int Currnet work session time</returns>
        public int GetWorkSessionTimeSec()
        {
            return WorkSessionTimeSec;
        }
    }
}
