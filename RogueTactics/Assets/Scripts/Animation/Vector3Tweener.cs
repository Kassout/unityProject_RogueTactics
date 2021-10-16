using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public abstract class Vector3Tweener : Tweener
{
    #region Protected

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void OnUpdate()
    {
        currentTweenValue = (endTweenValue - startTweenValue) * currentValue + startTweenValue;
        base.OnUpdate();
    }

    #endregion

    #region Fields / Properties

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Vector3 startTweenValue;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Vector3 endTweenValue;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Vector3 currentTweenValue { get; private set; }

    #endregion
}