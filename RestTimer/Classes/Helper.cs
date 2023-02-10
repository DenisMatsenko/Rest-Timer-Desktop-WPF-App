using System.Media;
using System.Windows.Media;

namespace RestTimer
{
    class Helper
    {
        BrushConverter brushConverter = new BrushConverter();

        /// <summary>
        /// Return current app theme color in Brush type.  
        /// </summary>
        /// <returns>Brush</returns>
        public Brush GetAppThemeColor()
        {
            return (Brush) brushConverter.ConvertFrom(DataManager.AppColorTheme);
        }

        /// <summary>
        /// Play work session finish sound.
        /// </summary>
        public static void PlayWorkSessionFinishSound()
        {
            new SoundPlayer("Finish.wav").Play();
        }
    }
}
