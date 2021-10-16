using UnityEngine;

namespace Common.UI
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class LayoutAnchor : MonoBehaviour
    {
        #region MonoBehaviour

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private void Awake()
        {
            Transform thisTransform = transform;
            _rectTransform = thisTransform as RectTransform;
            _parentRectTransform = thisTransform.parent as RectTransform;
            if (_parentRectTransform == null)
            {
                Debug.LogError("This component requires a RectTransform parent to work.", gameObject);
            }
        }

        #endregion

        #region Private

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="rectTransform">TODO: comments</param>
        /// <param name="anchor">TODO: comments</param>
        /// <returns>TODO: comments</returns>
        private Vector2 GetPosition(RectTransform rectTransform, TextAnchor anchor)
        {
            Vector2 retValue = Vector2.zero;
            switch (anchor)
            {
                case TextAnchor.LowerCenter:
                case TextAnchor.MiddleCenter:
                case TextAnchor.UpperCenter:
                    retValue.x += rectTransform.rect.width * 0.5f;
                    break;
                case TextAnchor.LowerRight:
                case TextAnchor.MiddleRight:
                case TextAnchor.UpperRight:
                    retValue.x += rectTransform.rect.width;
                    break;
            }

            switch (anchor)
            {
                case TextAnchor.MiddleLeft:
                case TextAnchor.MiddleCenter:
                case TextAnchor.MiddleRight:
                    retValue.y += rectTransform.rect.height * 0.5f;
                    break;
                case TextAnchor.UpperLeft:
                case TextAnchor.UpperCenter:
                case TextAnchor.UpperRight:
                    retValue.y += rectTransform.rect.height;
                    break;
            }

            return retValue;
        }

        #endregion

        #region Fields / Properties

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private RectTransform _parentRectTransform;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        private RectTransform _rectTransform;

        #endregion

        #region Public

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="myAnchor">TODO: comments</param>
        /// <param name="parentAnchor">TODO: comments</param>
        /// <param name="offset">TODO: comments</param>
        /// <returns>TODO: comments</returns>
        public Vector2 AnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
        {
            Vector2 myOffset = GetPosition(_rectTransform, myAnchor);
            Vector2 parentOffset = GetPosition(_parentRectTransform, parentAnchor);
            Vector2 anchorCenter =
                new Vector2(Mathf.Lerp(_rectTransform.anchorMin.x, _rectTransform.anchorMax.x, _rectTransform.pivot.x),
                    Mathf.Lerp(_rectTransform.anchorMin.y, _rectTransform.anchorMax.y, _rectTransform.pivot.y));

            Rect parentRect = _parentRectTransform.rect;
            Vector2 myAnchorOffset = new Vector2(parentRect.width * anchorCenter.x, parentRect.height * anchorCenter.y);
            Vector2 myPivotOffset = new Vector2(_rectTransform.rect.width * _rectTransform.pivot.x,
                _rectTransform.rect.height * _rectTransform.pivot.y);
            Vector2 pos = parentOffset - myAnchorOffset - myOffset + myPivotOffset + offset;

            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            return pos;
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="myAnchor">TODO: comments</param>
        /// <param name="parentAnchor">TODO: comments</param>
        /// <param name="offset">TODO: comments</param>
        public void SnapToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
        {
            _rectTransform.anchoredPosition = AnchorPosition(myAnchor, parentAnchor, offset);
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="myAnchor">TODO: comments</param>
        /// <param name="parentAnchor">TODO: comments</param>
        /// <param name="offset">TODO: comments</param>
        /// <returns>TODO: comments</returns>
        public Tweener MoveToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
        {
            return _rectTransform.AnchorTo(AnchorPosition(myAnchor, parentAnchor, offset));
        }

        #endregion
    }
}