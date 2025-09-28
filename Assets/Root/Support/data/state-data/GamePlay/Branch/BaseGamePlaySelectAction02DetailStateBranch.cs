using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlaySelectAction02DetailStateBranch : BaseGamePlaySelectActionDetailStateBranch
    {
        public override GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            if (GamePlaySelectAction_to_Conduct03(manager_data, state))
                return GamePlayStateID.Conduct03;
            if (GamePlaySelectAction_to_Date04(manager_data, state))
                return GamePlayStateID.Date04;
            if (GamePlaySelectAction_to_GoodNight05(manager_data, state))
                return GamePlayStateID.GoodNight05;
            if (GamePlaySelectAction_to_Optin07(manager_data, state))
                return GamePlayStateID.Optin07;
            if (GamePlaySelectAction_to_FinishExit11(manager_data, state))
                return GamePlayStateID.FinishExit11;
            return GamePlayStateID.None;
        }

        public override abstract bool GamePlaySelectAction_to_Conduct03(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public override abstract bool GamePlaySelectAction_to_Date04(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public override abstract bool GamePlaySelectAction_to_GoodNight05(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public override abstract bool GamePlaySelectAction_to_Optin07(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public override abstract bool GamePlaySelectAction_to_FinishExit11(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
    }
}
