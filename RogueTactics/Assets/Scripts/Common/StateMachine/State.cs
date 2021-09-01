using UnityEngine;

namespace Common.StateMachine
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public abstract class State : MonoBehaviour
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        protected virtual void OnDestroy()
        {
            RemoveListeners();
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public virtual void Enter()
        {
            AddListeners();
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public virtual void Exit()
        {
            RemoveListeners();
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        protected virtual void AddListeners()
        {
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        protected virtual void RemoveListeners()
        {
        }
    }
}