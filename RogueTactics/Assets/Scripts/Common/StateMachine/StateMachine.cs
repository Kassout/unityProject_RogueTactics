using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public class StateMachine : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual State CurrentState
    {
        get => _currentState;
        set => Transition(value);
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    private State _currentState;

    /// <summary>
    /// TODO: comments
    /// </summary>
    private State _previousState;

    /// <summary>
    /// TODO: comments
    /// </summary>
    private bool _inTransition;
    
    #endregion
    
    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <typeparam name="T">TODO: comments</typeparam>
    /// <returns>TODO: comments</returns>
    protected virtual T GETState<T>() where T : State
    {
        T target = GetComponent<T>();

        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }

        return target;
    }


    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="value">TODO: comments</param>
    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
        {
            return;
        }

        _inTransition = true;

        if (null != _currentState)
        {
            _currentState.Exit();
        }

        _previousState = _currentState;
        _currentState = value;

        if (null != _currentState)
        {
            _currentState.Enter();
        }

        _inTransition = false;
    }

    #endregion

    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <typeparam name="T">TODO: comments</typeparam>
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GETState<T>();
    }
        
    public virtual string GetCurrentState()
    {
        return _currentState.GetType().Name;
    }
        
    public virtual string GetPreviousState()
    {
        return _previousState.GetType().Name;
    }

    #endregion
}