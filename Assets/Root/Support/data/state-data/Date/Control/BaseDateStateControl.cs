using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseDateStateControl
        : BaseStateControl<DateStateID, DateStateManagerData, BaseDateState>
    {
        protected override DateStateID GetInitStartID()
        {
            return DateStateID.SelectGirl01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == DateStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case DateStateID.SelectGirl:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == DateStateID.None) id = state_manager_data.SaveStateID;
                    if(id == DateStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(DateStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = DateStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(DateStateID.None);
                        state_manager_data.SaveStateID = DateStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case DateStateID.Scenario:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == DateStateID.None) id = state_manager_data.SaveStateID;
                    if(id == DateStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(DateStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = DateStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(DateStateID.None);
                        state_manager_data.SaveStateID = DateStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case DateStateID.SelectGirl01:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    var next_id = DateStateID.Scenario02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(DateStateID.None);
                        state_manager_data.SaveStateID = DateStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case DateStateID.Scenario02:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseDateState FactoryState(DateStateID state_id)
        {
            switch (state_id)
            {
                case DateStateID.SelectGirl: return new DateSelectGirlState();
                case DateStateID.Scenario: return new DateScenarioState();
                case DateStateID.SelectGirl01: return new DateSelectGirlState();
                case DateStateID.Scenario02: return new DateScenarioState();
                default: return null;
            }
        }
    }
}
