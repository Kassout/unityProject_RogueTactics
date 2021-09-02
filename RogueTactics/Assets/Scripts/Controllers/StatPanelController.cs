using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelController : MonoBehaviour
{
    #region Constant

    private const string ShowKey = "Show";
    private const string HideKey = "Hide";

    #endregion

    #region Fields / Properties

    [SerializeField] private StatPanel primaryPanel;
    [SerializeField] private StatPanel secondaryPanel;

    private Tweener primaryTransition;
    private Tweener secondaryTransition;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        if (primaryPanel.panel.CurrentPosition == null)
        {
            primaryPanel.panel.SetPosition(HideKey, false);
        }

        if (secondaryPanel.panel.CurrentPosition == null)
        {
            secondaryPanel.panel.SetPosition(ShowKey, false);
        }
    }

    #endregion

    #region Public

    public void ShowPrimary(GameObject obj)
    {
        primaryPanel.Display(obj);
        MovePanel(primaryPanel, ShowKey, ref primaryTransition);
    }

    public void HidePrimary()
    {
        MovePanel(primaryPanel, HideKey, ref primaryTransition);
    }

    public void ShowSecondary(GameObject obj)
    {
        secondaryPanel.Display(obj);
        MovePanel(secondaryPanel, ShowKey, ref secondaryTransition);
    }

    public void HideSecondary()
    {
        MovePanel(secondaryPanel, HideKey, ref secondaryTransition);
    }

    #endregion

    #region Private

    private void MovePanel(StatPanel obj, string pos, ref Tweener t)
    {
        Panel.Position target = obj.panel[pos];
        if (obj.panel.CurrentPosition != target)
        {
            if (t != null && t.animationEasingControl != null)
            {
                t.animationEasingControl.Stop();
            }

            t = obj.panel.SetPosition(pos, true);
            t.animationEasingControl.duration = 0.5f;
            t.animationEasingControl.equation = EasingEquations.EaseOutQuad;
        }
    }

    #endregion
}
