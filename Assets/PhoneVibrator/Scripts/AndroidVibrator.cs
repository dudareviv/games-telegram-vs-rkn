using UnityEngine;

namespace PhoneVibrator
{
    public class AndroidVibrator : Vibrator
    {
        private static readonly AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        private static readonly AndroidJavaObject currentActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        private static readonly AndroidJavaObject vibrator =
            currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

        private static readonly AndroidJavaClass vibrationEffectClass =
            new AndroidJavaClass("android.os.VibrationEffect");

        private static readonly int defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
        private static readonly AndroidJavaClass androidVersion = new AndroidJavaClass("android.os.Build$VERSION");
        private static readonly int apiLevel = androidVersion.GetStatic<int>("SDK_INT");

        public override void Vibrate(long milliseconds)
        {
            Debug.Log("AndroidVibrator.Vibrate");
            
            if (apiLevel >= 26) {
                AndroidJavaObject vibrationEffect;

                if (HasAmplituideControl()) {
                    vibrationEffect = GetVibrationEffect("createOneShot", milliseconds, defaultAmplitude);
                } else {
                    vibrationEffect = GetVibrationEffect("createOneShot", milliseconds);
                }

                vibrator.Call("vibrate", vibrationEffect);
            } else {
                vibrator.Call("vibrate", milliseconds);
            }
        }

        private static AndroidJavaObject GetVibrationEffect(string function, params object[] args)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
        }

        private static bool HasAmplituideControl()
        {
            return vibrator.Call<bool>("hasAmplitudeControl");
        }


        public override bool HasVibrator()
        {
            Debug.Log("AndroidVibrator.HasVibrator");
            return vibrator.Call<bool>("hasVibration");
        }

        public override void Cancel()
        {
            Debug.Log("AndroidVibrator.Cancel");
            vibrator.Call("cancel");
        }
    }
}