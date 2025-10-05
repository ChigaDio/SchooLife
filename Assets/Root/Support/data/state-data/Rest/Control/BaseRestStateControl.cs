using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseRestStateControl
        : BaseStateControl<RestStateID, RestStateManagerData, BaseRestState>
    {
        protected override RestStateID GetInitStartID()
        {
            return RestStateID.RestAnimation01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == RestStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case RestStateID.RestAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == RestStateID.None) id = state_manager_data.SaveStateID;
                    if(id == RestStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(RestStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = RestStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(RestStateID.None);
                        state_manager_data.SaveStateID = RestStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case RestStateID.RestAnimation01:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseRestState FactoryState(RestStateID state_id)
        {
            switch (state_id)
            {
                case RestStateID.RestAnimation: return new RestRestAnimationState();
                case RestStateID.RestAnimation01: return new RestRestAnimationState();
                default: return null;
            }
        }
    }
}
