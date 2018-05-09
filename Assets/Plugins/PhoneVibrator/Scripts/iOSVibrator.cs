using UnityEngine;

namespace PhoneVibrator
{
    public class iOSVibrator : Vibrator
    {
        public override void Vibrate(float milliseconds)
        {
            throw new System.NotImplementedException();
        }

        public override void Vibrate(long[] pattern, int repeat)
        {
            throw new System.NotImplementedException();
        }

        public override bool HasVibrator()
        {
            throw new System.NotImplementedException();
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}