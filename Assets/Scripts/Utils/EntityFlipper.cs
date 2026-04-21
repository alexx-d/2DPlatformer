using UnityEngine;

public class EntityFlipper : MonoBehaviour
{
    private const float FaceRightAngle = 0f;
    private const float FaceLeftAngle = 180f;

    public void FaceDirection(float direction)
    {
        if (Mathf.Approximately(direction, 0))
        {
            return;
        }

        float yAngle = direction > 0 ? FaceRightAngle : FaceLeftAngle;
        transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }
}