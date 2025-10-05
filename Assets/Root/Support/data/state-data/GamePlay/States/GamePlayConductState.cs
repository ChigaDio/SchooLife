using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
namespace GameCore.States
{
    public class GamePlayConductState : BaseGamePlayConductState
    {
        ConductStateControl ctl = new ConductStateControl();
        public override void Enter(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { ctl.StartState(); }
        public override void Update(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) 
        {
            ctl.UpdateState();
            if(ctl.IsFinish)
            {
                IsActiveOff();
            }
        }
        public override void Exit(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
    }
}
