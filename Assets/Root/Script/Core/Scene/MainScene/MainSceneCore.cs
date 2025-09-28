using Cysharp.Threading.Tasks;
using GameCore;
using GameCore.SaveSystem;
using GameCore.States.Control;
using UnityEngine;

public class MainSceneCore : BaseSingleton<MainSceneCore>
{
    MainLoopStateControl mainLoopCtl = new MainLoopStateControl();
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
