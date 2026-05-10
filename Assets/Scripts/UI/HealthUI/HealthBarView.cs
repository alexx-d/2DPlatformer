using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : HealthView
{
    [SerializeField] private Slider _slider;

    protected override void OnHealthChanged(int currentHealth)
    {
        _slider.value = (float)currentHealth / Health.MaxHealth;
    }
}