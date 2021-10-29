public abstract class AddStatusFeature<T> : Feature where T : StatusEffect
{
    #region Fields

    private StatusCondition _statusCondition;

    #endregion

    #region Protected

    protected override void OnApply()
    {
        Status status = GetComponentInParent<Status>();
        _statusCondition = status.Add<T, StatusCondition>();
    }

    protected override void OnRemove()
    {
        if (_statusCondition != null)
        {
            _statusCondition.Remove();
        }
    }

    #endregion
}
