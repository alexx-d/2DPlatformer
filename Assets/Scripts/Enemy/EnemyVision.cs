using System;
using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _checkInterval = 0.1f;

    private Transform _confirmedTarget;
    private Coroutine _visibilityCoroutine;
    private Transform _parentTransform;

    public event Action<Transform> PlayerSpotted;
    public event Action PlayerLost;

    private void Awake()
    {
        _parentTransform = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerLayer.Contains(other.gameObject.layer))
        {
            if (_visibilityCoroutine == null)
            {
                _visibilityCoroutine = StartCoroutine(CheckVisibilityRoutine(other.transform));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_playerLayer.Contains(other.gameObject.layer))
        {
            if (_visibilityCoroutine != null)
            {
                StopCoroutine(_visibilityCoroutine);
                _visibilityCoroutine = null;
            }

            _confirmedTarget = null;
        }
    }

    private IEnumerator CheckVisibilityRoutine(Transform target)
    {
        while (enabled)
        {
            Vector2 direction = (target.position - _parentTransform.position).normalized;
            float distance = Vector2.Distance(_parentTransform.position, target.position);

            RaycastHit2D hit = Physics2D.Raycast(_parentTransform.position, direction, distance, _obstacleLayer);

            if (hit.collider == null)
            {
                if (_confirmedTarget == null)
                {
                    _confirmedTarget = target;
                    PlayerSpotted?.Invoke(_confirmedTarget);
                }
            }
            else
            {
                if (_confirmedTarget != null)
                {
                    _confirmedTarget = null;
                    PlayerLost?.Invoke();
                }
            }

            yield return new WaitForSeconds(_checkInterval);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = transform.parent != null ? transform.parent.position : transform.position;

        if (_confirmedTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, _confirmedTarget.position);
        }
    }
}