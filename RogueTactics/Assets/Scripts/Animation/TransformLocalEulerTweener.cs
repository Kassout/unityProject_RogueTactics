﻿/// <summary>
/// TODO: comments
/// </summary>
public class TransformLocalEulerTweener : Vector3Tweener
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="sender">TODO: comments</param>
    /// <param name="e">TODO: comments</param>
    protected override void OnUpdate(object sender, System.EventArgs e)
    {
        base.OnUpdate(sender, e);
        transform.localEulerAngles = currentValue;
    }
}