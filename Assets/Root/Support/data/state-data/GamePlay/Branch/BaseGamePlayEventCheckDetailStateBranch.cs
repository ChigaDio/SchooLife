using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlayEventCheckDetailStateBranch : BaseGamePlayDetailStateBranch<GamePlayEventCheckState>
    {
        public override abstract GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlayEventCheckState state);
        public abstract bool GamePlayEventCheck_to_Event08(GamePlayStateManagerData manager_data, GamePlayEventCheckState state);
        public abstract bool GamePlayEventCheck_to_Save09(GamePlayStateManagerData manager_data, GamePlayEventCheckState state);
    }
}
