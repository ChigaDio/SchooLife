using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseTitleSceneStateControl
        : BaseStateControl<TitleSceneStateID, TitleSceneStateManagerData, BaseTitleSceneState>
    {
        protected override TitleSceneStateID GetInitStartID()
        {
            return TitleSceneStateID.TitleStartAnimation01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == TitleSceneStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case TitleSceneStateID.TitleStartAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.TitleSelect:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.GameStart:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.Option:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.Credit:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.FadeOut:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.LoadAssets:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.FadeIn:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.UnLoadAssets:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.Exit:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == TitleSceneStateID.None) id = state_manager_data.SaveStateID;
                    if(id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.TitleStartAnimation01:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.TitleSelect02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.TitleSelect02:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == TitleSceneStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.GameStart03:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.FadeOut06;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.Option04:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.TitleSelect02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.Credit05:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.TitleSelect02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.FadeOut06:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.UnLoadAssets09;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.LoadAssets07:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.FadeIn08;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.FadeIn08:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleSceneStateID.TitleStartAnimation01;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleSceneStateID.UnLoadAssets09:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
                case TitleSceneStateID.Exit10:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.SaveStateID = TitleSceneStateID.None;
                    state_manager_data.PushStateID(TitleSceneStateID.FadeOut);
                    var next_id = state_manager_data.PopStateID();
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(TitleSceneStateID.None);
                        state_manager_data.SaveStateID = TitleSceneStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
            }
        }

        public override BaseTitleSceneState FactoryState(TitleSceneStateID state_id)
        {
            switch (state_id)
            {
                case TitleSceneStateID.TitleStartAnimation: return new TitleSceneTitleStartAnimationState();
                case TitleSceneStateID.TitleSelect: return new TitleSceneTitleSelectState();
                case TitleSceneStateID.GameStart: return new TitleSceneGameStartState();
                case TitleSceneStateID.Option: return new TitleSceneOptionState();
                case TitleSceneStateID.Credit: return new TitleSceneCreditState();
                case TitleSceneStateID.FadeOut: return new TitleSceneFadeOutState();
                case TitleSceneStateID.LoadAssets: return new TitleSceneLoadAssetsState();
                case TitleSceneStateID.FadeIn: return new TitleSceneFadeInState();
                case TitleSceneStateID.UnLoadAssets: return new TitleSceneUnLoadAssetsState();
                case TitleSceneStateID.Exit: return new TitleSceneExitState();
                case TitleSceneStateID.TitleStartAnimation01: return new TitleSceneTitleStartAnimationState();
                case TitleSceneStateID.TitleSelect02: return new TitleSceneTitleSelectState();
                case TitleSceneStateID.GameStart03: return new TitleSceneGameStartState();
                case TitleSceneStateID.Option04: return new TitleSceneOptionState();
                case TitleSceneStateID.Credit05: return new TitleSceneCreditState();
                case TitleSceneStateID.FadeOut06: return new TitleSceneFadeOutState();
                case TitleSceneStateID.LoadAssets07: return new TitleSceneLoadAssetsState();
                case TitleSceneStateID.FadeIn08: return new TitleSceneFadeInState();
                case TitleSceneStateID.UnLoadAssets09: return new TitleSceneUnLoadAssetsState();
                case TitleSceneStateID.Exit10: return new TitleSceneExitState();
                default: return null;
            }
        }
    }
}
