using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Managers
{
    public class MainLoopStateManagerData : BaseMainLoopStateManagerData
    {
        private bool isExit = false;
        public void OnExit() { isExit = true; }
        public bool IsExit() { return isExit; }
    }
}
