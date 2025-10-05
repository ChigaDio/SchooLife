using System;
using UnityEngine;
using GameCore.Tables;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public static class TurnEventIDExtensions
    {
        public static TurnEventRow GetRow(this TurnEventTableID id)
        {
            if (TurnEventTable.Table.TryGetValue(id, out var row))
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
