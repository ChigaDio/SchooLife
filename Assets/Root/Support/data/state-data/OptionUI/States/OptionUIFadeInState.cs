using UnityEngine;

using GameCore.States.Branch;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class OptionUIFadeInState : BaseOptionUIFadeInState
    {
        public override void Enter(GameCore.States.Managers.OptionUIStateManagerData state_manager_data)
        {
            OptionCanvas.Instance.FadeInAsync(0.5f, 0.5f, () =>
            {
                IsActiveOff();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
    }
}
