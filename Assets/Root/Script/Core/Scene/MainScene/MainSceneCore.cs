using Cysharp.Threading.Tasks;
using GameCore;
using GameCore.SaveSystem;
using GameCore.States.Control;
using System.Threading;
using System;
using UnityEngine;
using GameCore.Enums;
using GameCore.Tables.ID;

public class MainSceneCore : BaseSingleton<MainSceneCore>
{
    MainLoopStateControl mainLoopCtl = new MainLoopStateControl();

    TimeLimitSystem timeLimitSystem = new TimeLimitSystem();

    public void UpdateStudyCalculation(Action<float> action, Action<CharacterStatID> OnComplete, CancellationToken cancellationToken = default)
    {
        instance?.timeLimitSystem.UpdateCalculationStudyCommand(action, OnComplete, SaveManagerCore.instance.PlayerProgress, cancellationToken).Forget();
    }

    public void UpdateActionCalculation(Action<float> action, Action<ActionExecuteCommandTableID> OnComplete, CancellationToken cancellationToken = default)
    {
        instance?.timeLimitSystem.UpdateCalculationActionCommand(action, OnComplete, SaveManagerCore.instance.PlayerProgress, cancellationToken).Forget();
    }


    public override void AwakeSingleton()
    {
        mainLoopCtl.StartState();
        base.AwakeSingleton();
        DontDestroyOnLoad(this);
        
    }

    public void Start()
    {
        SaveManagerCore.instance.LoadAllDataAsync().Forget();
    }

    public void Update()
    {
        mainLoopCtl.UpdateState();
        if(mainLoopCtl.IsFinish)
        {
            //�I���Ȃ�Q�[���I��
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
