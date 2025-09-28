using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseOptionUIStateControl
        : BaseStateControl<OptionUIStateID, OptionUIStateManagerData, BaseOptionUIState>
    {
        protected override OptionUIStateID GetInitStartID()
        {
            return OptionUIStateID.FadeIn01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == OptionUIStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case OptionUIStateID.FadeIn:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == OptionUIStateID.None) id = state_manager_data.SaveStateID;
                    if(id == OptionUIStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.StartAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == OptionUIStateID.None) id = state_manager_data.SaveStateID;
                    if(id == OptionUIStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.ExitCheck:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == OptionUIStateID.None) id = state_manager_data.SaveStateID;
                    if(id == OptionUIStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.FinishAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == OptionUIStateID.None) id = state_manager_data.SaveStateID;
                    if(id == OptionUIStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.SaveData:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == OptionUIStateID.None) id = state_manager_data.SaveStateID;
                    if(id == OptionUIStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.FadeOut:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == OptionUIStateID.None) id = state_manager_data.SaveStateID;
                    if(id == OptionUIStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.FadeIn01:
                {
                    state.Exit(state_manager_data);
                    var next_id = OptionUIStateID.StartAnimation02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.StartAnimation02:
                {
                    state.Exit(state_manager_data);
                    var next_id = OptionUIStateID.ExitCheck03;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.ExitCheck03:
                {
                    state.Exit(state_manager_data);
                    var next_id = OptionUIStateID.FinishAnimation04;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.FinishAnimation04:
                {
                    state.Exit(state_manager_data);
                    var next_id = OptionUIStateID.SaveData05;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.SaveData05:
                {
                    state.Exit(state_manager_data);
                    var next_id = OptionUIStateID.FadeOut06;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(OptionUIStateID.None);
                        state_manager_data.SaveStateID = OptionUIStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case OptionUIStateID.FadeOut06:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseOptionUIState FactoryState(OptionUIStateID state_id)
        {
            switch (state_id)
            {
                case OptionUIStateID.FadeIn: return new OptionUIFadeInState();
                case OptionUIStateID.StartAnimation: return new OptionUIStartAnimationState();
                case OptionUIStateID.ExitCheck: return new OptionUIExitCheckState();
                case OptionUIStateID.FinishAnimation: return new OptionUIFinishAnimationState();
                case OptionUIStateID.SaveData: return new OptionUISaveDataState();
                case OptionUIStateID.FadeOut: return new OptionUIFadeOutState();
                case OptionUIStateID.FadeIn01: return new OptionUIFadeInState();
                case OptionUIStateID.StartAnimation02: return new OptionUIStartAnimationState();
                case OptionUIStateID.ExitCheck03: return new OptionUIExitCheckState();
                case OptionUIStateID.FinishAnimation04: return new OptionUIFinishAnimationState();
                case OptionUIStateID.SaveData05: return new OptionUISaveDataState();
                case OptionUIStateID.FadeOut06: return new OptionUIFadeOutState();
                default: return null;
            }
        }
    }
}
