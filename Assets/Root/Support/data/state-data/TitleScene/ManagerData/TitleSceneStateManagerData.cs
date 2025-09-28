using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Managers
{
    public class TitleSceneStateManagerData : BaseTitleSceneStateManagerData
    {
        public TitleCanvas.SelectTile selectTitle = TitleCanvas.SelectTile.None;
        private bool isExit = false;
        public void OnExit() { isExit = true;}
        public bool IsExit() { return isExit; }
    }
}
