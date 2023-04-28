using CookieUtils.UtilSubHelpers.DataTypes;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers
{
    /// <summary>
    /// Is a scriptable object holding all the information for the timer.
    /// Should be created through the Utils class, to make sure MonoUtils exists.
    /// </summary>
    public class TimerData : ScriptableObject
    {
        public TimerData()
        {
            timerDone = CreateInstance<BoolData>();
            paused = CreateInstance<BoolData>();
            timeData = CreateInstance<FloatData>();
            Utils.timerDatas.Add(this);
        }

        public FloatData timeData;
        public BoolData paused;
        public BoolData timerDone;

        /// <summary>
        /// initialize timer settings
        ///  - Set timer start time
        ///  - If it starts of paused
        /// </summary>
        /// <param name="maxTime">max time of timer</param>
        /// <param name="paused">whether timer is paused on creation</param>
        public void InitializeTimer(float maxTime, bool paused)
        {
            timeData.SetMaxValue(maxTime);
            timeData.useMaxValue = true;
            this.paused.value = paused;
            timerDone.value = false;
            timeData.ResetValue();
        }
        
        /// <summary>
        /// initialize timer settings
        ///  - Set timer start time
        /// </summary>
        /// <param name="maxTime">max time of timer</param>
        public void InitializeTimer(float maxTime)
        {
            timeData.SetMaxValue(maxTime);
            paused.value = false;
            timerDone.value = false;
            timeData.ResetValue();
        }

        /// <summary>
        /// Reset timer to maxTime and set timerDone = false.
        /// </summary>
        public void ResetTimer()
        {
            timeData.ResetValue();
            timerDone.value = false;
        }

        public void EndTimer()
        {
            timeData.SetCurrentValue(0);
            timerDone.value = true;
        }

        public void AlterValue(float val)
        {
            timeData.SetMaxValue(val);
        }
    
        /// <summary>
        /// Toggle whether the timer is paused or not.
        /// </summary>
        public void TogglePause()
        {
            paused.value = !paused.value;
        }

        /// <summary>
        /// Pause the timer.
        /// </summary>
        public void Pause()
        {
            paused.value = true;
        }
        
        /// <summary>
        /// Unpause the timer.
        /// </summary>
        public void Unpause()
        {
            paused.value = false;
        }

        public bool IsTimingDown(bool takeInPaused = false)
        {
            if (takeInPaused)
                return timeData.GetCurrentValue() > 0;
            return timeData.GetCurrentValue() > 0 && !paused.value;
        }
    }
}
