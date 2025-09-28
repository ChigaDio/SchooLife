using UnityEngine;

using GameCore.States.Branch;
using GameCore.Sound;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class TitleSceneUnLoadAssetsState : BaseTitleSceneUnLoadAssetsState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            SoundCore.Instance.UnloadGroup(SoundGroup.Title,AddressableSystem.GroupCategory.Title,action:() =>
            {
                SceneLoader.UnloadSceneAsync(GameScene.Title, action: () =>
                {
                    IsActiveOff();
                }).Forget();

            });
        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}
