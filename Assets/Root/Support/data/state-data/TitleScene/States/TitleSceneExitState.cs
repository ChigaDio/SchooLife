using UnityEngine;

using GameCore.States.Branch;
using GameCore.Sound;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class TitleSceneExitState : BaseTitleSceneExitState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            state_manager_data.OnExit();
            IsActiveOff();
        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}
