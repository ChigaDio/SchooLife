using System;
using System.Collections.Generic;
namespace GameCore.States.Control
{
    public abstract class BaseStateControl<T, E, F>
        where T : Enum
        where E : GameCore.States.Managers.BaseStateManagerData<T>,new()
        where F : GameCore.States.BaseState<T,E>
    {

        protected E state_manager_data = new E();
        public E StateManagerData{get { return state_manager_data; }}

        protected F state;

        protected bool is_finish = false;
        public bool IsFinish { get { return is_finish; } }

        public void StartState(Action<E> action)
        {
            OnStartState(GetInitStartID(), action);
        }
        public void StartState(T state_id)
        {
            OnStartState(state_id, null);
        }
        public void StartState()
        {
            OnStartState(GetInitStartID(), null);
        }

        protected abstract T GetInitStartID();
        protected  void OnStartState(
    T state_id,
    Action<E> action)
        {
            state = FactoryState(state_id);
            state_manager_data.ChangeStateNowID(state_id);
            action?.Invoke(state_manager_data);
            state.Enter(state_manager_data);
        }

        public void UpdateState(Action<E> befor_action = null, Action<E> after_action = null)
        {
            if (state == null) StartState();
            OnUpdateState(befor_action, after_action);
        }

        protected void OnUpdateState(Action<E> befor_action = null, Action<E> after_action = null)
        {
            befor_action?.Invoke(state_manager_data);
            state.Update(state_manager_data);
            BranchState();
            after_action?.Invoke(state_manager_data);
        }

        public abstract void BranchState();
        
        public abstract F FactoryState(T state_id);

    }
}
