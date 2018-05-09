using UnityEngine;

namespace PhoneVibrator
{
    public class DummyVibratior : Vibrator
    {
        public override void Vibrate(float milliseconds)
        {
            Debug.Log("DummyVibratior.Vibrate");
        }

        public override void Vibrate(long[] pattern, int repeat)
        {
            Debug.Log("DummyVibratior.Vibrate");
        }

        public override bool HasVibrator()
        {
            Debug.Log("DummyVibratior.HasVibrate");
            return false;
        }

        public override void Cancel()
        {
            Debug.Log("DummyVibratior.Cancel");
        }
    }
}