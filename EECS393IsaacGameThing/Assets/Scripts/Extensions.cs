using UnityEngine;
 
public static class Extensions
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        if (degrees != 0)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
        }
        return v;
    }

    public static float Angle(this Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    /*public static float AngleTo(this Vector2 src, Vector2 dest)
    {
        float angle = 0;
        var srcN = src.normalized;
        var destN = dest.normalized;
        if (!srcN.Equals(destN))
        {
            angle = Mathf.Acos(Vector2.Dot(srcN, destN)) * Mathf.Rad2Deg;
        }
        return angle;
    } */
}