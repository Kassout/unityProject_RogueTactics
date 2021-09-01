using UnityEngine;

public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    private RectTransform rt;

    protected override void Awake()
    {
        base.Awake();
        rt = transform as RectTransform;
    }

    protected override void OnUpdate(object sender, System.EventArgs e)
    {
        base.OnUpdate(sender, e);
        rt.anchoredPosition = currentValue;
    }
}
