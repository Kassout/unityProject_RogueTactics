using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuEntry : MonoBehaviour
{
    [SerializeField] private Text label;

    private Outline _outline;

    public string Title
    {
        get => label.text;
        set => label.text = value;
    }

    [Flags]
    private enum States
    {
        None = 0,
        Selected = 1 << 0,
        Locked = 1 << 1
    }

    public bool isLocked
    {
        get => (State & States.Locked) != States.None;
        set
        {
            if (value)
            {
                State |= States.Locked;
            }
            else
            {
                State &= ~States.Locked;
            }
        }
    }

    public bool isSelected
    {
        get => (State & States.Selected) != States.None;
        set
        {
            if (value)
            {
                State |= States.Selected;
            }
            else
            {
                State &= ~States.Selected;
            }
        }
    }

    private States State
    {
        get => _state;
        set
        {
            if (_state == value)
            {
                return;
            }

            _state = value;

            if (isLocked)
            {
                // bullet.sprite = disabledSprite;
                label.color = Color.gray;
                _outline.effectColor = new Color32(20, 36, 44, 255);
            } 
            else if (isSelected)
            {
                // bullet.sprite = selectedSprite;
                label.color = new Color32(249, 210, 118, 255);
                _outline.effectColor = new Color32(255, 160, 72, 255);
            }
            else
            {
                // bullet.sprite = normalSprite;
                label.color = Color.white;
                _outline.effectColor = new Color32(20, 36, 44, 255);   
            }
        }
    }

    private States _state;
    
    private void Awake()
    {
        _outline = label.GetComponent<Outline>();
    }

    public void Reset()
    {
        State = States.None;
    }
}
