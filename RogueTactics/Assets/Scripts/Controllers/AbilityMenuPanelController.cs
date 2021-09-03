using System.Collections.Generic;
using UnityEngine;

public class AbilityMenuPanelController : MonoBehaviour
{
    private const string ShowKey = "Show";
    private const string HideKey = "Hide";
    private const string EntryPoolKey = "AbilityMenuPanel.Entry";
    private const int MenuCount = 4;
    
    public int selection { get; private set; }
    
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvas;

    private readonly List<AbilityMenuEntry> _menuEntries = new List<AbilityMenuEntry>(MenuCount);

    private void Awake()
    {
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }

    private void Start()
    {
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }

    private AbilityMenuEntry Dequeue()
    {
        Poolable p = GameObjectPoolController.Dequeue(EntryPoolKey);
        AbilityMenuEntry entry = p.GetComponent<AbilityMenuEntry>();
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;
    }

    private void Enqueue(AbilityMenuEntry entry)
    {
        Poolable p = entry.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    private void Clear()
    {
        for (int i = _menuEntries.Count - 1; i >= 0; --i)
        {
            Enqueue(_menuEntries[i]);
        }
        
        _menuEntries.Clear();
    }

    private Tweener TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.animationEasingControl.duration = 0.5f;
        t.animationEasingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }

    private bool SetSelection(int value)
    {
        if (_menuEntries[value].isLocked)
        {
            return false;
        }
        
        // Deselect the previously selected entry
        if (selection >= 0 && selection < _menuEntries.Count)
        {
            _menuEntries[selection].isSelected = false;
        }

        selection = value;
        
        // Select the new entry
        if (selection >= 0 && selection < _menuEntries.Count)
        {
            _menuEntries[selection].isSelected = true;
        }

        return true;
    }

    public void Next()
    {
        for (int i = selection + 1; i < selection + _menuEntries.Count; ++i)
        {
            int index = i % _menuEntries.Count;
            if (SetSelection(index))
            {
                break;
            }
        }
    }

    public void NextMenuSelection()
    {
        if (selection + 1 <= _menuEntries.Count - 1)
        {
            SetSelection(selection + 1);
        }
    }

    public void Previous()
    {
        for (int i = selection - 1 + _menuEntries.Count; i > selection; --i)
        {
            int index = i % _menuEntries.Count;
            if (SetSelection(index))
            {
                break;
            }
        }
    }

    public void PreviousMenuSelection()
    {
        if (selection - 1 >= 0)
        {
            SetSelection(selection - 1);
        }
    }

    public void Show(List<string> options)
    {
        canvas.SetActive(true);
        Clear();

        foreach (var t in options)
        {
            AbilityMenuEntry entry = Dequeue();
            entry.Title = t;
            _menuEntries.Add(entry);
        }

        SetSelection(0);
        TogglePos(ShowKey);
    }

    public void SetLocked(int index, bool value)
    {
        if (index < 0 || index >= _menuEntries.Count)
        {
            return;
        }

        _menuEntries[index].isLocked = value;

        if (value && selection == index)
        {
            Next();
        }
    }

    public void Hide()
    {
        Tweener t = TogglePos(HideKey);
        t.animationEasingControl.CompletedEvent += delegate(object sender, System.EventArgs e)
        {
            if (panel.CurrentPosition == panel[HideKey])
            {
                Clear();
                canvas.SetActive(false);
            }
        };
    }
}