using UnityEngine;

using GameCore.States.Branch;
using GameCore.SaveSystem;
using GameCore.Tables.ID;
namespace GameCore.States
{
    public class GameInitPlayerInputCheckState : BaseGameInitPlayerInputCheckState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) 
        {
            PlayerInitCanvas.Instance.FinishButtonInputAction(() =>
            {
                var input = PlayerInitCanvas.Instance.GetInputData;
                if (input == null) return;

             
                if (string.IsNullOrEmpty(input.familyInput.text)) return;
                if(string.IsNullOrEmpty(input.firstInput.text)) return;    
                if(input.personalityDown.value < 0) return;

                SaveManagerCore.Instance.PlayerProgress.familyName = input.familyInput.text;
                SaveManagerCore.Instance.PlayerProgress.firstName = input.firstInput.text;
                SaveManagerCore.Instance.PlayerProgress.personalityTableID = (PersonalityTableID)input.personalityDown.value + 1;

                IsActiveOff();
            });
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}
