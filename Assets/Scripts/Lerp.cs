using System;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Vector3 control;
    [SerializeField] private Vector3 control2;
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;
    [SerializeField] private float lerpTime;
    [SerializeField] private float lerpAmount = 3;
    
    [Header("Manual")]
    [Range(0, 1)][SerializeField] private float manualLerpControl = 0;
    [SerializeField] private bool manualLerp = false;
    
    private float lerpedValue;

    private void Start()
    {
        start = transform.position;
        lerpTime = 0;
        lerpedValue = 0;
    }

    private void Update()
    {
        lerpTime += Time.deltaTime;
        lerpedValue = Mathf.Clamp01(lerpTime / lerpAmount);

        Vector3 newPosition = QuadraticBezier(start, control, end, lerpedValue);
        transform.position = newPosition;
    }

    private Vector3 QuadraticBezier(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return Mathf.Pow(1-t, 2) * start + (1 - t) * control * 2 + t * t * end;
    }

    private Vector3 CubicBezier(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 3) * start + (1 - t) * control1 * 3 + Mathf.Pow(1 - t, 2) * control2 * 3 +
               t * t * t * end;
    }

}
