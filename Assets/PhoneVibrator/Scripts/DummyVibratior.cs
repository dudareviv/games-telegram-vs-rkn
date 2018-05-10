using UnityEngine;

namespace PhoneVibrator
{
    public class DummyVibratior : Vibrator
    {
        public override void Vibrate(long milliseconds)
        {
            Debug.Log("DummyVibratior.Vibrate");
            Handheld.Vibrate();
        }

//        public override void Vibrate(long[] pattern, int repeat)
//        {
//            Debug.Log("DummyVibratior.Vibrate");
//            Handheld.Vibrate();
//        }

        public override bool HasVibrator()
        {
            Debug.Log("DummyVibratior.HasVibrate");
            return true;
        }

        public override void Cancel()
        {
            Debug.Log("DummyVibratior.Cancel");
        }
    }
}