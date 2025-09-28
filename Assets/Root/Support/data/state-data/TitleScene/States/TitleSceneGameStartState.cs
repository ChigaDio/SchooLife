using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class TitleSceneGameStartState : BaseTitleSceneGameStartState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data)
        {
            IsActiveOff();
        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}
