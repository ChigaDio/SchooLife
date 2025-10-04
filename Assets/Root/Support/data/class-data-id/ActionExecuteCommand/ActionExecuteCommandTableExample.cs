using System;
using UnityEngine;
using GameCore.Tables;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public static class ActionExecuteCommandIDExtensions
    {
        public static ActionExecuteCommandRow GetRow(this ActionExecuteCommandTableID id)
        {
            if (ActionExecuteCommandTable.Table.TryGetValue(id, out var row))
            {
                return row;
            }
            else
            {
                return null; // または throw new KeyNotFoundException()
            }
        }
    }
}
