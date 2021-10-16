using System;
using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public static class TransformAnimationExtensions
{
    #region Public

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener MoveTo(this Transform t, Vector3 position)
    {
        return MoveTo(t, position, Tweener.DefaultDuration);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener MoveTo(this Transform t, Vector3 position, float duration)
    {
        return MoveTo(t, position, duration, Tweener.DefaultEquation);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <param name="equation">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener MoveTo(this Transform t, Vector3 position, float duration,
        Func<float, float, float, float> equation)
    {
        TransformPositionTweener tweener = t.gameObject.AddComponent<TransformPositionTweener>();
        tweener.startTweenValue = t.position;
        tweener.endTweenValue = position;
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play();
        return tweener;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener MoveToLocal(this Transform t, Vector3 position)
    {
        return MoveToLocal(t, position, Tweener.DefaultDuration);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener MoveToLocal(this Transform t, Vector3 position, float duration)
    {
        return MoveToLocal(t, position, duration, Tweener.DefaultEquation);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <param name="equation">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener MoveToLocal(this Transform t, Vector3 position, float duration,
        Func<float, float, float, float> equation)
    {
        TransformLocalPositionTweener tweener = t.gameObject.AddComponent<TransformLocalPositionTweener>();
        tweener.startTweenValue = t.localPosition;
        tweener.endTweenValue = position;
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play();
        return tweener;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="euler">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <param name="equation">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener RotateToLocal(this Transform t, Vector3 euler, float duration,
        Func<float, float, float, float> equation)
    {
        TransformLocalEulerTweener tweener = t.gameObject.AddComponent<TransformLocalEulerTweener>();
        tweener.startTweenValue = t.localEulerAngles;
        tweener.endTweenValue = euler;
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play();
        return tweener;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="scale">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener ScaleTo(this Transform t, Vector3 scale)
    {
        return ScaleTo(t, scale, Tweener.DefaultDuration);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="scale">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener ScaleTo(this Transform t, Vector3 scale, float duration)
    {
        return ScaleTo(t, scale, duration, Tweener.DefaultEquation);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="scale">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <param name="equation">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener ScaleTo(this Transform t, Vector3 scale, float duration,
        Func<float, float, float, float> equation)
    {
        TransformScaleTweener tweener = t.gameObject.AddComponent<TransformScaleTweener>();
        tweener.startTweenValue = t.localScale;
        tweener.endTweenValue = scale;
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play();
        return tweener;
    }

    #endregion
}