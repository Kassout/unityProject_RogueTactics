using System.Collections;
using ViewModelComponent;

namespace BattleStates
{
    public class MoveSequenceState : BattleState 
    {
        public override void Enter()
        {
            base.Enter ();
            StartCoroutine("Sequence");
        }
  
        IEnumerator Sequence()
        {
            UnitMovement m = owner.currentUnit.GetComponent<UnitMovement>();
            yield return StartCoroutine(m.Traverse(currentTile));
            owner.ChangeState<SelectUnitState>();
        }
    }
}
