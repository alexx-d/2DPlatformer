using TMPro;
using UnityEngine;
using System.Text;

public class HealthTextView : HealthView
{
    [SerializeField] private TMP_Text _text;

    protected override void OnHealthChanged(int currentHealth)
    {
        UpdateText(currentHealth);
    }

    private void UpdateText(int currentHealth)
    {
        _text.text = $"{currentHealth}/{Health.MaxHealth}";
    }
}