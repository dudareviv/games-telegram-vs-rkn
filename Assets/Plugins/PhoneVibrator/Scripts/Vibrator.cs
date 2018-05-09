namespace PhoneVibrator
{
    public abstract class Vibrator
    {
        /// <summary>
        /// Vibrates constantly for the specified period of time.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to vibrate</param>
        public abstract void Vibrate(float milliseconds);

        /// <summary>
        /// <p>
        /// Vibrate with a given pattern.
        /// </p>
        /// 
        /// <p>
        /// Pass in an array of ints that are the durations for which to turn on or off the vibrator in milliseconds.
        /// The first value indicates the number of milliseconds to wait before turning the vibrator on.
        /// The next value indicates the number of milliseconds for which to keep the vibrator on before turning it off.
        /// Subsequent values alternate between durations in milliseconds to turn the vibrator off or to turn the vibrator on.
        /// </p>
        /// 
        /// <p>
        /// To cause the pattern to repeat, pass the index into the pattern array at which to start the repeat, or -1 to disable repeating.
        /// </p>
        /// </summary>
        /// <param name="pattern">an array of longs of times for which to turn the vibrator on or off.</param>
        /// <param name="repeat">the index into pattern at which to repeat, or -1 if you don't want to repeat.</param>
        public abstract void Vibrate(long[] pattern, int repeat);

        /// <summary>
        /// Check whether the hardware has a vibration.
        /// </summary>
        /// <returns>Returns true if a vibration is detected on the device.</returns>
        public abstract bool HasVibrator();

        /// <summary>
        /// Stops any running vibrations on the device.
        /// </summary>
        public abstract void Cancel();
    }
}