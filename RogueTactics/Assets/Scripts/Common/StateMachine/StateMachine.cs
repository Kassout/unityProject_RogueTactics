using UnityEngine;

namespace Common.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        protected virtual State CurrentState
        {
            get => _currentState;
            set => Transition(value);
        }

        private State _currentState;

        private bool _inTransition;

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
}
