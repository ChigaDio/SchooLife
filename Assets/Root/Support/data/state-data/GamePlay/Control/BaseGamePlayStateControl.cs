using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseGamePlayStateControl
        : BaseStateControl<GamePlayStateID, GamePlayStateManagerData, BaseGamePlayState>
    {
        protected override GamePlayStateID GetInitStartID()
        {
            return GamePlayStateID.FadeIn01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == default) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case GamePlayStateID.FadeIn:
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
                case GamePlayStateID.SelectAction:
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
                case GamePlayStateID.Conduct:
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
                case GamePlayStateID.Date:
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
                case GamePlayStateID.GoodNight:
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
                case GamePlayStateID.EventCheck:
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
                case GamePlayStateID.Optin:
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
                case GamePlayStateID.Event:
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
                case GamePlayStateID.Save:
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
                case GamePlayStateID.FinishCheck:
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
                case GamePlayStateID.FinishExit:
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
                case GamePlayStateID.FadeIn01:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.SelectAction02;
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
                case GamePlayStateID.SelectAction02:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == GamePlayStateID.None)
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
                case GamePlayStateID.Conduct03:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.EventCheck06;
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
                case GamePlayStateID.Date04:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.EventCheck06;
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
                case GamePlayStateID.GoodNight05:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.EventCheck06;
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
                case GamePlayStateID.EventCheck06:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == GamePlayStateID.None)
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
                case GamePlayStateID.Optin07:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.SelectAction02;
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
                case GamePlayStateID.Event08:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.Save09;
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
                case GamePlayStateID.Save09:
                {
                    state.Exit(state_manager_data);
                    var next_id = GamePlayStateID.FinishCheck10;
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
                case GamePlayStateID.FinishCheck10:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    state_manager_data.PushStateID(GamePlayStateID.FadeOut);
                    next_id = state_manager_data.PopStateID();
                    if (next_id == GamePlayStateID.None)
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
                case GamePlayStateID.FinishExit11:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseGamePlayState FactoryState(GamePlayStateID state_id)
        {
            switch (state_id)
            {
                case GamePlayStateID.FadeIn: return new GamePlayFadeInState();
                case GamePlayStateID.SelectAction: return new GamePlaySelectActionState();
                case GamePlayStateID.Conduct: return new GamePlayConductState();
                case GamePlayStateID.Date: return new GamePlayDateState();
                case GamePlayStateID.GoodNight: return new GamePlayGoodNightState();
                case GamePlayStateID.EventCheck: return new GamePlayEventCheckState();
                case GamePlayStateID.Optin: return new GamePlayOptinState();
                case GamePlayStateID.Event: return new GamePlayEventState();
                case GamePlayStateID.Save: return new GamePlaySaveState();
                case GamePlayStateID.FinishCheck: return new GamePlayFinishCheckState();
                case GamePlayStateID.FinishExit: return new GamePlayFinishExitState();
                case GamePlayStateID.FadeIn01: return new GamePlayFadeInState();
                case GamePlayStateID.SelectAction02: return new GamePlaySelectActionState();
                case GamePlayStateID.Conduct03: return new GamePlayConductState();
                case GamePlayStateID.Date04: return new GamePlayDateState();
                case GamePlayStateID.GoodNight05: return new GamePlayGoodNightState();
                case GamePlayStateID.EventCheck06: return new GamePlayEventCheckState();
                case GamePlayStateID.Optin07: return new GamePlayOptinState();
                case GamePlayStateID.Event08: return new GamePlayEventState();
                case GamePlayStateID.Save09: return new GamePlaySaveState();
                case GamePlayStateID.FinishCheck10: return new GamePlayFinishCheckState();
                case GamePlayStateID.FinishExit11: return new GamePlayFinishExitState();
                default: return null;
            }
        }
    }
}
