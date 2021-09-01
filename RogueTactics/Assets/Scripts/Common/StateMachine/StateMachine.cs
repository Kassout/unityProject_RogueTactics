using UnityEngine;

namespace Common.StateMachine
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        private State _currentState;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private bool _inTransition;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        protected virtual State CurrentState
        {
            get => _currentState;
            set => Transition(value);
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <typeparam name="T">TODO: comments</typeparam>
        /// <returns>TODO: comments</returns>
        protected virtual T GETState<T>() where T : State
        {
            var target = GetComponent<T>();

            if (target == null) target = gameObject.AddComponent<T>();

            return target;
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <typeparam name="T">TODO: comments</typeparam>
        public virtual void ChangeState<T>() where T : State
        {
            CurrentState = GETState<T>();
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="value">TODO: comments</param>
        protected virtual void Transition(State value)
        {
            if (_currentState == value || _inTransition) return;

            _inTransition = true;

            if (null != _currentState) _currentState.Exit();

            _currentState = value;

            if (null != _currentState) _currentState.Enter();

            _inTransition = false;
        }
    }
}