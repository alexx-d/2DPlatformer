using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismVisuals : MonoBehaviour
{
    [SerializeField] private PlayerVampirism _vampirism;
    [SerializeField] private SpriteRenderer _radiusVisual;
    [SerializeField] private Slider _timerSlider;

    private const float RadiusToDiameterMultiplier = 2f;
    private const float MaxSliderValue = 1f;
    private const float MinSliderValue = 0f;

    private Coroutine _sliderRoutine;

    private void OnEnable()
    {
        _vampirism.StateChanged += OnStateChanged;

        InitializeRadiusScale();
        ResetVisuals();
    }

    private void OnDisable()
    {
        _vampirism.StateChanged -= OnStateChanged;
    }

    private void OnStateChanged(float duration)
    {
        UpdateRadiusVisibility();

        if (_sliderRoutine != null)
        {
            StopCoroutine(_sliderRoutine);
        }

        float targetValue = _radiusVisual.gameObject.activeSelf ? MinSliderValue : MaxSliderValue;

        _sliderRoutine = StartCoroutine(AnimateSlider(targetValue, duration));
    }

    private IEnumerator AnimateSlider(float targetValue, float duration)
    {
        float startValue = _timerSlider.value;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _timerSlider.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }

        _timerSlider.value = targetValue;
    }

    private void InitializeRadiusScale()
    {
        float diameter = _vampirism.Radius * RadiusToDiameterMultiplier;
        _radiusVisual.transform.localScale = new Vector3(diameter, diameter, 1f);
    }

    private void UpdateRadiusVisibility()
    {
        _radiusVisual.gameObject.SetActive(!_radiusVisual.gameObject.activeSelf);
    }

    private void ResetVisuals()
    {
        _timerSlider.value = MaxSliderValue;
        _radiusVisual.gameObject.SetActive(false);
    }
}