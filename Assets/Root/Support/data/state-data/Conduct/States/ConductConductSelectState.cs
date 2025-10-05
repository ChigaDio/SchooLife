using UnityEngine;

using GameCore.States.Branch;
using System.Threading;
using GameCore.Tables.ID;
using GameCore.Enums;
using GameCore.Tables;
using GameCore.SaveSystem;
namespace GameCore.States
{
    public class ConductConductSelectState : BaseConductConductSelectState
    {
        private CancellationTokenSource cts = new CancellationTokenSource();

        private static readonly int MIN_UP = 10;
        private static readonly int MAX_UP = 30;
        private static readonly int POP_NUM = 8;
        public override void Enter(GameCore.States.Managers.ConductStateManagerData state_manager_data)
        {
            var instance = GameSceneCanvas.Instance;
            if (instance.Equals(null))
            {
                IsActiveOff();
                return;
            }

            instance.GetActionViewObjectList.PlayActionViewObject(Enums.ActionCommandID.Status, ActionAnimatorEnum.Base_Layer_Start, () =>
            {
                void CalucalculationAnimation(CharacterStatID id)
                {
                    var personalID = SaveManagerCore.Instance.PlayerProgress.personalityTableID;
                    int baseUp = Random.Range(MIN_UP, MAX_UP);
                    int resultCaluc = (int)(baseUp * PersonalityStatWeightsMatrixTable.Table[personalID][id].Risefactor);
                    var popNum = GenerateRandomSplit(resultCaluc, POP_NUM);

                    SaveManagerCore.Instance.PlayerProgress.AddPlayerStatusNum(id, resultCaluc);


                    Vector3 startPos = instance.GetActionViewObjectList.GetPosition(ActionCommandID.Status, id);
                    Vector3 endPos = instance.GetViewObjectAnimations.Find(data => data.GetID() == id).GetPosition();
                    instance.CreatehHermiteAsync(startPos, endPos,popNum,onEachArrived:(obj) =>
                    {
                        instance.GetViewObjectAnimations.Find(data => data.GetID() == id).animator.Play(StatusItemAnimationEnum.Base_Layer_StatusUp);
                        {
                            SaveManagerCore.Instance.PlayerProgress.AddPlayerStatusNum(id, obj.GetNum);
                            instance.GetViewObjectAnimations.Find(data => data.GetID() == id).SetText(SaveManagerCore.Instance.PlayerProgress.PlaterStatusNum(id).ToString());
                        }
                    },
                    onAllCompleted:()=>
                    {
                        IsActiveOff();
                    });
                }
                // 共通処理をラムダでまとめる
                void AddExecuteButton(CharacterStatID id)
                {
                    instance.GetActionViewObjectList.AddButtonClickListener(
                        Enums.ActionCommandID.Status,
                        id,
                        () =>
                        {
                            cts.Cancel();
                            cts.Dispose();
                            instance.GetActionViewObjectList.AllButtonEventClear();
                            instance.GetActionViewObjectList.PlayActionViewObject(
                                Enums.ActionCommandID.Status,
                                ActionAnimatorEnum.Base_Layer_Finish,
                                () =>
                                {

                                }
                            );
                            CalucalculationAnimation(id);
                        }
                    );

                }

                void RemoveExecuteButton(CharacterStatID id)
                {
                    instance.GetActionViewObjectList.RemoveButtonClickListener(
                        Enums.ActionCommandID.Status,
                        id
                    );
                }

                // それぞれのIDに対して登録
                AddExecuteButton(CharacterStatID.Study);
                AddExecuteButton(CharacterStatID.Stamina);
                AddExecuteButton(CharacterStatID.Appearance);


                //また自動での実行
                MainSceneCore.Instance.UpdateStudyCalculation((fillamout) =>
                {
                    GameSceneCanvas.Instance.TimeLimitBarView?.SetFillAmout(fillamout);
                },
                (id) =>
                {
                    if (cts == null) return;

                    RemoveExecuteButton(CharacterStatID.Stamina);
                    RemoveExecuteButton(CharacterStatID.Study);
                    RemoveExecuteButton(CharacterStatID.Appearance);

                    DebugLogBridge.Log(id.ToString());
                    instance.GetActionViewObjectList.PlayActionViewObject(
                        Enums.ActionCommandID.Status,
                        ActionAnimatorEnum.Base_Layer_Finish,
                        () => { }
                    );

                    CalucalculationAnimation(id);

                },
                cts.Token
                );
            });
        }
        public override void Update(GameCore.States.Managers.ConductStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.ConductStateManagerData state_manager_data) { }

        public int[] GenerateRandomSplit(int total, int parts)
        {
            int[] splits = new int[parts];
            int remaining = total;

            // 0を避けるために各部分に最低1を割り当てる（オプション）
            for (int i = 0; i < parts; i++)
            {
                splits[i] = 1;
                remaining--;
            }

            // 残りの値をランダムに分配
            for (int i = 0; i < parts - 1; i++)
            {
                int maxForThisPart = remaining - (parts - i - 1); // 他の部分に1ずつ残す
                if (maxForThisPart > 0)
                {
                    int value = UnityEngine.Random.Range(0, maxForThisPart + 1);
                    splits[i] += value;
                    remaining -= value;
                }
            }

            // 最後の部分に残りを全て割り当てる
            splits[parts - 1] += remaining;

            // 結果をシャッフル
            for (int i = 0; i < parts; i++)
            {
                int j = UnityEngine.Random.Range(i, parts);
                (splits[i], splits[j]) = (splits[j], splits[i]);
            }

            return splits;
        }
    }
}
