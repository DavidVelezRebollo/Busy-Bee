using UnityEngine;

namespace GOM.Classes.UI {
	public class Timer {
		private float _timer; // Timer in game time
		private int _currentMinutes; // Current minutes elapsed
		private int _currentSeconds;

		public Timer(int totalMinutes) {
			_timer = totalMinutes * 60;
			_currentMinutes = totalMinutes * 60;
			_currentSeconds = 0;
		}

		public void UpdateTimer() {
			_timer -= Time.deltaTime;
			_currentMinutes = Mathf.FloorToInt(_timer / 60f);
			_currentSeconds = Mathf.FloorToInt(_timer % 60f);
		}

		public int GetMinuteCount() { return _currentMinutes; }
		
		public int GetSecondCount() { return _currentSeconds; }
	}
}
