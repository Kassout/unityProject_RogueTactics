using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleStates
{
    public class CutSceneState : BattleState 
    {
        ConversationController _conversationController;
        ConversationData _data;
        protected override void Awake ()
        {
            base.Awake ();
            _conversationController = owner.GetComponentInChildren<ConversationController>();
            _data = Resources.Load<ConversationData>("Conversations/IntroScene");
        }
        protected override void OnDestroy ()
        {
            base.OnDestroy ();
            if (_data)
            {
                Resources.UnloadAsset(_data);   
            }
        }
        public override void Enter ()
        {
            base.Enter ();
            _conversationController.Show(_data);
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
        
        void OnCompleteConversation (object sender, System.EventArgs e)
        {
            owner.ChangeState<SelectUnitState>();
        }
    }
}