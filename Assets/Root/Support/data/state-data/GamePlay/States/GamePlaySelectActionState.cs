using UnityEngine;

using GameCore.States.Branch;
using GameCore.Tables.ID;
using GameCore.Enums;
using System.Threading;
namespace GameCore.States
{
    public class GamePlaySelectActionState : BaseGamePlaySelectActionState
    {

        private CancellationTokenSource cts = new CancellationTokenSource();
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
                // ���ʏ����������_�ł܂Ƃ߂�
                void AddExecuteButton(ActionExecuteCommandTableID id)
                {
                    instance.GetActionViewObjectList.AddButtonClickListener(
                        Enums.ActionCommandID.Execute,
                        id,
                        () =>
                        {
                            cts.Cancel();
                            cts.Dispose();
                            instance.GetActionViewObjectList.AllButtonEventClear();
                            state_manager_data.actionExecuteID = id;
                            instance.GetActionViewObjectList.PlayActionViewObject(
                                Enums.ActionCommandID.Execute,
                                ActionAnimatorEnum.Base_Layer_Finish,
                                () => IsActiveOff()
                            );
                        }
                    );
                }

                void RemoveExecuteButton(ActionExecuteCommandTableID id)
                {
                    instance.GetActionViewObjectList.RemoveButtonClickListener(
                        Enums.ActionCommandID.Execute,
                        id
                    );
                }

                // ���ꂼ���ID�ɑ΂��ēo�^
                AddExecuteButton(ActionExecuteCommandTableID.Study);
                AddExecuteButton(ActionExecuteCommandTableID.Date);
                AddExecuteButton(ActionExecuteCommandTableID.Rest);
                AddExecuteButton(ActionExecuteCommandTableID.Option);
                AddExecuteButton(ActionExecuteCommandTableID.Finish);


                //�܂������ł̎��s
                MainSceneCore.Instance.UpdateActionCalculation((fillamout) =>
                {
                    GameSceneCanvas.Instance.TimeLimitBarView?.SetFillAmout( fillamout );
                },
                (id) =>
                {
                    if (cts == null) return;

                    RemoveExecuteButton(ActionExecuteCommandTableID.Option);
                    RemoveExecuteButton(ActionExecuteCommandTableID.Finish);
                    RemoveExecuteButton(ActionExecuteCommandTableID.Rest);
                    RemoveExecuteButton(ActionExecuteCommandTableID.Date);
                    RemoveExecuteButton(ActionExecuteCommandTableID.Study);

                    state_manager_data.actionExecuteID = id;
                    DebugLogBridge.Log(id.ToString());
                    instance.GetActionViewObjectList.PlayActionViewObject(
                        Enums.ActionCommandID.Execute,
                        ActionAnimatorEnum.Base_Layer_Finish,
                        () => IsActiveOff()
                    );


                },
                cts.Token
                );
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
