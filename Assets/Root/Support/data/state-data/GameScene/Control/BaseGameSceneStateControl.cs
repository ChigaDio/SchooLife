using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseGameSceneStateControl
        : BaseStateControl<GameSceneStateID, GameSceneStateManagerData, BaseGameSceneState>
    {
        protected override GameSceneStateID GetInitStartID()
        {
            return GameSceneStateID.LoadAssets01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == default) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case GameSceneStateID.LoadAssets:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.SwitchGameLoad:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.InitGame:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.LoadSaveData:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.GameLoop:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.UnLoadAssets:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.LoadAssets01:
                {
                    state.Exit(state_manager_data);
                    var next_id = GameSceneStateID.SwitchGameLoad02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.SwitchGameLoad02:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == GameSceneStateID.None)
                    {
                        is_finish = true;
                        return;
                    }
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.InitGame03:
                {
                    state.Exit(state_manager_data);
                    var next_id = GameSceneStateID.GameLoop05;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.LoadSaveData04:
                {
                    state.Exit(state_manager_data);
                    var next_id = GameSceneStateID.GameLoop05;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.GameLoop05:
                {
                    state.Exit(state_manager_data);
                    var next_id = GameSceneStateID.UnLoadAssets06;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameSceneStateID.UnLoadAssets06:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseGameSceneState FactoryState(GameSceneStateID state_id)
        {
            switch (state_id)
            {
                case GameSceneStateID.LoadAssets: return new GameSceneLoadAssetsState();
                case GameSceneStateID.SwitchGameLoad: return new GameSceneSwitchGameLoadState();
                case GameSceneStateID.InitGame: return new GameSceneInitGameState();
                case GameSceneStateID.LoadSaveData: return new GameSceneLoadSaveDataState();
                case GameSceneStateID.GameLoop: return new GameSceneGameLoopState();
                case GameSceneStateID.UnLoadAssets: return new GameSceneUnLoadAssetsState();
                case GameSceneStateID.LoadAssets01: return new GameSceneLoadAssetsState();
                case GameSceneStateID.SwitchGameLoad02: return new GameSceneSwitchGameLoadState();
                case GameSceneStateID.InitGame03: return new GameSceneInitGameState();
                case GameSceneStateID.LoadSaveData04: return new GameSceneLoadSaveDataState();
                case GameSceneStateID.GameLoop05: return new GameSceneGameLoopState();
                case GameSceneStateID.UnLoadAssets06: return new GameSceneUnLoadAssetsState();
                default: return null;
            }
        }
    }
}
