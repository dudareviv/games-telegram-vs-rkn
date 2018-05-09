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

        private Vibrator _adapter;

        private VibratorController()
        {

#if UNITY_EDITOR
            _adapter = new DummyVibratior();
#else
    #if UNITY_ANDROID
            _adapter = new AndroidVibrator();
    #elif UNITY_IOS
            _adapter = new iOSVibrator();
    #else
            _adapter = new DummyVibratior();
    #endif
#endif
        }

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

        public void Vibrate(float duration)
        {
            if (!HasVibrator())
                return;

            _adapter.Vibrate(duration);
        }

        public void Vibrate(long[] patter, int repeat)
        {
            if (!HasVibrator())
                return;

            _adapter.Vibrate(patter, repeat);
        }

        public bool HasVibrator()
        {
            return _adapter.HasVibrator();
        }

        public void Cancel()
        {
            if (HasVibrator())
                return;

            _adapter.Cancel();
        }

        #endregion

    }
}