using System.Runtime.InteropServices;

namespace Tank_Dome.Model
{
    class Playsound
    {
        private const int SND_SYNC = 0x0;
        private const int SND_ASYNC = 0x1;          //异步
        private const int SND_NODEFAULT = 0x2;      //
        private const int SND_LOOP = 0x8;
        private const int SND_NOSTOP = 0x10;

        public static void Play(string file)
        {
            int flags = SND_ASYNC | SND_NODEFAULT;
            sndPlaySound(file, flags);
        }
        [DllImport("winmm.dll")]
        private extern static int sndPlaySound(string file, int uFlags);

        /// <summary>   
        /// 播放   
        /// </summary>   
        public static void playSound(string FilePath)
        {
            mciSendString("close all", "", 0, 0);
            mciSendString("open " + FilePath + " alias media", "", 0, 0);
            mciSendString("play media", "", 0, 0);
        }

        /// <summary>   
        /// 停止   
        /// </summary>   
        public static void Stop()
        {
            mciSendString("close media", "", 0, 0);
        }

        /// <summary>
        /// API函数
        /// </summary>
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            int uReturnLength,
            int hwndCallback
        );
    }
}
