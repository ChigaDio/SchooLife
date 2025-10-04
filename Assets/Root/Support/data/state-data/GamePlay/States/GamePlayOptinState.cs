using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
namespace GameCore.States
{
    public class GamePlayOptinState : BaseGamePlayOptinState
    {
        OptionUIStateControl con = new OptionUIStateControl();
        public override void Enter(GameCore.States.Managers.GamePlayStateManagerData state_manager_data)
        {
            con.StartState();
        }
        public override void Update(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) 
        {
            con.UpdateState();
            if (con.IsFinish)
            {
                IsActiveOff();
            }
        }
        public override void Exit(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
    }
}
