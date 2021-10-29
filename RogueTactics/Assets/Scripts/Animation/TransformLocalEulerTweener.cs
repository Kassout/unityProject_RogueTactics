/// <summary>
/// TODO: comments
/// </summary>
public class TransformLocalEulerTweener : Vector3Tweener
{
    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void OnUpdate()
    {
        base.OnUpdate();
        transform.localEulerAngles = CurrentTweenValue;
    }

    #endregion
}