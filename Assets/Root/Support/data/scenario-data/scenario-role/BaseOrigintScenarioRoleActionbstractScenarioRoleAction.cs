

using System;
using System.Collections;
using UnityEngine;
using System.IO;
namespace GameCore.Scenario
{
    public  class BaseOrigintScenarioRoleAction
    {
        public bool IsCompleted { get; protected set; } = false;
        public bool IsOneExecute { get; protected set; } = false;
        public bool IsStartUp { get; protected set; } = false;
        public bool IsRelease { get; protected set; } = false;
        public virtual void ReadBinary(BinaryReader reader)
        {
            
        }
        public virtual void OnInitialize(ScenarioExecuteData executeData)
        {
            IsCompleted = false;
        }
        public virtual void OnOneExecute(ScenarioExecuteData executeData)
        {
            // Implement action logic here
        }
        public virtual void OnExecute(ScenarioExecuteData executeData)
        {
            // Implement action logic here
        }
        public virtual void OnFinalize(ScenarioExecuteData executeData)
        {
            // Implement cleanup logic here
        }
    }
}

