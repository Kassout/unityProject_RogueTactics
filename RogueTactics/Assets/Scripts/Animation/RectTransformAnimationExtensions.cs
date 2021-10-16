using UnityEngine;
using System;

/// <summary>
/// TODO: comments
/// </summary>
public static class RectTransformAnimationExtensions 
{
    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="t">TODO: comments</param>
    /// <param name="position">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static Tweener AnchorTo (this RectTransform t, Vector3 position)
    {
        return AnchorTo (t, position, Tweener.DefaultDuration);
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
        return AnchorTo (t, position, duration, Tweener.DefaultEquation);
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
        tweener.startTweenValue = t.anchoredPosition;
        tweener.endTweenValue = position;
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play ();
        return tweener;
    }

    #endregion
}