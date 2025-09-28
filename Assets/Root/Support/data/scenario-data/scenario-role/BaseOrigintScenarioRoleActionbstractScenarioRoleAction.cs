
using System;
using System.Collections;
using UnityEngine;
using System.IO;
namespace GameCore.Scenario
{
    public  class BaseOrigintScenarioRoleAction
    {
        public bool IsCompleted { get; protected set; } = false;
        public virtual void ReadBinary(BinaryReader reader)
        {
            
        }
        public virtual void OnInitialize()
        {
            IsCompleted = false;
        }
        public virtual void OnExecute()
        {
            // Implement action logic here
        }
        public virtual void OnFinalize()
        {
            // Implement cleanup logic here
        }
    }
}
