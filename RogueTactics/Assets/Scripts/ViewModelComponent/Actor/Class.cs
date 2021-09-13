using UnityEngine;

public sealed class Class : MonoBehaviour
{
    #region Fields / Properties

    public static readonly UnitStatTypes[] StatOrder = new UnitStatTypes[]
    {
        UnitStatTypes.MHP,
        UnitStatTypes.MMP,
        UnitStatTypes.STR,
        UnitStatTypes.MAG,
        UnitStatTypes.SKL,
        UnitStatTypes.SPD,
        UnitStatTypes.LUC,
        UnitStatTypes.DEF,
        UnitStatTypes.RES,
        UnitStatTypes.EVD,
        UnitStatTypes.TEN
    };

    public Sprite classModel;

    public AnimatorOverrideController classAnimator;

    public int[] baseStats = new int[StatOrder.Length];

    public float[] growStats = new float[StatOrder.Length];

    public int[] statCaps = new int[StatOrder.Length];

    private UnitStats _unitStats;

    #endregion

    #region MonoBehaviour

    private void OnDestroy()
    {
        this.RemoveObserver(OnLevelChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.LVL));
    }

    #endregion

    #region Public

    public void Promote()
    {
        gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = classModel;
        gameObject.GetComponentInParent<Animator>().runtimeAnimatorController = classAnimator;
        
        _unitStats = gameObject.GetComponentInParent<UnitStats>();
        this.AddObserver(OnLevelChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.LVL), _unitStats);

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
        
        this.RemoveObserver(OnLevelChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.LVL), _unitStats);
        _unitStats = null;
    }

    public void LoadDefaultStats()
    {
        for (int i = 0; i < StatOrder.Length; ++i)
        {
            UnitStatTypes type = StatOrder[i];
            _unitStats.SetValue(type, baseStats[i], false);
        }
        
        _unitStats.SetValue(UnitStatTypes.HP, _unitStats[UnitStatTypes.MHP], false);
        _unitStats.SetValue(UnitStatTypes.MP, _unitStats[UnitStatTypes.MMP], false);
    }

    #endregion

    #region Event Handlers

    private void OnLevelChangeNotification (object sender, object args)
    {
        int oldValue = (int)args;
        int newValue = _unitStats[UnitStatTypes.LVL];
        for (int i = oldValue; i < newValue; ++i)
        {
            LevelUp();
        }
    }

    #endregion
    
    #region Private
    
    void LevelUp ()
    {
        for (int i = 0; i < StatOrder.Length; ++i)
        {
            UnitStatTypes type = StatOrder[i];
            int whole = Mathf.FloorToInt(growStats[i]);
            float fraction = growStats[i] - whole;
            int value = _unitStats[type];
            value += whole;
            if (UnityEngine.Random.value > (1f - fraction))
                value++;
            _unitStats.SetValue(type, value, false);
        }
        _unitStats.SetValue(UnitStatTypes.HP, _unitStats[UnitStatTypes.MHP], false);
        _unitStats.SetValue(UnitStatTypes.MP, _unitStats[UnitStatTypes.MMP], false);
    }
    
    #endregion
}
