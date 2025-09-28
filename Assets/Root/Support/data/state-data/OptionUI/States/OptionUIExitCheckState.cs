using UnityEngine;

using GameCore.States.Branch;
using GameCore.Sound;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class OptionUIExitCheckState : BaseOptionUIExitCheckState
    {
        public override void Enter(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) 
        {
            OptionCanvas.Instance.AddListenerButton(() =>
            {
                SoundCore.Instance.SetSystemBGMVolume();
                SoundCore.Instance.SetSystemSEVolume();
                SoundCore.Instance.PlaySEAsync(SoundGroup.UI, SoundID.UI_PushEnter).Forget();
                IsActiveOff();
            });
        }
        public override void Update(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
    }
}
