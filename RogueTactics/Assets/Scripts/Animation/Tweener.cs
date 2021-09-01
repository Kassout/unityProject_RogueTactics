using System;
using UnityEngine;

public abstract class Tweener : MonoBehaviour
{
    #region Properties

    public static float defaultDuration = 1.0f;
    public static Func<float, float, float, float> defaultEquation = EasingEquations.EaseInOutQuad;

    public AnimationEasingControl animationEasingControl;
    public bool destroyOnComplete = true;

    #endregion

    #region MonoBehaviour

    protected virtual void Awake()
    {
        animationEasingControl = gameObject.AddComponent<AnimationEasingControl>();
    }

    protected virtual void OnEnable()
    {
        animationEasingControl.updateEvent += OnUpdate;
        animationEasingControl.completedEvent += OnComplete;
    }

    protected virtual void OnDisable()
    {
        animationEasingControl.updateEvent -= OnUpdate;
        animationEasingControl.completedEvent -= OnComplete;
    }

    protected virtual void OnDestroy()
    {
        if (animationEasingControl != null) Destroy(animationEasingControl);
    }

    #endregion

    #region Event Handlers

    protected abstract void OnUpdate(object sender, System.EventArgs e);

    protected virtual void OnComplete(object sender, System.EventArgs e)
    {
        if (destroyOnComplete) Destroy(this);
    }

    #endregion
}