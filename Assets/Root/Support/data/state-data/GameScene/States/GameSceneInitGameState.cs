using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
namespace GameCore.States
{
    public class GameSceneInitGameState : BaseGameSceneInitGameState
    {
        GameInitPlayerStateControl ctl = new GameInitPlayerStateControl();
        public override void Enter(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) { ctl.StartState(); }
        public override void Update(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) 
        {
            ctl.UpdateState();
            if(ctl.IsFinish)
            {
                IsActiveOff();
            }
        }
        public override void Exit(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) { }
    }
}
