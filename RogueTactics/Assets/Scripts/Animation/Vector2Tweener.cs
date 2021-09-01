using UnityEngine;

public class Vector2Tweener : Tweener
{
    public Vector2 startValue;

    public Vector2 endValue;

    public Vector2 currentValue { get; private set; }

    protected override void OnUpdate(object sender, System.EventArgs e)
    {
        currentValue = (endValue - startValue) * easingControl.currentValue + startValue;
    }
}