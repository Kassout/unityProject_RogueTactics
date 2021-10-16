using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    [RequireComponent(typeof(LayoutAnchor))]
    public class Panel : MonoBehaviour
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        [Serializable]
        public class Position
        {
            #region Fields / Properties

            /// <summary>
            ///     TODO: comments
            /// </summary>
            public string name;

            /// <summary>
            ///     TODO: comments
            /// </summary>
            public TextAnchor myAnchor;

            /// <summary>
            ///     TODO: comments
            /// </summary>
            public TextAnchor parentAnchor;

            /// <summary>
            ///     TODO: comments
            /// </summary>
            public Vector2 offset;

            #endregion

            #region Public

            /// <summary>
            ///     TODO: comments
            /// </summary>
            /// <param name="name">TODO: comments</param>
            public Position(string name)
            {
                this.name = name;
            }

            /// <summary>
            ///     TODO: comments
            /// </summary>
            /// <param name="name">TODO: comments</param>
            /// <param name="myAnchor">TODO: comments</param>
            /// <param name="parentAnchor">TODO: comments</param>
            public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor) : this(name)
            {
                this.myAnchor = myAnchor;
                this.parentAnchor = parentAnchor;
            }

            /// <summary>
            ///     TODO: comments
            /// </summary>
            /// <param name="name">TODO: comments</param>
            /// <param name="myAnchor">TODO: comments</param>
            /// <param name="parentAnchor">TODO: comments</param>
            /// <param name="offset">TODO: comments</param>
            public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name,
                myAnchor, parentAnchor)
            {
                this.offset = offset;
            }

            #endregion
        }

        #region Fields / Properties

        /// <summary>
        ///     TODO: comments
        /// </summary>
        [SerializeField] private List<Position> positionList;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private Dictionary<string, Position> _positionMap;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private LayoutAnchor _anchor;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public Position CurrentPosition { get; private set; }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public Tweener Transition { get; private set; }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public bool InTransition => Transition != null;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="key">TODO: comments</param>
        public Position this[string key]
        {
            get
            {
                if (_positionMap.ContainsKey(key))
                {
                    return _positionMap[key];
                }

                return null;
            }
        }

        #endregion

        #region MonoBehaviour

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private void Awake()
        {
            _anchor = GetComponent<LayoutAnchor>();

            _positionMap = new Dictionary<string, Position>(positionList.Count);

            for (int i = positionList.Count - 1; i >= 0; --i)
            {
                AddPosition(positionList[i]);
            }
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private void Start()
        {
            if (CurrentPosition == null && positionList.Count > 0)
            {
                SetPosition(positionList[0], false);
            }
        }

        #endregion

        #region Public

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="p"></param>
        public void AddPosition(Position p)
        {
            _positionMap[p.name] = p;
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="p">TODO: comments</param>
        public void RemovePosition(Position p)
        {
            if (_positionMap.ContainsKey(p.name))
            {
                _positionMap.Remove(p.name);
            }
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="positionName">TODO: comments</param>
        /// <param name="animated">TODO: comments</param>
        /// <returns>TODO: comments</returns>
        public Tweener SetPosition(string positionName, bool animated)
        {
            return SetPosition(this[positionName], animated);
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="p">TODO: comments</param>
        /// <param name="animated">TODO: comments</param>
        /// <returns>TODO: comments</returns>
        public Tweener SetPosition(Position p, bool animated)
        {
            CurrentPosition = p;
            if (CurrentPosition == null)
            {
                return null;
            }

            if (InTransition)
            {
                Transition.Stop();
            }

            if (animated)
            {
                Transition = _anchor.MoveToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
                return Transition;
            }

            _anchor.SnapToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return null;
        }

        #endregion
    }
}