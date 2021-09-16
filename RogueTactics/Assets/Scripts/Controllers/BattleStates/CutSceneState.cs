using UnityEngine;
using UnityEngine.InputSystem;

public class CutSceneState : BattleState
{
    ConversationController _conversationController;
    ConversationData _data;

    public override void Enter()
    {
        base.Enter();
        
        _conversationController = owner.GetComponentInChildren<ConversationController>();
        if (IsBattleOver())
        {
            if (DidPlayerWin())
            {
                _data = Resources.Load<ConversationData>("Conversations/OutroSceneWin");
            }
            else
            {
                _data = Resources.Load<ConversationData>("Conversations/OutroSceneLose");
            }
        }
        else
        {
            _data = Resources.Load<ConversationData>("Conversations/IntroScene");
        }
        _conversationController.Show(_data);
    }

    public override void Exit()
    {
        base.Exit();
        
        if (_data)
        {
            Resources.UnloadAsset(_data);
        }
    }
        
    protected override void AddListeners()
    {
        base.AddListeners();
        ConversationController.completeEvent += OnCompleteConversation;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ConversationController.completeEvent -= OnCompleteConversation;
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        _conversationController.Next();
    }

    void OnCompleteConversation(object sender, System.EventArgs e)
    {
        if (IsBattleOver())
        {
            owner.ChangeState<EndBattleState>();
        }
        else
        {
            owner.ChangeState<TurnManagerState>();
        }
    }
}