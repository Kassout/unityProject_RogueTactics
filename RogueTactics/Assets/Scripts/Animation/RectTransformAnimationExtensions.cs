using System;
using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public static class RectTransformAnimationExtensions
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener AnchorTo(this RectTransform t, Vector3 position)
    {
        return AnchorTo(t, position, Tweener.defaultDuration);
    }
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener AnchorTo (this RectTransform t, Vector3 position, float duration)
    {
        return AnchorTo (t, position, duration, Tweener.defaultEquation);
    }
  
    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <param name="duration">TODO: comments</param>
    /// <param name="equation">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener AnchorTo (this RectTransform t, Vector3 position, float duration, Func<float, float, float, float> equation)
    {
        RectTransformAnchorPositionTweener tweener = t.gameObject.AddComponent<RectTransformAnchorPositionTweener> ();
        tweener.startValue = t.anchoredPosition;
        tweener.endValue = position;
        tweener.animationEasingControl.duration = duration;
        tweener.animationEasingControl.equation = equation;
        tweener.animationEasingControl.Play ();
        return tweener;
    }
}
