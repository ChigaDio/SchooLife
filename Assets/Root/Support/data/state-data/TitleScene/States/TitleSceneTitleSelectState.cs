using UnityEngine;

using GameCore.States.Branch;
using GameCore.Sound;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class TitleSceneTitleSelectState : BaseTitleSceneTitleSelectState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            state_manager_data.selectTitle = TitleCanvas.SelectTile.None;
            TitleCanvas.Instance.AddListenerButton(TitleCanvas.SelectTile.GameStart, () =>
            {
                if (state_manager_data.selectTitle != TitleCanvas.SelectTile.None) return;
                state_manager_data.selectTitle = TitleCanvas.SelectTile.GameStart;
                SoundCore.Instance.PlaySEAsync(SoundGroup.UI, SoundID.UI_PushEnter).Forget();
                IsActiveOff();
            });
            TitleCanvas.Instance.AddListenerButton(TitleCanvas.SelectTile.Exit, () =>
            {
                if (state_manager_data.selectTitle != TitleCanvas.SelectTile.None) return;
                state_manager_data.selectTitle = TitleCanvas.SelectTile.Exit;
                SoundCore.Instance.PlaySEAsync(SoundGroup.UI, SoundID.UI_PushEnter).Forget();
                IsActiveOff();
            });
            TitleCanvas.Instance.AddListenerButton(TitleCanvas.SelectTile.Credit, () =>
            {
                if (state_manager_data.selectTitle != TitleCanvas.SelectTile.None) return;
                state_manager_data.selectTitle = TitleCanvas.SelectTile.Credit;
                SoundCore.Instance.PlaySEAsync(SoundGroup.UI, SoundID.UI_PushEnter).Forget();
                IsActiveOff();
            });
            TitleCanvas.Instance.AddListenerButton(TitleCanvas.SelectTile.Setting, () =>
            {
                if (state_manager_data.selectTitle != TitleCanvas.SelectTile.None) return;
                state_manager_data.selectTitle = TitleCanvas.SelectTile.Setting;
                SoundCore.Instance.PlaySEAsync(SoundGroup.UI, SoundID.UI_PushEnter).Forget();
                IsActiveOff();
            });
        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override GameCore.States.ID.TitleSceneStateID BranchNextState(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data)
        {
            var branch = new TitleSceneTitleSelectStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}
