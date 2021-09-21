using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("Camera Target Options")]
    [SerializeField] private Transform target = default;
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private Vector2 xClampValue = Vector2.zero;
    [SerializeField] private Vector2 yClampValue = Vector2.zero;

    private Vector3 r_velocity;

    private void FixedUpdate()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(transform.position.x, xClampValue[0], xClampValue[1]);
        position.y = Mathf.Clamp(transform.position.y, yClampValue[0], yClampValue[1]);

        r_velocity *= Time.smoothDeltaTime;
        transform.position = Vector3.SmoothDamp(position, target.position + offset, ref r_velocity, smoothTime);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
