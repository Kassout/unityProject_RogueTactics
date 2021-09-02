using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public abstract class Vector3Tweener : Tweener
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    public Vector3 startValue;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public Vector3 endValue;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public Vector3 currentValue { get; private set; }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="sender">TODO: comments</param>
    /// <param name="e">TODO: comments</param>
    protected override void OnUpdate(object sender, System.EventArgs e)
    {
        currentValue = (endValue - startValue) * animationEasingControl.currentValue + startValue;
    }
}