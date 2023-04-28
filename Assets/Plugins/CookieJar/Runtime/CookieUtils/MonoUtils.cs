using UnityEngine;

namespace CookieUtils
{
	public class MonoUtils : MonoBehaviour
	{
        /// <summary>
        ///     Initialize MonoUtils settings.
        ///     - whether to pause all timers
        ///     - whether to destroy object on loading of new scene (false will destroy)
        /// </summary>
        /// <param name="pauseAllTimers">Pause all timers</param>
        /// <param name="dontDestroyOnLoad">Don't destroy on load</param>
        public MonoUtils(bool pauseAllTimers, bool dontDestroyOnLoad)
		{
			TimersPaused = pauseAllTimers;
			if (dontDestroyOnLoad)
				DontDestroyOnLoad(this);
		}

        /// <summary>
        ///     Initialize MonoUtils settings.
        ///     - whether to destroy object on loading of new scene (false will destroy)
        /// </summary>
        /// <param name="dontDestroyOnLoad">Don't destroy on load</param>
        public MonoUtils(bool dontDestroyOnLoad)
		{
			TimersPaused = false;
			if (dontDestroyOnLoad)
				DontDestroyOnLoad(this);
		}

        /// <summary>
        ///     Don't destroy on load is set to true, when creating through this method
        /// </summary>
        public MonoUtils()
		{
			TimersPaused = false;
		}

		private bool TimersPaused { get; set; }

		private void FixedUpdate()
		{
			UpdateTimers();
			DontDestroyOnLoad(this);
		}

        /// <summary>
        ///     Updates all timers that are not paused.
        ///     is a private function not interactable
        /// </summary>
        private void UpdateTimers()
		{
			if (TimersPaused) return;
			for (var i = Utils.timerDatas.Count - 1; i >= 0; i--)
			{
				var timer = Utils.timerDatas[i];
				if (timer.paused.value) continue;
				if (timer.timeData.GetCurrentValue() <= 0)
				{
					timer.timerDone.value = true;
					continue;
				}

				timer.timeData.AdjustCurrentValue(-Time.fixedDeltaTime);
			}
		}

        /// <summary>
        ///     Will pause the updateTimers function, pausing all timers.
        ///     Does not pause individual timers
        /// </summary>
        public void PauseAllTimers()
		{
			TimersPaused = false;
		}

        /// <summary>
        ///     For use with the utils class, no point in calling this yourself
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="time">Time to destroy object</param>
        public void DestroyOb(Object go, float time)
		{
			Destroy(go, time);
		}

        /// <summary>
        ///     For use with the utils class, no point in calling this yourself
        /// </summary>
        /// <param name="go">GameObject</param>
        public void DestroyOb(Object go)
		{
			Destroy(go);
		}
	}
}