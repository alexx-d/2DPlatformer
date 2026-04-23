using UnityEngine;

public class EntityFlipper : MonoBehaviour
{
    private const float FaceRightAngle = 0f;
    private const float FaceLeftAngle = 180f;

    private bool _isFacingRight = true;

    public void FaceDirection(float direction)
    {
        if (Mathf.Approximately(direction, 0))
        {
            return;
        }

        bool movingRight = direction > 0;

        if (movingRight == _isFacingRight)
        {
            return;
        }

        _isFacingRight = movingRight;

        float yAngle = direction > 0 ? FaceRightAngle : FaceLeftAngle;
        transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }
}