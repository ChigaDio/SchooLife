using UnityEngine;

using GameCore.States.Branch;
using GameCore.SaveSystem;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class GameInitPlayerSavePlayerState : BaseGameInitPlayerSavePlayerState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) 
        {
            SaveManagerCore.Instance.SavePlayerDataAsync(() =>
            {
                IsActiveOff();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}
