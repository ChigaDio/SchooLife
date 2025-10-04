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

public class GameSceneCanvas : BaseSingleton<GameSceneCanvas>
{
    [Serializable]
    public class ViewObject<TID>                
        where TID : struct, Enum
    {
        protected Button button;
        public Button Button { get { return button; } }

        private UnityAction action;

        private TMPro.TextMeshPro textMeshPro;

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
            textMeshPro = gameObject?.GetComponentInChildren<TMPro.TextMeshPro>();
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
        string[] text,
        float duration = 1.5f,
        Action<HermiteUIObject> onEachArrived = null,
        Action onAllCompleted = null)
    {
        hermiteUIManager?.CreateAsync(start, target, text, duration, onEachArrived, onAllCompleted).Forget();
    }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
