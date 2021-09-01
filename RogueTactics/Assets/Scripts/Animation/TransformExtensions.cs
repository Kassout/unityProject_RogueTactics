using System;
using UnityEngine;

public static class TransformExtensions
{
    public static Tweener MoveTo(this Transform t, Vector3 position)
    {
        return MoveTo(t, position, Tweener.defaultDuration);
    }

    public static Tweener MoveTo(this Transform t, Vector3 position, float duration)
    {
        return MoveTo(t, position, duration, Tweener.defaultEquation);
    }

    public static Tweener MoveTo(this Transform t, Vector3 position, float duration,
        Func<float, float, float, float> equation)
    {
        var tweener = t.gameObject.AddComponent<TransformPositionTweener>();
        tweener.startValue = t.position;
        tweener.endValue = position;
        tweener.animationEasingControl.duration = duration;
        tweener.animationEasingControl.equation = equation;
        tweener.animationEasingControl.Play();
        return tweener;
    }

    public static Tweener MoveToLocal(this Transform t, Vector3 position)
    {
        return MoveToLocal(t, position, Tweener.defaultDuration);
    }

    public static Tweener MoveToLocal(this Transform t, Vector3 position, float duration)
    {
        return MoveToLocal(t, position, duration, Tweener.defaultEquation);
    }

    public static Tweener MoveToLocal(this Transform t, Vector3 position, float duration,
        Func<float, float, float, float> equation)
    {
        var tweener = t.gameObject.AddComponent<TransformLocalPositionTweener>();
        tweener.startValue = t.localPosition;
        tweener.endValue = position;
        tweener.animationEasingControl.duration = duration;
        tweener.animationEasingControl.equation = equation;
        tweener.animationEasingControl.Play();
        return tweener;
    }

    public static Tweener ScaleTo(this Transform t, Vector3 scale)
    {
        return ScaleTo(t, scale, Tweener.defaultDuration);
    }

    public static Tweener ScaleTo(this Transform t, Vector3 scale, float duration)
    {
        return ScaleTo(t, scale, duration, Tweener.defaultEquation);
    }

    public static Tweener ScaleTo(this Transform t, Vector3 scale, float duration,
        Func<float, float, float, float> equation)
    {
        var tweener = t.gameObject.AddComponent<TransformScaleTweener>();
        tweener.startValue = t.localScale;
        tweener.endValue = scale;
        tweener.animationEasingControl.duration = duration;
        tweener.animationEasingControl.equation = equation;
        tweener.animationEasingControl.Play();
        return tweener;
    }
}