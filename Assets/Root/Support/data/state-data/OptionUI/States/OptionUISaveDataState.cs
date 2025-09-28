using UnityEngine;

using GameCore.States.Branch;
using GameCore.SaveSystem;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class OptionUISaveDataState : BaseOptionUISaveDataState
    {
        public override void Enter(GameCore.States.Managers.OptionUIStateManagerData state_manager_data)
        {
            SaveManagerCore.Instance.SaveSystemDataAsync(() =>
            {
                IsActiveOff();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
    }
}
