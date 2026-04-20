using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(SpriteRenderer))]
public class HealthVisuals : MonoBehaviour
{
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private float _flashDuration = 0.1f;

    private Health _health;
    private SpriteRenderer _spriteRenderer;

    private Material _originalMaterial;
    private Coroutine _flashRoutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _originalMaterial = _spriteRenderer.material;
        _wait = new WaitForSeconds(_flashDuration);
    }

    private void OnEnable()
    {
        _health.Changed += OnDamageTaken;
    }
    private void OnDisable()
    {
        _health.Changed -= OnDamageTaken;
    }

    private void OnDamageTaken(int currentHealth)
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        _spriteRenderer.material = _flashMaterial;

        yield return _wait;

        _spriteRenderer.material = _originalMaterial;
        _flashRoutine = null;
    }
}