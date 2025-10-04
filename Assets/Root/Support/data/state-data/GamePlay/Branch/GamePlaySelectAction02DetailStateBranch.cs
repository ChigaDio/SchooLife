using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GamePlaySelectAction02DetailStateBranch : BaseGamePlaySelectAction02DetailStateBranch
    {
        public override bool GamePlaySelectAction_to_Conduct03(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            return manager_data.actionExecuteID == Tables.ID.ActionExecuteCommandTableID.Study;
        }

        public override bool GamePlaySelectAction_to_Date04(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            return manager_data.actionExecuteID == Tables.ID.ActionExecuteCommandTableID.Date;
        }

        public override bool GamePlaySelectAction_to_GoodNight05(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            return manager_data.actionExecuteID == Tables.ID.ActionExecuteCommandTableID.Rest;
        }

        public override bool GamePlaySelectAction_to_Optin07(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            return manager_data.actionExecuteID == Tables.ID.ActionExecuteCommandTableID.Option;
        }

        public override bool GamePlaySelectAction_to_FinishExit11(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            return manager_data.actionExecuteID == Tables.ID.ActionExecuteCommandTableID.Finish;
        }

    }
}
