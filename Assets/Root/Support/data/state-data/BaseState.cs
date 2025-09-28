using GameCore.States.Managers;
using System;
namespace GameCore.States
{
    public abstract class  BaseState<E,T>where E : Enum where T : BaseStateManagerData<E>
    {
        private bool is_active = true;
        public bool IsActive => is_active;

        protected void IsActiveOff()
        {
            is_active = false;
        }

        public abstract void Enter(T state_manager_data);
        public abstract void Update(T state_manager_data);
        public abstract void Exit(T state_manager_data);
        
        public virtual E BranchNextState(T state_manager_data)
        {
            return default;
        }

    }
}
