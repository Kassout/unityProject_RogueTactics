using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTests : MonoBehaviour
{
    private Panel _panel;

    private const string Show = "Show";

    private const string Hide = "Hide";

    private const string Center = "Center";

    private void Start()
    {
        _panel = GetComponent<Panel>();

        Panel.Position centerPos = new Panel.Position(Center, TextAnchor.MiddleCenter, TextAnchor.MiddleCenter);
        _panel.AddPosition(centerPos);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), Show))
        {
            _panel.SetPosition(Show, true);
        }

        if (GUI.Button(new Rect(10, 50, 100, 30), Hide))
        {
            _panel.SetPosition(Hide, true);
        }

        if (GUI.Button(new Rect(10, 90, 100, 30), Center))
        {
            Tweener t = _panel.SetPosition(Center, true);
            t.animationEasingControl.equation = EasingEquations.EaseInOutBack;
        }
    }
}
