using UnityEngine;

using GameCore.States.Branch;
using GameCore.Tables;
using Cysharp.Threading.Tasks;
using GameCore.Tables.ID;

namespace GameCore.States
{
    public class GameSceneLoadAssetsState : BaseGameSceneLoadAssetsState
    {
        
        public override void Enter(GameCore.States.Managers.GameSceneStateManagerData state_manager_data)
        {
            //ここでデータをロード
            ClassDataIDCore.Instance.LoadClassDataAsync((reader, header) =>
            {
                header.GetData<PersonalityTable>(Enums.TableID.Personality, reader);
                header.GetData<CharacterTable>(Enums.TableID.Character, reader);
                header.GetData<ActionExecuteCommandTable>(Enums.TableID.ActionExecuteCommand, reader);
                header.GetData<TurnEventTable>(Enums.TableID.TurnEvent, reader);
                header.GetData<TurnTable>(Enums.TableID.Turn, reader);
                //マトリックスデータをロード
                ClassDataMatrixIDCore.Instance.LoadClassDataAsync((reader, header) =>
                {
                    header.GetData<ItemSelectMatrixTable>(MatrixTableID.ItemSelect, reader);
                    header.GetData<PersonalityStatWeightsMatrixTable>(MatrixTableID.PersonalityStatWeights, reader);
                    header.GetData<PersonalityActionExecuteWeightsMatrixTable>(MatrixTableID.PersonalityActionExecuteWeights,reader);
                    IsActiveOff();
                    return UniTask.CompletedTask;
                }).Forget();
                return UniTask.CompletedTask;
            }).Forget();

        }
        public override void Update(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) { }
    }
}
