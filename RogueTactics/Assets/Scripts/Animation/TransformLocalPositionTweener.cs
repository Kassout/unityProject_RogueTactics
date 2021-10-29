/// <summary>
/// TODO: comments
/// </summary>
public class TransformLocalPositionTweener : Vector3Tweener
{
    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void OnUpdate()
    {
        base.OnUpdate();
        transform.localPosition = CurrentTweenValue;
    }

    #endregion
}