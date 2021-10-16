/// <summary>
///     TODO: comments
/// </summary>
public class TransformPositionTweener : Vector3Tweener
{
    #region Protected

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void OnUpdate()
    {
        base.OnUpdate();
        transform.position = currentTweenValue;
    }

    #endregion
}