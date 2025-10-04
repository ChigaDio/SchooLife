using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class GameSceneGameLoopState : BaseGameSceneGameLoopState
    {
        GamePlayStateControl control = new GamePlayStateControl();
        private bool isScene = false;
        public override void Enter(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) 
        {
            SceneLoader.LoadSceneAsync(GameScene.GamePlay, action: ()
                =>
            {
                isScene = true;
                control.StartState();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) 
        {
            if (!isScene) return;
            control.UpdateState();
            if(control.IsFinish)
            {
                IsActiveOff();
            }

        }
        public override void Exit(GameCore.States.Managers.GameSceneStateManagerData state_manager_data)
        {
        }
    }
}
