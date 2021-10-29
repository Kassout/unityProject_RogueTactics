/// <summary>
/// TODO: comments
/// </summary>
public class TransformScaleTweener : Vector3Tweener
{
    #region Protected

        /// <summary>
        /// TODO: comments
        /// </summary>
        protected override void OnUpdate()
        {
            base.OnUpdate();
            transform.localScale = CurrentTweenValue;
        }

    #endregion
}