using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    private RectTransform rt;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        rt = transform as RectTransform;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="sender">TODO: comments</param>
    /// <param name="e">TODO: comments</param>
    protected override void OnUpdate(object sender, System.EventArgs e)
    {
        base.OnUpdate(sender, e);
        rt.anchoredPosition = currentValue;
    }
}
