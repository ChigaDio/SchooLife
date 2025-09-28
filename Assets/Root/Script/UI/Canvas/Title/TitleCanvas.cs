using GameCore;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : BaseSingleton<TitleCanvas>
{
    public enum SelectTile
    {
        None = -1,
        GameStart,
        Exit,
        Credit,
        Setting,
        Max
    }

    [SerializeField]
    private Button[] selectButton = new Button[(int)SelectTile.Max];

    private Dictionary<SelectTile, UnityAction> registeredActions = new Dictionary<SelectTile, UnityAction>();

    public void AddListenerButton(SelectTile selectTile, UnityAction action)
    {
        if (selectTile == SelectTile.None || selectTile == SelectTile.Max) return;

        int index = (int)selectTile;
        if (selectButton == null || index < 0 || index >= selectButton.Length || selectButton[index] == null)
        {
            Debug.LogError($"Button at index {index} ({selectTile}) is null or array is invalid!");
            return;
        }

        // Remove previous listener
        if (registeredActions.TryGetValue(selectTile, out var oldAction))
        {
            selectButton[index].onClick.RemoveListener(oldAction);
            registeredActions.Remove(selectTile);
        }

        // Add new listener
        selectButton[index].onClick.AddListener(action);
        registeredActions[selectTile] = action;
    }

    private void OnDestroy()
    {
        foreach (var pair in registeredActions)
        {
            if (selectButton[(int)pair.Key] != null)
            {
                selectButton[(int)pair.Key].onClick.RemoveListener(pair.Value);
            }
        }
        registeredActions.Clear();
    }
}