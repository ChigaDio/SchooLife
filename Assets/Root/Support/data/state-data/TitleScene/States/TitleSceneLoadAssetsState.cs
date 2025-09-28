using UnityEngine;

using GameCore.States.Branch;
using Cysharp.Threading.Tasks;
using GameCore.Sound;
using AddressableSystem;
namespace GameCore.States
{
    public class TitleSceneLoadAssetsState : BaseTitleSceneLoadAssetsState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            SceneLoader.LoadSceneAsync(GameScene.Title,additive:true, action: async () =>
            {
                while(SoundCore.Instance.IsLoadDatabase == false)
                {
                    await UniTask.Yield();
                }
                SoundCore.Instance.LoadGroupAsync(SoundGroup.Title, GroupCategory.Title,action: () =>
                {
                    SoundCore.Instance.PlayBGM(SoundGroup.Title, SoundID.Title_TitleBGM, fadeTime: 3.0f);
                    IsActiveOff();
                }).Forget();
            }).Forget();

        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}
