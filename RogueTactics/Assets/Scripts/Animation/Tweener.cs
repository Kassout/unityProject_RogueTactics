using System;
using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public abstract class Tweener : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    public static float defaultDuration = 1.0f;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    public static Func<float, float, float, float> defaultEquation = EasingEquations.EaseInOutQuad;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public AnimationEasingControl animationEasingControl;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    public bool destroyOnComplete = true;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void Awake()
    {
        animationEasingControl = gameObject.AddComponent<AnimationEasingControl>();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void OnEnable()
    {
        animationEasingControl.UpdateEvent += OnUpdate;
        animationEasingControl.CompletedEvent += OnComplete;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void OnDisable()
    {
        animationEasingControl.UpdateEvent -= OnUpdate;
        animationEasingControl.CompletedEvent -= OnComplete;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (animationEasingControl != null) Destroy(animationEasingControl);
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="sender">TODO: comments</param>
    /// <param name="e">TODO: comments</param>
    protected abstract void OnUpdate(object sender, System.EventArgs e);

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="sender">TODO: comments</param>
    /// <param name="e">TODO: comments</param>
    protected virtual void OnComplete(object sender, System.EventArgs e)
    {
        if (destroyOnComplete) Destroy(this);
    }

    #endregion
}