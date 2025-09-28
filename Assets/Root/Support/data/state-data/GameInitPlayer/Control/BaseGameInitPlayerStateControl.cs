using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseGameInitPlayerStateControl
        : BaseStateControl<GameInitPlayerStateID, GameInitPlayerStateManagerData, BaseGameInitPlayerState>
    {
        protected override GameInitPlayerStateID GetInitStartID()
        {
            return GameInitPlayerStateID.LoadAssets01;
        }

        public override void BranchState()
        {
            if (state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == GameInitPlayerStateID.None) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case GameInitPlayerStateID.LoadAssets:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.FadeIn:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.StartAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.InputCheck:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.InputFinishAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.SelectItemStartAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.SelectFinishCheck:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.SelectItemFinishAnimation:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.FadeOut:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.SavePlayer:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.UnloadAssets:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    id = state_manager_data.PopStateID();
                    if(id == GameInitPlayerStateID.None) id = state_manager_data.SaveStateID;
                    if(id == GameInitPlayerStateID.None)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        is_finish = true;
                        return;
                    }
                    else
                    {
                        state_manager_data.ChangeStateNowID(id);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    }
                    state = FactoryState(id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.LoadAssets01:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    var next_id = GameInitPlayerStateID.StartAnimation02;
                    state_manager_data.SaveStateID = next_id;
                    state_manager_data.PushStateID(GameInitPlayerStateID.FadeIn);
                    state_manager_data.PushStateID(next_id);
                    next_id = state_manager_data.PopStateID();
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.StartAnimation02:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    var next_id = GameInitPlayerStateID.SelectItemStartAnimation03;
                    state_manager_data.SaveStateID = next_id;
                    state_manager_data.PushStateID(GameInitPlayerStateID.InputCheck);
                    state_manager_data.PushStateID(GameInitPlayerStateID.InputFinishAnimation);
                    state_manager_data.PushStateID(next_id);
                    next_id = state_manager_data.PopStateID();
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.SelectItemStartAnimation03:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    var next_id = GameInitPlayerStateID.FadeOut04;
                    state_manager_data.SaveStateID = next_id;
                    state_manager_data.PushStateID(GameInitPlayerStateID.SelectFinishCheck);
                    state_manager_data.PushStateID(GameInitPlayerStateID.SelectItemFinishAnimation);
                    state_manager_data.PushStateID(next_id);
                    next_id = state_manager_data.PopStateID();
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case GameInitPlayerStateID.FadeOut04:
                {
                    state.Exit(state_manager_data);
                    state_manager_data.PopUpStateID();
                    state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                    state_manager_data.PushStateID(GameInitPlayerStateID.SavePlayer);
                    state_manager_data.PushStateID(GameInitPlayerStateID.UnloadAssets);
                    var next_id = state_manager_data.PopStateID();
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        state_manager_data.ChangeStateNowID(GameInitPlayerStateID.None);
                        state_manager_data.SaveStateID = GameInitPlayerStateID.None;
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
            }
        }

        public override BaseGameInitPlayerState FactoryState(GameInitPlayerStateID state_id)
        {
            switch (state_id)
            {
                case GameInitPlayerStateID.LoadAssets: return new GameInitPlayerLoadAssetsState();
                case GameInitPlayerStateID.FadeIn: return new GameInitPlayerFadeInState();
                case GameInitPlayerStateID.StartAnimation: return new GameInitPlayerStartAnimationState();
                case GameInitPlayerStateID.InputCheck: return new GameInitPlayerInputCheckState();
                case GameInitPlayerStateID.InputFinishAnimation: return new GameInitPlayerInputFinishAnimationState();
                case GameInitPlayerStateID.SelectItemStartAnimation: return new GameInitPlayerSelectItemStartAnimationState();
                case GameInitPlayerStateID.SelectFinishCheck: return new GameInitPlayerSelectFinishCheckState();
                case GameInitPlayerStateID.SelectItemFinishAnimation: return new GameInitPlayerSelectItemFinishAnimationState();
                case GameInitPlayerStateID.FadeOut: return new GameInitPlayerFadeOutState();
                case GameInitPlayerStateID.SavePlayer: return new GameInitPlayerSavePlayerState();
                case GameInitPlayerStateID.UnloadAssets: return new GameInitPlayerUnloadAssetsState();
                case GameInitPlayerStateID.LoadAssets01: return new GameInitPlayerLoadAssetsState();
                case GameInitPlayerStateID.StartAnimation02: return new GameInitPlayerStartAnimationState();
                case GameInitPlayerStateID.SelectItemStartAnimation03: return new GameInitPlayerSelectItemStartAnimationState();
                case GameInitPlayerStateID.FadeOut04: return new GameInitPlayerFadeOutState();
                default: return null;
            }
        }
    }
}
