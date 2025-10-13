using System;
using UnityEngine;
using GameCore.Tables;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public static class ScenarioEventIDExtensions
    {
        public static ScenarioEventRow GetRow(this ScenarioEventTableID id)
        {
            if (ScenarioEventTable.Table.TryGetValue(id, out var row))
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
