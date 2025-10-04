using UnityEngine;

using GameCore.States.Branch;
using GameCore.Tables.ID;
using GameCore.Enums;
namespace GameCore.States
{
    public class GamePlaySelectActionState : BaseGamePlaySelectActionState
    {
        public override void Enter(GameCore.States.Managers.GamePlayStateManagerData state_manager_data)
        {
            var instance = GameSceneCanvas.Instance;
            if (instance.Equals(null))
            {
                IsActiveOff();
                return;
            }

            instance.GetActionViewObjectList.PlayActionViewObject(Enums.ActionCommandID.Execute, ActionAnimatorEnum.Base_Layer_Start, () =>
            {
                // ‹¤’Êˆ—‚ðƒ‰ƒ€ƒ_‚Å‚Ü‚Æ‚ß‚é
                void AddExecuteButton(ActionExecuteCommandTableID id)
                {
                    instance.GetActionViewObjectList.AddButtonClickListener(
                        Enums.ActionCommandID.Execute,
                        id,
                        () =>
                        {
                            instance.GetActionViewObjectList.AllButtonEventClear();
                            state_manager_data.actionExecuteID = id;
                            instance.GetActionViewObjectList.PlayActionViewObject(
                                Enums.ActionCommandID.Execute,
                                ActionAnimatorEnum.Base_Layer_Finish,
                                () => IsActiveOff()
                            );

                            var start_pos = instance.GetActionViewObjectList.GetPosition(Enums.ActionCommandID.Execute, ActionExecuteCommandTableID.Study);
                            var end_pos = instance.GetViewObjectAnimations.Find(data => data.GetID().Equals(CharacterStatID.Study)).GetPosition();

                            instance.CreatehHermiteAsync(start_pos, end_pos, new string[] {"10","22","5","17","9","34","25" });
                        }
                    );
                }

                // ‚»‚ê‚¼‚ê‚ÌID‚É‘Î‚µ‚Ä“o˜^
                AddExecuteButton(ActionExecuteCommandTableID.Study);
                AddExecuteButton(ActionExecuteCommandTableID.Date);
                AddExecuteButton(ActionExecuteCommandTableID.Rest);
                AddExecuteButton(ActionExecuteCommandTableID.Option);
                AddExecuteButton(ActionExecuteCommandTableID.Finish);
            });



        }
        public override void Update(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) 
        {

        }
        public override GameCore.States.ID.GamePlayStateID BranchNextState(GameCore.States.Managers.GamePlayStateManagerData state_manager_data)
        {
            var branch = new GamePlaySelectActionStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}
