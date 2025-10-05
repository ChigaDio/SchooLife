using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseConductStateControl
        : BaseStateControl<ConductStateID, ConductStateManagerData, BaseConductState>
    {
        protected override ConductStateID GetInitStartID()
        {
            return ConductStateID.ConductSelect01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == ConductStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case ConductStateID.ConductSelect:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == ConductStateID.None) id = state_manager_data.SaveStateID;
                    if(id == ConductStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(ConductStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = ConductStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(ConductStateID.None);
                        state_manager_data.SaveStateID = ConductStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case ConductStateID.CalculationAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == ConductStateID.None) id = state_manager_data.SaveStateID;
                    if(id == ConductStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(ConductStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = ConductStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(ConductStateID.None);
                        state_manager_data.SaveStateID = ConductStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case ConductStateID.ConductSelect01:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    var next_id = ConductStateID.CalculationAnimation02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(ConductStateID.None);
                        state_manager_data.SaveStateID = ConductStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case ConductStateID.CalculationAnimation02:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseConductState FactoryState(ConductStateID state_id)
        {
            switch (state_id)
            {
                case ConductStateID.ConductSelect: return new ConductConductSelectState();
                case ConductStateID.CalculationAnimation: return new ConductCalculationAnimationState();
                case ConductStateID.ConductSelect01: return new ConductConductSelectState();
                case ConductStateID.CalculationAnimation02: return new ConductCalculationAnimationState();
                default: return null;
            }
        }
    }
}
