using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlaySelectActionDetailStateBranch : BaseGamePlayDetailStateBranch<GamePlaySelectActionState>
    {
        public override abstract GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public abstract bool GamePlaySelectAction_to_Conduct03(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public abstract bool GamePlaySelectAction_to_Date04(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public abstract bool GamePlaySelectAction_to_GoodNight05(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public abstract bool GamePlaySelectAction_to_Optin07(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
        public abstract bool GamePlaySelectAction_to_FinishExit11(GamePlayStateManagerData manager_data, GamePlaySelectActionState state);
    }
}
