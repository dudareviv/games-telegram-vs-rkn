using UnityEngine;

namespace PhoneVibrator
{
    /// <summary>
    /// </summary>
    public class VibratorController
    {

        #region Singleton implementation

        // Singleton: https://msdn.microsoft.com/en-us/library/ff650316.aspx

        private static volatile VibratorController instance;
        private static object syncRoot = new Object();

#if UNITY_ANDROID
        private static Vibrator _adapter = new AndroidVibrator();
#elif UNITY_IOS
        private static Vibrator _adapter = new iOSVibrator();
#else
        private static Vibrator _adapter = new DummyVibratior();
#endif

        public static VibratorController Instance {
            get
            {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null) {
                            instance = new VibratorController();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Vibrator controller implementation

        public static void Vibrate(long duration)
        {
            if (!HasVibrator())
                return;

            _adapter.Vibrate(duration);
        }

//        public static void Vibrate(long[] patter, int repeat)
//        {
//            if (!HasVibrator())
//                return;
//
//            _adapter.Vibrate(patter, repeat);
//        }

        public static bool HasVibrator()
        {
            return _adapter.HasVibrator();
        }

        public static void Cancel()
        {
            if (HasVibrator())
                return;

            _adapter.Cancel();
        }

        #endregion

    }
}