using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class HitSuccessIndicator : MonoBehaviour
{
    private const string ShowKey = "Show";
    private const string HideKey = "Hide";

    [SerializeField] private Canvas canvas;
    [SerializeField] private Panel primaryPanel;
    [SerializeField] private Panel secondaryPanel;
    [SerializeField] private Text primaryLabel;
    [SerializeField] private Text secondaryLabel;
    private Tweener transition;

    private void Start()
    {
        primaryPanel.SetPosition(HideKey, false);
        secondaryPanel.SetPosition(HideKey, false);
        canvas.gameObject.SetActive(false);
    }

    public void SetAttackerStats(int chance, int amout)
    {
        primaryLabel.text = string.Format("{0}% {1}pt(s)", chance, amout);
    }

    public void SetDefenderStats(int chance, int amount)
    {
        secondaryLabel.text = string.Format("{0}% {1}pt(s)", chance, amount);
    }

    public void Show()
    {
        canvas.gameObject.SetActive(true);
        SetPanelPos(ShowKey);
    }

    public void Hide()
    {
        SetPanelPos(HideKey);
        transition.CompletedEvent += delegate(object sender, System.EventArgs args)
        {
            canvas.gameObject.SetActive(false);
        };
    }

    void SetPanelPos(string pos)
    {
        if (transition != null && transition.IsPlaying)
        {
            transition.Stop();
        }

        transition = primaryPanel.SetPosition(pos, true);
        transition.duration = 0.5f;
        transition.equation = EasingEquations.EaseInOutQuad;

        transition = secondaryPanel.SetPosition(pos, true);
        transition.duration = 0.5f;
        transition.equation = EasingEquations.EaseInOutQuad;
    }
}
