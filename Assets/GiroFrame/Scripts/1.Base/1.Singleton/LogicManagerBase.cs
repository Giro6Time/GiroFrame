using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GiroFrame
{
    /// <summary>
    /// 逻辑管理器基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LogicManagerBase<T> : SingletonMono<T> where T : LogicManagerBase<T>
    {
        /// <summary>
        /// 注册事件监听
        /// </summary>
        protected abstract void RegisterEventListener();
        /// <summary>
        /// 取消事件监听
        /// </summary>
        protected abstract void CancelEventListener();
    }
}