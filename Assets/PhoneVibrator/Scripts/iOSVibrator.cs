using UnityEngine;

namespace PhoneVibrator
{
    public class iOSVibrator : Vibrator
    {
        public override void Vibrate(long milliseconds)
        {
            Debug.Log("iOSVibrator.Vibrate");
            throw new System.NotImplementedException();
        }

//        public override void Vibrate(long[] pattern, int repeat)
//        {
//            Debug.Log("iOSVibrator.Vibrate");
//            throw new System.NotImplementedException();
//        }

        public override bool HasVibrator()
        {
            Debug.Log("iOSVibrator.HasVibrator");
            throw new System.NotImplementedException();
        }

        public override void Cancel()
        {
            Debug.Log("iOSVibrator.Cancel");
            throw new System.NotImplementedException();
        }
    }
}