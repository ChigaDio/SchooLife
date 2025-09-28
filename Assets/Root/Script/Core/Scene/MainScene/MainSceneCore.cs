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
            //終了ならゲーム終了
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
