using Cysharp.Threading.Tasks;
using GameCore.Scenario;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScenarioSubGroupExecuteAction
{
    private List<ScenarioExecuteAction> scenarioActionList = new List<ScenarioExecuteAction>();
    public int SubGroupID { get; private set; }

    public void SetUp(BinaryReader reader)
    {
        SubGroupID = reader.ReadInt32(); // �T�u�C�x���gID
        int actionCount = reader.ReadInt32(); // �A�N�V�����i���[���j��
        for (int i = 0; i < actionCount; i++)
        {
            var addAction = new ScenarioExecuteAction();
            var id = (ScenarioRoleID)reader.ReadInt32(); // ���[��ID
            addAction.SetUp(id, reader);
            scenarioActionList.Add(addAction);
        }
    }

    public async UniTask OnInitializeAsync(ScenarioExecuteData executeData)
    {
        var tasks = scenarioActionList.Select(action => action.OnInitializeAsync(executeData));
        await UniTask.WhenAll(tasks);
    }

    public async UniTask OnExecuteAsync(ScenarioExecuteData executeData)
    {
        var oneTasks = scenarioActionList
                    .Where(action => !action.IsOneCompleted)
                    .Select(action => action.OnOneExecuteAsync(executeData))
                    .ToArray();
        await UniTask.WhenAll(oneTasks);
        while (scenarioActionList.Any(action => !action.IsCompleted))
        {
            var tasks = scenarioActionList
                .Where(action => !action.IsCompleted)
                .Select(action => action.OnExecuteAsync(executeData))
                .ToArray();
            await UniTask.WhenAll(tasks);
            await UniTask.Yield();
        }
    }

    public async UniTask OnFinalizeAsync(ScenarioExecuteData executeData)
    {
        var tasks = scenarioActionList.Select(action => action.OnFinalizeAsync(executeData)).ToArray();
        await UniTask.WhenAll(tasks);
    }
}