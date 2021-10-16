using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public class RectTransformAnchorPositionTweener : Vector3Tweener 
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    private RectTransform _rectTransform;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// TODO: comments
    /// </summary>
    private void Awake ()
    {
        _rectTransform = transform as RectTransform;
    }

    #endregion

    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void OnUpdate ()
    {
        base.OnUpdate ();
        _rectTransform.anchoredPosition = currentTweenValue;
    }

    #endregion
}