using System;
using UnityEngine;


public static class DebugShape
{
    public static void DrawArrow(
        Vector3 origin,
        Vector3 direction,
        float duration = 1f,
        float length = 1f,
        Color? color = null,
        float headLength = 0.25f,
        float headAngle = 20f)
    {
        return;

        if (direction.sqrMagnitude < 0.0001f)
            return;

        Color c = color ?? Color.red;
        Vector3 dir = direction.normalized;
        Vector3 end = origin + dir * length;

        Debug.DrawLine(origin, end, c, duration);

        Vector3 right = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 + headAngle, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 - headAngle, 0) * Vector3.forward;

        Debug.DrawLine(end, end + right * headLength, c, duration);
        Debug.DrawLine(end, end + left * headLength, c, duration);
    }

    public static void DrawSphere(
        Vector3 center,
        float radius,
        Color color,
        float duration = 0f,
        int segments = 24)
    {
        DrawCircle(center, radius, color, duration, segments, (a) => new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a)));
        DrawCircle(center, radius, color, duration, segments, (a) => new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0));
        DrawCircle(center, radius, color, duration, segments, (a) => new Vector3(0, Mathf.Cos(a), Mathf.Sin(a)));
    }

    private static void DrawCircle(
        Vector3 center,
        float radius,
        Color color,
        float duration,
        int segments,
        Func<float, Vector3> pointOnCircle)
    {

        Vector3 lastPoint = center + pointOnCircle(0) * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 nextPoint = center + pointOnCircle(angle) * radius;
            Debug.DrawLine(lastPoint, nextPoint, color, duration);
            lastPoint = nextPoint;
        }
    }
}
