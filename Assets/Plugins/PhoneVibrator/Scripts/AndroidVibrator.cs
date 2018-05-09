using UnityEngine;

namespace PhoneVibrator
{
    public class AndroidVibrator : Vibrator
    {
        private const string UNITY_PACKAGE_NAME = "com.unity3d.player.UnityPlayer";
        private const string VIBRATE_PACKAGE_NAME = "unitylibraries.dudareviv.com.vibrator.VibratorWrapper";

        private AndroidJavaObject androidObject;

        public AndroidVibrator()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass(UNITY_PACKAGE_NAME);
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            androidObject = new AndroidJavaObject(VIBRATE_PACKAGE_NAME, currentActivity);
        }

        public override void Vibrate(float milliseconds)
        {
            androidObject.Call("vibrate", milliseconds);
        }

        public override void Vibrate(long[] pattern, int repeat)
        {
            androidObject.Call("vibrate", pattern, repeat);
        }

        public override bool HasVibrator()
        {
            return androidObject.Call<bool>("hasVibration");
        }

        public override void Cancel()
        {
            androidObject.Call("cancel");
        }
    }
}