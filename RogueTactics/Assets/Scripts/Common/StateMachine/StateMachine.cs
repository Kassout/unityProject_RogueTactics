using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }

    protected State _currentState;

    protected bool _inTransition;

    public virtual T getState<T>() where T : State
    {
        T target = GetComponent<T>();

        if (target == null) {
            target = gameObject.AddComponent<T>();
        }

        return target;
    }

    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = getState<T>();
    }

    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition) {
            return;
        }

        _inTransition = true;

        if (null != _currentState)
        {
            _currentState.Exit();
        }

        _currentState = value;

        if (null != _currentState) {
            _currentState.Enter();
        }

        _inTransition = false;
    }
}
