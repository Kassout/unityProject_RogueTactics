using UnityEngine.SceneManagement;

public class EndBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        
        this.PostNotification(BattleController.BattleEndedNotification);
        
        SceneManager.LoadScene(0);
    }
}
