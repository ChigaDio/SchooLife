using UnityEngine;
using GameCore.States.Managers;
using GameCore.States.ID;

namespace GameCore.States
{
    public abstract class BaseGameSceneState : BaseState<GameSceneStateID, GameSceneStateManagerData>
    {
    }
}
