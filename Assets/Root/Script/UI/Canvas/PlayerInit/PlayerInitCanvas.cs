using GameCore;
using GameCore.Tables;
using GameCore.Tables.ID;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInitCanvas : BaseSingleton<PlayerInitCanvas>
{
    [Serializable]
    public class ButtonItem
    {
        [SerializeField]
        private Button button;
        private UnityAction action;
        public void ButtonActive(bool active)
        {
            if (button == null) return;
            button.enabled = active;
        }

        public void AddEvent(UnityAction valueAction)
        {
            if(button == null) return;
            if (action != null) button.onClick.RemoveListener(action);
            action = valueAction;
            button.onClick.AddListener(action);
        }

        public void Release()
        {
            if(button == null) return; 
            if(action != null)
            {
                button.onClick.RemoveListener(action); 
            }
        }
    }
    [Serializable]
    public class InputData
    {
        public TMP_InputField familyInput;
        public TMP_InputField firstInput;
        public TMP_Dropdown personalityDown;

        public void SetUp()
        {
            if (personalityDown == null) return;
            personalityDown.ClearOptions();

            List<string> option = new List<string>();
            for(PersonalityTableID id = PersonalityTableID.None + 1; id < PersonalityTableID.Max; id++)
            {
                if(!id.GetRow().Use) continue;
                option.Add(id.GetRow().Jptext);
            }
            personalityDown.AddOptions(option);
            personalityDown.RefreshShownValue();

        }
    }

    [Serializable]
    public class SelectItem
    {
        public ItemTableID id;
        public Button button;
        public TMP_Text text;
        public void SetUp(bool active,string valueText)
        {
            if (button == null) return;
            button.enabled = active;
            if (text == null) return;
            text.text = valueText;
        }

    }

    [SerializeField]
    public List<SelectItem> items = new List<SelectItem>();
    public void SetUp(ItemTableID id, bool active, string valueText)
    {
        var find = items?.Find(x => x.id == id);
        if (find == null) return;
        find.SetUp(active, valueText);
    }

    [SerializeField]
    private ButtonItem finishButtonInput = null;
    [SerializeField]
    private InputData inputData = null;
    public InputData GetInputData { get { return inputData; } }

    [SerializeField]
    private ButtonItem finishButtonSelectItem = null;

    public enum ParentField
    {
        Input,
        Select,
        Max
    }
    [SerializeField]
    private GameObject[] parentArray = new GameObject[(int)ParentField.Max];
    public void SetActive(bool active,ParentField field)
    {
        parentArray[(int)field].SetActive(active);
    }

    public void FinishButtonInputAction(UnityAction action)
    {
        if(finishButtonInput == null) return;
        finishButtonInput.AddEvent(action);
    }

    public void FinishButtonSelectItem(UnityAction action)
    {
        if( finishButtonSelectItem == null) return;
        finishButtonSelectItem.AddEvent(action);
    }

    public void OnDestroy()
    {
        finishButtonInput?.Release();
        finishButtonSelectItem?.Release();
    }

    public override void AwakeSingleton()
    {
        base.AwakeSingleton();
        inputData?.SetUp();

    }
}
