using System;

/// <summary>
/// TODO: comments
/// </summary>
public abstract class Tweener : EasingControl
{
    #region Event Handlers

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void OnComplete()
    {
        base.OnComplete();
        if (destroyOnComplete)
        {
            Destroy(this);
        }
    }

    #endregion

    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    public static float DefaultDuration = 1f;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public static readonly Func<float, float, float, float> DefaultEquation = EasingEquations.EaseInOutQuad;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public bool destroyOnComplete = true;

    #endregion
}