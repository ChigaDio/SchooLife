using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
using GameCore.Sound;
using Cysharp.Threading.Tasks;
using AddressableSystem;
namespace GameCore.States
{
    public class MainLoopTitleState : BaseMainLoopTitleState
    {
        private TitleSceneStateControl state = new TitleSceneStateControl();
        private bool isLoadScene = false;
        public override void Enter(GameCore.States.Managers.MainLoopStateManagerData state_manager_data)
        {
            SoundCore.Instance.LoadGroupAsync(SoundGroup.UI, GroupCategory.Game,action: () =>
            {
                state.StartState(ID.TitleSceneStateID.LoadAssets07);
                isLoadScene = true;
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.MainLoopStateManagerData state_manager_data)
        {
            if (isLoadScene == false) return;
            state.UpdateState();
            if(state.IsFinish)
            {
                IsActiveOff();
                if(state.StateManagerData.IsExit()) state_manager_data.OnExit();
            }
        }
        public override void Exit(GameCore.States.Managers.MainLoopStateManagerData state_manager_data) { isLoadScene = false; }
        public override GameCore.States.ID.MainLoopStateID BranchNextState(GameCore.States.Managers.MainLoopStateManagerData state_manager_data)
        {
            var branch = new MainLoopTitleStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}
