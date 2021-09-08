using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Class : MonoBehaviour
{
    #region Fields / Properties

    public static readonly StatTypes[] statOrder = new StatTypes[]
    {
        StatTypes.MHP,
        StatTypes.MMP,
        StatTypes.STR,
        StatTypes.DEF,
        StatTypes.MAG,
        StatTypes.RES,
        StatTypes.SPD
    };

    public Sprite classModel;

    public AnimatorOverrideController classAnimator;

    public int[] baseStats = new int[statOrder.Length];

    public float[] growStats = new float[statOrder.Length];

    private Stats _stats;

    #endregion

    #region MonoBehaviour

    private void OnDestroy()
    {
        this.RemoveObserver(OnLevelChangeNotification, Stats.DidChangeNotification(StatTypes.LVL));
    }

    #endregion

    #region Public

    public void Promote()
    {
        gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = classModel;
        gameObject.GetComponentInParent<Animator>().runtimeAnimatorController = classAnimator;
        
        _stats = gameObject.GetComponentInParent<Stats>();
        this.AddObserver(OnLevelChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), _stats);

        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Activate(gameObject);
        }
    }

    public void Relegate()
    {
        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Deactivate();
        }
        
        this.RemoveObserver(OnLevelChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), _stats);
        _stats = null;
    }

    public void LoadDefaultStats()
    {
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            _stats.SetValue(type, baseStats[i], false);
        }
        
        _stats.SetValue(StatTypes.HP, _stats[StatTypes.MHP], false);
        _stats.SetValue(StatTypes.MP, _stats[StatTypes.MMP], false);
    }

    #endregion

    #region Event Handlers

    protected virtual void OnLevelChangeNotification (object sender, object args)
    {
        int oldValue = (int)args;
        int newValue = _stats[StatTypes.LVL];
        for (int i = oldValue; i < newValue; ++i)
        {
            LevelUp();
        }
    }

    #endregion
    
    #region Private
    
    void LevelUp ()
    {
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            int whole = Mathf.FloorToInt(growStats[i]);
            float fraction = growStats[i] - whole;
            int value = _stats[type];
            value += whole;
            if (UnityEngine.Random.value > (1f - fraction))
                value++;
            _stats.SetValue(type, value, false);
        }
        _stats.SetValue(StatTypes.HP, _stats[StatTypes.MHP], false);
        _stats.SetValue(StatTypes.MP, _stats[StatTypes.MMP], false);
    }
    
    #endregion
}
