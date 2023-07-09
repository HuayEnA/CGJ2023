using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendMethod
{
    public static void SetLocalScaleX(this Transform transform,float value)
    {
        transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform transform, float value)
    {
        transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform transform, float value)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
    }
}
