using System.Collections.Generic;
using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Events {
	public class OnPickupGained : ScriptableObject {
		private readonly List<PickupGainedListener> listeners = new List<PickupGainedListener>();

		public void Raise(PlayerController player, PickupType type) {
			foreach (var listener in this.listeners) {
				listener.Invoke(player, type);
			}
		}

		public void RegisterListener(PickupGainedListener listener) {
			if (!this.listeners.Contains(listener)) {
				this.listeners.Add(listener);
			}
		}

		public void DeregisterListener(PickupGainedListener listener) {
			if (this.listeners.Contains(listener)) {
				this.listeners.Remove(listener);
			}
		}
	}
	
	public delegate void PickupGainedListener(PlayerController player, PickupType type);
}