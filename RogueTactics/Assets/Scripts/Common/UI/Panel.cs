using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutAnchor))]
public class Panel : MonoBehaviour
{
    [Serializable]
    public class Position
    {
        public string name;

        public TextAnchor myAnchor;

        public TextAnchor parentAnchor;

        public Vector2 offset;

        public Position(string name)
        {
            this.name = name;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor) : this(name)
        {
            this.myAnchor = myAnchor;
            this.parentAnchor = parentAnchor;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name,
            myAnchor, parentAnchor)
        {
            this.offset = offset;
        }
    }

    [SerializeField] private List<Position> positionList;

    private Dictionary<string, Position> _positionMap;

    private LayoutAnchor _anchor;

    private void Awake()
    {
        _anchor = GetComponent<LayoutAnchor>();

        _positionMap = new Dictionary<string, Position>(positionList.Count);

        for (int i = positionList.Count - 1; i >= 0; --i)
        {
            AddPosition(positionList[i]);
        }
    }
    
    public Position CurrentPosition { get; private set; }
    
    public Tweener Transition { get; private set; }
    
    public bool InTransition
    {
        get { return Transition != null; }
    }

    public Position this[string name]
    {
        get
        {
            if (_positionMap.ContainsKey(name))
            {
                return _positionMap[name];
            }

            return null;
        }
    }

    public void AddPosition(Position p)
    {
        _positionMap[p.name] = p;
    }

    public void RemovePosition(Position p)
    {
        if (_positionMap.ContainsKey(p.name))
        {
            _positionMap.Remove(p.name);
        }
    }

    public Tweener SetPosition(string positionName, bool animated)
    {
        return SetPosition(this[positionName], animated);
    }

    public Tweener SetPosition(Position p, bool animated)
    {
        CurrentPosition = p;
        if (CurrentPosition == null)
        {
            return null;
        }

        if (InTransition)
        {
            Transition.easingControl.Stop();
        }

        if (animated)
        {
            Transition = _anchor.MoveToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return Transition;
        }
        else
        {
            _anchor.SnapToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return null;
        }
    }

    private void Start()
    {
        if (CurrentPosition == null && positionList.Count > 0)
        {
            SetPosition(positionList[0], false);
        }
    }
}
