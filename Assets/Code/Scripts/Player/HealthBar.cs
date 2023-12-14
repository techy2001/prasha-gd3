using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Player {
    public class HealthBar : MonoBehaviour {
        public Slider healthSlider;

        public void SetSlider(float amount) {
            this.healthSlider.value = amount;
        }

        public void SetSliderMax(float amount) {
            this.healthSlider.maxValue = amount;
            this.SetSlider(amount);
        }
    }
}
