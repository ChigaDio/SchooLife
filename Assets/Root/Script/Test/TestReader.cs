using Cysharp.Threading.Tasks;
using GameCore;
using GameCore.Scenario;
using System.IO;
using System.Text;
using UnityEngine;

public class TestReader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScenarioEventBinaryHeader.ReadHeaderAsync(async () =>
        {
            DebugLogBridge.Log("シナリオのイベントバイナリ読み込み");

            var eventData = ScenarioEventBinaryHeader.Events.Find(data => data.EventId == "Z-99999");
            var subEventData = eventData.SubEvents.Find(data => data.SubEventId == 1);
            long seek = subEventData.SubEventOffset;

            ScenarioCanvas.Instance.FadeTalkFrameAlpha(1.0f);
            await UniTask.Yield();
            using (var stream = new FileStream(SupportFiles.ALL_SCENARIO_EVENTS_BIN, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                stream.Seek(seek, SeekOrigin.Begin);
                var master = new ScenarioMasterExecuteAction();
                master.SetUp(reader);
                

                while (master.IsExecuteFinish == false)
                {
                    await master.OnInitializeAsync();
                    await master.OnExecuteAsync();
                    await master.OnFinalizeAsync();
                    await UniTask.Yield();
                }
            }

        }).Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
