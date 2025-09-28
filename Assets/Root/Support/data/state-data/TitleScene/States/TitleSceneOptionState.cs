using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
namespace GameCore.States
{
    public class TitleSceneOptionState : BaseTitleSceneOptionState
    {
        OptionUIStateControl con = new OptionUIStateControl();
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            con.StartState();
        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data)
        {
            con.UpdateState();
            if(con.IsFinish)
            {
                IsActiveOff();
            }
        }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}
