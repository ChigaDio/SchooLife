using System;
using UnityEngine;
using GameCore.Tables;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public static class TurnIDExtensions
    {
        public static TurnRow GetRow(this TurnTableID id)
        {
            if (TurnTable.Table.TryGetValue(id, out var row))
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
