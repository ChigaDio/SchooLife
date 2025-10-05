using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
using Cysharp.Threading.Tasks;
using GameCore.SaveSystem;
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
                var instance = GameSceneCanvas.Instance;
                var playerData = SaveManagerCore.Instance.PlayerProgress;
                //‚±‚±‚Å‰Šú‚Ìİ’è
                instance?.SetUp(playerData);

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
