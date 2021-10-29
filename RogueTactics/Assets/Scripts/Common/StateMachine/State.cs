using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public abstract class State : MonoBehaviour
{
    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void AddListeners()
    {
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void RemoveListeners()
    {
    }

    #endregion
    
    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    public virtual void Enter()
    {
        AddListeners();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public virtual void Exit()
    {
        RemoveListeners();
    }

    #endregion
}