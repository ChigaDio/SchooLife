using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseMainLoopStateControl
        : BaseStateControl<MainLoopStateID, MainLoopStateManagerData, BaseMainLoopState>
    {
        protected override MainLoopStateID GetInitStartID()
        {
            return MainLoopStateID.Title01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == MainLoopStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case MainLoopStateID.Title:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == MainLoopStateID.None) id = state_manager_data.SaveStateID;
                    if(id == MainLoopStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case MainLoopStateID.Game:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == MainLoopStateID.None) id = state_manager_data.SaveStateID;
                    if(id == MainLoopStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case MainLoopStateID.Exit:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == MainLoopStateID.None) id = state_manager_data.SaveStateID;
                    if(id == MainLoopStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case MainLoopStateID.Title01:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == MainLoopStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case MainLoopStateID.Game02:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == MainLoopStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(MainLoopStateID.None);
                        state_manager_data.SaveStateID = MainLoopStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case MainLoopStateID.Exit03:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseMainLoopState FactoryState(MainLoopStateID state_id)
        {
            switch (state_id)
            {
                case MainLoopStateID.Title: return new MainLoopTitleState();
                case MainLoopStateID.Game: return new MainLoopGameState();
                case MainLoopStateID.Exit: return new MainLoopExitState();
                case MainLoopStateID.Title01: return new MainLoopTitleState();
                case MainLoopStateID.Game02: return new MainLoopGameState();
                case MainLoopStateID.Exit03: return new MainLoopExitState();
                default: return null;
            }
        }
    }
}
