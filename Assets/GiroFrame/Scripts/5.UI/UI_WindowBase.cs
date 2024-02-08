using UnityEngine;
using System;
namespace GiroFrame
{
    /// <summary>
    /// 窗口结果
    /// </summary>
    public enum WindowReslut
    {
        None,
        Yes,
        No
    }
    /// <summary>
    /// 窗口基类
    /// </summary>
    public class UI_WindowBase : MonoBehaviour
    {
        public Type Type { get { return this.GetType(); } }
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init() { }
        /// <summary>
        /// 显示
        /// </summary>
        public virtual void OnShow()
        {
            OnUpdateLanguage();
            RegisterEventListener();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            UIManager.Instance.Close(Type);
            CancelEventListener();
        }
        /// <summary>
        /// 点击是/确认
        /// </summary>
        public virtual void OnCloseClick() { Close(); }
        /// <summary>
        /// 点击否/取消
        /// </summary>
        public virtual void OnYesClick() { Close(); }

        protected virtual void RegisterEventListener()
        {
            EventManager.AddEventListener("UpdateLanguage", OnUpdateLanguage);
        }
        protected virtual void CancelEventListener()
        {
            EventManager.RemoveEventListener("UpdateLanguage", OnUpdateLanguage);
        }
        protected virtual void OnUpdateLanguage()
        {

        }
    }
}