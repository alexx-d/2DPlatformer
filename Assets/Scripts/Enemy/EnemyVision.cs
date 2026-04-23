using System;
using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 5f;
    [SerializeField] private float _checkInterval = 0.2f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    private const int MaxTargetsToFind = 1;
    private readonly Collider2D[] _overlapResults = new Collider2D[MaxTargetsToFind];

    private Transform _parentTransform;
    private Transform _confirmedTarget;
    private Coroutine _visionCoroutine;
    private WaitForSeconds _wait;

    public event Action<Transform> PlayerSpotted;
    public event Action PlayerLost;

    private void Awake()
    {
        _parentTransform = transform.parent != null ? transform.parent : transform;
        _wait = new WaitForSeconds(_checkInterval);
    }

    private void OnEnable()
    {
        _visionCoroutine = StartCoroutine(VisionRoutine());
    }

    private void OnDisable()
    {
        if (_visionCoroutine != null)
        {
            StopCoroutine(_visionCoroutine);
            _visionCoroutine = null;
        }

        ClearTarget();
    }

    private IEnumerator VisionRoutine()
    {
        while (enabled)
        {
            int count = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _viewRadius,
                _overlapResults,
                _playerLayer
            );

            if (count > 0)
            {
                Transform target = _overlapResults[0].transform;

                if (IsVisible(target))
                {
                    if (_confirmedTarget == null)
                    {
                        _confirmedTarget = target;
                        PlayerSpotted?.Invoke(_confirmedTarget);
                    }
                }
                else
                {
                    ClearTarget();
                }
            }
            else
            {
                ClearTarget();
            }

            yield return _wait;
        }
    }

    private bool IsVisible(Transform target)
    {
        Vector2 origin = _parentTransform.position;
        Vector2 direction = (Vector2)target.position - origin;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, direction.magnitude, _obstacleLayer);

        return hit.collider == null;
    }

    private void ClearTarget()
    {
        if (_confirmedTarget != null)
        {
            _confirmedTarget = null;
            PlayerLost?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        if (_confirmedTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_parentTransform.position, _confirmedTarget.position);
        }
    }
}