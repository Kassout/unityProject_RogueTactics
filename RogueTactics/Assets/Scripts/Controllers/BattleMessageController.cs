using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMessageController : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private GameObject canvas;
    [SerializeField] private CanvasGroup group;
    private AnimationEasingControl ec;

    private void Awake()
    {
        ec = gameObject.AddComponent<AnimationEasingControl>();
        ec.duration = 0.5f;
        ec.equation = EasingEquations.EaseInOutQuad;
        ec.endBehaviour = AnimationEasingControl.EndBehaviour.Constant;
        ec.UpdateEvent += OnUpdateEvent;
    }

    public void Display(string message)
    {
        group.alpha = 0;
        canvas.SetActive(true);
        label.text = message;
        StartCoroutine(Sequence());
    }

    public void OnUpdateEvent(object sender, System.EventArgs e)
    {
        group.alpha = ec.currentValue;
    }

    IEnumerator Sequence()
    {
        ec.Play();

        while (ec.IsPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);
        
        ec.Reverse();

        while (ec.IsPlaying)
        {
            yield return null;
        }
        
        canvas.SetActive(false);
    }
}
