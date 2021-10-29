using UnityEngine;

public class KnockOutStatusEffect : StatusEffect
{
    private Unit _owner;
    private UnitStats _unitStats;

    private void Awake()
    {
        _owner = GetComponentInParent<Unit>();
        _unitStats = _owner.GetComponent<UnitStats>();
    }

    private void OnEnable()
    {
        _owner.transform.localScale = new Vector2(0.75f, 0.75f);
        _owner.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        BattleController.Instance.units.Remove(_owner);
        Board.GetTile(_owner.transform.position).content = null;
    }

    private void OnDisable()
    {
        _owner.transform.localScale = Vector2.zero;
        _owner.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        BattleController.Instance.units.Add(_owner);
        Board.GetTile(_owner.transform.position).content = _owner.gameObject;
    }
}
