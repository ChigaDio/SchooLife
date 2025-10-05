using GameCore;
using GameCore.Enums;
using System;
using UnityEngine;
using System.Collections.Generic;
using GameCore.Tables.ID;
using UnityEngine.UI;
using static GameSceneCanvas;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Threading;
using GameCore.SaveSystem;

public class GameSceneCanvas : BaseSingleton<GameSceneCanvas>
{
    [Serializable]
    public class BarView
    {
        [SerializeField]
        private Image image;
        public readonly float CHANGE_TIME = 1.0f;


        public void SetUp()
        {
            if (!image.Equals(null)) return;

        }



        public void SetFillAmout(float value)
        {
            if(image.Equals(null)) return;

            value = Mathf.Clamp(value, 0.0f, 1.0f);
            image.fillAmount = value;
        }
    }


    [Serializable]
    public class TopText
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI nameText;

        [SerializeField]
        private TMPro.TextMeshProUGUI turnText;

        public void SetNameText(string value)
        {
            if (nameText.Equals(null)) return;

            nameText.text = $"名前: {value}";
        }

        public void SetTurnText(TurnTableID turnID)
        {
            if(turnText.Equals(null)) return;
            turnText.text = $"{(int)turnID - 1}ターン";
        }
    }
    [Serializable]
    public class TopViewObject
    {
        [SerializeField]
        private TopText topText = new TopText();

        [SerializeField]
        private BarView hpBarView = new BarView();

        public void SetUp()
        {
            hpBarView.SetUp();
        }



        public void SetNameText(string value)
        {
            topText.SetNameText(value);
        }

        public void SetTurnText(TurnTableID turnID)
        {
            topText.SetTurnText(turnID);
        }
    }

    [Serializable]
    public class ViewObject<TID>                
        where TID : struct, Enum
    {
        protected Button button;
        public Button Button { get { return button; } }

        private UnityAction action;

        [SerializeField]
        private TMPro.TextMeshProUGUI textMeshPro;

        [SerializeField]
        protected GameObject gameObject;

        protected RectTransform rectTransform;

        [SerializeField]
        private TID statID = default;
        public TID GetID() { return statID; }


        public Vector3 GetPosition()
        {
            if (gameObject == null) return Vector3.zero;
            return rectTransform.position;


        }


        public virtual void SetUp()
        {
            if (gameObject == null) return;
            if(textMeshPro == null)textMeshPro = gameObject?.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            rectTransform = gameObject.GetComponent<RectTransform>();
            button = gameObject?.GetComponentInChildren<Button>();

        }


        public void SetText(string text)
        {
            if (textMeshPro == null) return;
            textMeshPro.text = text;
        }

        public void AddButtonClickListener(UnityAction valueAction)
        {
            if (button == null) return;
            if (action != null) RemoveButtonClickListener();
            button.onClick.AddListener(valueAction);
            this.action = valueAction;
        }

        public void RemoveButtonClickListener()
        {
            if (action == null) return;
            if (button == null) return;
            button.onClick.RemoveListener(action);
            action = null;
        }

        public void ClearButtonClickListeners()
        {
            if (button == null) return;
            button.onClick.RemoveAllListeners();
        }
    }
    [Serializable]
    public class ViewObjectAnimation<TAnim, TEnum, TID> : ViewObject<TID>
                where TAnim : BaseAnimState<TEnum>, new()
        where TEnum : struct, Enum
        where TID : struct, Enum
    {
        public GameAnimatorController<TAnim, TEnum> animator = new GameAnimatorController<TAnim, TEnum>();




        public override void SetUp()
        {
            base.SetUp();
            if (gameObject == null) return;
            animator.SetUp(gameObject);

        }



    }

    [Serializable]
    public class ActionViewObject<TID>
        where TID : struct, Enum
    {
        [SerializeField]
        private List<ViewObject<TID>> viewStatusList = new List<ViewObject<TID>>();

        public GameAnimatorController<ActionAnimator, ActionAnimatorEnum> animator = new GameAnimatorController<ActionAnimator, ActionAnimatorEnum>();

        [SerializeField]
        private GameObject gameObject;

        public void SetUp()
        {
            if(gameObject == null) return;
            animator.SetUp(gameObject);
            foreach(var data in viewStatusList)
            {
                data.SetUp();
            }
        }

        private ViewObject<TID> FInd(TID findID)
        {
            return viewStatusList.Find(v => v.GetID().Equals(findID));
        }

        public Vector3 GetPosition(TID findID)
        {
            return viewStatusList.Find(v => v.GetID().Equals(findID))?.GetPosition() ?? Vector3.zero;
        }


        public void AllButtonEventClear()
        {
            foreach(var data in viewStatusList)
            {
                data.RemoveButtonClickListener();
            }
        }

        public void AddButtonClickListener(TID findID,UnityAction valueAction)
        {
            var result = FInd(findID);
            if (result == null) return;
            result.AddButtonClickListener(valueAction);
        }

        public void RemoveButtonClickListener(TID findID)
        {
            var result = FInd(findID);
            if (result == null) return;
            result.RemoveButtonClickListener();
        }


    }
    [Serializable]
    public class ActionViewObjectList
    {
        /// <summary>
        /// 実行リスト
        /// </summary>
        [SerializeField]
        private ActionViewObject<ActionExecuteCommandTableID> viewExecute = new ActionViewObject<ActionExecuteCommandTableID>();

        /// <summary>
        /// 学習リスト
        /// </summary>
        [SerializeField]
        private ActionViewObject<CharacterStatID> viewStatus = new ActionViewObject<CharacterStatID>();

        /// <summary>
        /// デートリスト
        /// </summary>
        [SerializeField]
        private ActionViewObject< CharacterTableID> viewDate = new ActionViewObject<CharacterTableID>();

        public void SetUp()
        {
            viewStatus.SetUp();
            viewDate.SetUp();
            viewExecute.SetUp();
        }



        public void PlayActionViewObject(ActionCommandID id, ActionAnimatorEnum animID, Action action = null)
        {
            switch(id)
            {
                case ActionCommandID.Execute:
                    viewExecute.animator.Play(animID, onComplete:action);
                    break;
                case ActionCommandID.Status:
                    viewStatus.animator.Play(animID, onComplete: action);
                    break;
                case ActionCommandID.Date:
                    viewDate.animator.Play(animID, onComplete: action);
                    break;
            }        
        }

        public Vector3 GetPosition(ActionCommandID id, Enum findID)
        {
            switch (id)
            {
                case ActionCommandID.Execute:
                    return viewExecute.GetPosition((ActionExecuteCommandTableID)findID);
                case ActionCommandID.Status:
                    return viewStatus.GetPosition((CharacterStatID)findID);
                case ActionCommandID.Date:
                    return viewDate.GetPosition((CharacterTableID)findID);
            }
            return Vector3.zero;
        }

        public void AddButtonClickListener(ActionCommandID id, Enum findID, UnityAction valueAction)
        {
            switch (id)
            {
                case ActionCommandID.Execute:
                    viewExecute.AddButtonClickListener((ActionExecuteCommandTableID)findID, valueAction);
                    break;
                case ActionCommandID.Status:
                    viewStatus.AddButtonClickListener((CharacterStatID)findID, valueAction);
                    break;
                case ActionCommandID.Date:
                    viewDate.AddButtonClickListener((CharacterTableID)findID, valueAction);
                    break;
            }
        }

        public void RemoveButtonClickListener(ActionCommandID id, Enum findID)
        {
            switch (id)
            {
                case ActionCommandID.Execute:
                    viewExecute.RemoveButtonClickListener((ActionExecuteCommandTableID)findID);
                    break;
                case ActionCommandID.Status:
                    viewStatus.RemoveButtonClickListener((CharacterStatID)findID);
                    break;
                case ActionCommandID.Date:
                    viewDate.RemoveButtonClickListener((CharacterTableID)findID);
                    break;
            }
        }

        public void AllButtonEventClear()
        {
            viewExecute.AllButtonEventClear();
            viewStatus.AllButtonEventClear();
            viewDate.AllButtonEventClear();
        }



    }


    /// <summary>
    /// ステータス表示用
    /// </summary>
    [SerializeField]
    private List<ViewObjectAnimation<StatusItemAnimation, StatusItemAnimationEnum, CharacterStatID>> viewStatusList = new List<ViewObjectAnimation<StatusItemAnimation, StatusItemAnimationEnum, CharacterStatID>>();
    public List<ViewObjectAnimation<StatusItemAnimation, StatusItemAnimationEnum, CharacterStatID>> GetViewObjectAnimations { get { return viewStatusList; } }

    /// <summary>
    /// ボタンアクション
    /// </summary>
    [SerializeField]
    private ActionViewObjectList actionViewObjectList = new ActionViewObjectList();
    public ActionViewObjectList GetActionViewObjectList { get { return actionViewObjectList; } }

    /// <summary>
    /// エルミートマネージャー
    /// </summary>
    [SerializeField]
    private HermiteUIManager hermiteUIManager = null;
    public void CreatehHermiteAsync(
        Vector3 start,
        Vector3 target,
        int[] text,
        float duration = 1.5f,
        Action<HermiteUIObject> onEachArrived = null,
        Action onAllCompleted = null)
    {
        hermiteUIManager?.CreateAsync(start, target, text, duration, onEachArrived, onAllCompleted).Forget();
    }

    /// <summary>
    /// top部分のUI
    /// </summary>
    [SerializeField]
    private TopViewObject topViewObject = new TopViewObject();
    public TopViewObject GetTopViewObject {  get { return topViewObject; } }

    [SerializeField]
    private BarView timeLimitBarView = new BarView();
    public BarView TimeLimitBarView { get { return timeLimitBarView; } }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        DebugLogBridge.Log("Debug Start----");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(PlayerData playerData)
    {
        //名前
        topViewObject.SetNameText(playerData.familyName + " " + playerData.firstName);
        topViewObject.SetTurnText(playerData.nowTurn);

        //ステータス
        foreach (var item in viewStatusList)
        {
            item.SetText(playerData.PlaterStatusNum(item.GetID()).ToString());
        }
    }



    public override void AwakeSingleton()
    {
        base.AwakeSingleton();
        foreach (var item in viewStatusList)
        {
            item.SetUp();
        }
        actionViewObjectList.SetUp();
    }
}
