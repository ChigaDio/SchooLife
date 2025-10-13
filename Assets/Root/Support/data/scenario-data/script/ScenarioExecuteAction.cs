using Cysharp.Threading.Tasks;
using GameCore.Scenario;
using System.IO;
using UnityEngine;

public class ScenarioExecuteAction
{
    private BaseScenarioRoleData roleData;
    private BaseOrigintScenarioRoleAction action;


    public bool IsStartUp => action != null && action.IsStartUp;
    public bool IsRelease => action != null && action.IsRelease;
    public bool IsCompleted => action != null && action.IsCompleted && action.IsStartUp;
    public bool IsOneCompleted => action != null && action.IsOneExecute && action.IsStartUp;

    public void SetUp(ScenarioRoleID id, BinaryReader reader)
    {
        roleData = ScenarioRoleFactory.CreateRoleData(id);
        roleData.ReadBinary(reader);
        action = ScenarioRoleFactory.CreateRoleAction(roleData);
    }


    public UniTask OnInitializeAsync(ScenarioExecuteData executeData)
    {
        if (IsStartUp)
        {
            return UniTask.CompletedTask;
        }
        action.OnInitialize(executeData);
        return UniTask.CompletedTask;
    }


    public UniTask OnOneExecuteAsync(ScenarioExecuteData executeData)
    {
        if (IsOneCompleted)
        {
            return UniTask.CompletedTask;
        }
        action.OnOneExecute(executeData);
        return UniTask.CompletedTask;
    }


    public UniTask OnExecuteAsync(ScenarioExecuteData executeData)
    {
        if (IsCompleted)
        {
            return UniTask.CompletedTask;
        }
        action.OnExecute(executeData);
        return UniTask.CompletedTask;
    }

    public UniTask OnFinalizeAsync(ScenarioExecuteData executeData)
    {
        if (IsRelease)
        {
            return UniTask.CompletedTask;
        }
        action.OnFinalize(executeData);
        return UniTask.CompletedTask;
    }
}