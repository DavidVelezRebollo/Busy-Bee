using UnityEngine;

namespace GOM.Classes.UI {
	public class Timer {
		private float _timer; // Timer in game time
		private int _initialMinutes; // Initial minutes of the timer
		private int _initialSeconds; // Initial seconds of the timer
		private int _currentMinutes; // Current minutes elapsed
		private int _currentSeconds; // Current seconds elapsed

		public Timer(int totalMinutes, int totalSeconds) {
			_timer = totalMinutes * 60 + totalSeconds;
			_initialMinutes = totalMinutes * 60;
			_initialSeconds = totalSeconds;
			_currentMinutes = totalMinutes * 60;
			_currentSeconds = 0;
		}

        #region Getters & Setters

        public int GetMinuteCount() { return _currentMinutes; }
		
		public int GetSecondCount() { return _currentSeconds; }

		public void SetInitialSeconds(int initialSeconds) { _initialSeconds = initialSeconds; }

        #endregion

        #region Methods

        public void UpdateTimer() {
			_timer -= Time.deltaTime;
			_currentMinutes = Mathf.FloorToInt(_timer / 60f);
			_currentSeconds = Mathf.FloorToInt(_timer % 60f);
		}

		public void RestartTimer() {
			_timer = _initialMinutes * 60 + _initialSeconds;
        }

        #endregion
    }
}
