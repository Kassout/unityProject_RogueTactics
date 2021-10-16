using System;
using System.Collections;
using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public class EasingControl : MonoBehaviour
{
    #region Events

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler UpdateEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler StateChangeEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler CompletedEvent;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public event EventHandler LoopedEvent;

    #endregion

    #region Enums

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum TimeType
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        Normal,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        Real,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        Fixed
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum PlayState
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        Stopped,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        Paused,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        Playing,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        Reversing
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum EndBehaviour
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        Constant,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        Reset
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public enum LoopType
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        Repeat,

        /// <summary>
        ///     TODO: comments
        /// </summary>
        PingPong
    }

    #endregion

    #region Fields / Properties

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public TimeType timeType = TimeType.Normal;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public PlayState playState { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public PlayState previousPlayState { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public EndBehaviour endBehaviour = EndBehaviour.Constant;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public LoopType loopType = LoopType.Repeat;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public bool IsPlaying => playState == PlayState.Playing || playState == PlayState.Reversing;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float startValue;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float endValue = 1.0f;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float duration = 1.0f;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public int loopCount;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Func<float, float, float, float> equation = EasingEquations.Linear;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float currentTime { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float currentValue { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public float currentOffset { get; private set; }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public int loops { get; private set; }

    #endregion

    #region MonoBehaviour

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void OnEnable()
    {
        Resume();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void OnDisable()
    {
        Pause();
    }

    #endregion

    #region Public

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Play()
    {
        SetPlayState(PlayState.Playing);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Reverse()
    {
        SetPlayState(PlayState.Reversing);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Pause()
    {
        if (IsPlaying)
        {
            SetPlayState(PlayState.Paused);
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Resume()
    {
        if (playState == PlayState.Paused)
        {
            SetPlayState(previousPlayState);
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void Stop()
    {
        SetPlayState(PlayState.Stopped);
        previousPlayState = PlayState.Stopped;
        loops = 0;
        if (endBehaviour == EndBehaviour.Reset)
        {
            SeekToBeginning();
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="time">TODO: comments</param>
    public void SeekToTime(float time)
    {
        currentTime = Mathf.Clamp01(time / duration);
        float newValue = (endValue - startValue) * currentTime + startValue;
        currentOffset = newValue - currentValue;
        currentValue = newValue;
        OnUpdate();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void SeekToBeginning()
    {
        SeekToTime(0.0f);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public void SeekToEnd()
    {
        SeekToTime(duration);
    }

    #endregion

    #region Protected

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected virtual void OnUpdate()
    {
        if (UpdateEvent != null)
        {
            UpdateEvent(this, System.EventArgs.Empty);
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected virtual void OnLoop()
    {
        if (LoopedEvent != null)
        {
            LoopedEvent(this, System.EventArgs.Empty);
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected virtual void OnComplete()
    {
        if (CompletedEvent != null)
        {
            CompletedEvent(this, System.EventArgs.Empty);
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected virtual void OnStateChange()
    {
        if (StateChangeEvent != null)
        {
            StateChangeEvent(this, System.EventArgs.Empty);
        }
    }

    #endregion

    #region Private

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="target">TODO: comments</param>
    private void SetPlayState(PlayState target)
    {
        if (isActiveAndEnabled)
        {
            if (playState == target)
            {
                return;
            }

            previousPlayState = playState;
            playState = target;
            OnStateChange();
            StopCoroutine(nameof(Ticker));
            if (IsPlaying)
            {
                StartCoroutine(nameof(Ticker));
            }
        }
        else
        {
            previousPlayState = target;
            playState = PlayState.Paused;
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    private IEnumerator Ticker()
    {
        while (true)
        {
            switch (timeType)
            {
                case TimeType.Normal:
                    yield return new WaitForEndOfFrame();
                    Tick(Time.deltaTime);
                    break;
                case TimeType.Real:
                    yield return new WaitForEndOfFrame();
                    Tick(Time.unscaledDeltaTime);
                    break;
                default: // Fixed
                    yield return new WaitForFixedUpdate();
                    Tick(Time.fixedDeltaTime);
                    break;
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="time">TODO: comments</param>
    private void Tick(float time)
    {
        bool finished;
        if (playState == PlayState.Playing)
        {
            currentTime = Mathf.Clamp01(currentTime + time / duration);
            finished = Mathf.Approximately(currentTime, 1.0f);
        }
        else // Reversing
        {
            currentTime = Mathf.Clamp01(currentTime - time / duration);
            finished = Mathf.Approximately(currentTime, 0.0f);
        }

        float frameValue = (endValue - startValue) * equation(0.0f, 1.0f, currentTime) + startValue;
        currentOffset = frameValue - currentValue;
        currentValue = frameValue;
        OnUpdate();

        if (finished)
        {
            ++loops;
            if (loopCount < 0 || loopCount >= loops)
            {
                if (loopType == LoopType.Repeat)
                {
                    SeekToBeginning();
                }
                else // PingPong
                {
                    SetPlayState(playState == PlayState.Playing ? PlayState.Reversing : PlayState.Playing);
                }

                OnLoop();
            }
            else
            {
                OnComplete();
                Stop();
            }
        }
    }

    #endregion
}