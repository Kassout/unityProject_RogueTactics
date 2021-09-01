using System;
using UnityEngine;

public static class RectTransformAnimationExtensions
{
    public static Tweener AnchorTo(this RectTransform t, Vector3 position)
    {
        return AnchorTo(t, position, Tweener.defaultDuration);
    }
    
    public static Tweener AnchorTo (this RectTransform t, Vector3 position, float duration)
    {
        return AnchorTo (t, position, duration, Tweener.defaultEquation);
    }
  
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
