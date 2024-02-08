using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace GiroFrame
{
    public enum GiroEventType
    {
        OnMouseEnter,
        OnMouseExit,
        OnClick,
        OnClickDown,
        OnClickUp,
        OnDrag,
        OnBeginDrag,
        OnEndDrag,
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnCollisionEnter2D,
        OnCollisionStay2D,
        OnCollisionExit2D,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,
        OnTriggerEnter2D,
        OnTriggerStay2D,
        OnTriggerExit2D,
    }
    public interface IMouseEvent :
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
    }

    public class GiroEventListener : MonoBehaviour, IMouseEvent
    {
        private Dictionary<GiroEventType, IGiroEventListenerEventInfos> eventInfoDic = new Dictionary<GiroEventType, GiroEventListener.IGiroEventListenerEventInfos>();

        interface IGiroEventListenerEventInfos
        {
            /// <summary>
            /// 移除全部
            /// </summary>
            void RemoveAll();

        }
        #region 内部类，接口等
        /// <summary> 某个事件中一个时间的数据包装类
        /// </summary>
        /// <typeparam name="T"></typeparam>

        private class GiroEventListenerEventInfo<T>
        {
            //T：事件本身的参数(PointerEventData、Collision)
            //object[]：事件的参数
            public Action<T, object[]> action;
            public object[] args;
            public void Init(Action<T, object[]> action, object[] args)
            {
                this.action = action;
                this.args = args;

            }
            public void Destroy()
            {
                this.action = null;
                this.args = null;
                this.GiroPushPool();
            }
            public void TriggerEvent(T eventData)
            {
                action?.Invoke(eventData, args);
            }
        }
        /// <summary>一类事件的数据包装类型，包含多个GiroEventListenerEventInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>

        private class GiroEventListenerEventInfos<T> : IGiroEventListenerEventInfos
        {
            //所有的事件

            private List<GiroEventListenerEventInfo<T>> eventList = new List<GiroEventListenerEventInfo<T>>();
            /// <summary>
            /// 添加事件
            /// </summary>
            public void AddListener(Action<T, object[]> action, params object[] args)
            {
                GiroEventListenerEventInfo<T> info = PoolManager.Instance.GetObject<GiroEventListenerEventInfo<T>>();
                info.Init(action, args);
                eventList.Add(info);

            }
            /// <summary>
            /// 移除事件
            /// </summary>
            public void RemoveListener(Action<T, object[]> action, bool checkArgs = false, params object[] args)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    //是否需要检查参数
                    if (eventList[i].Equals(action))
                    {
                        //是否需要检查参数
                        if (checkArgs && args.Length > 0)
                        {   //参数如果相等
                            if (args.ArrayEquals(eventList[i].args))
                            {
                                //移除
                                eventList[i].Destroy();
                                eventList.RemoveAt(i);
                                return;
                            }
                        }
                    }
                    else
                    {
                        //移除
                        eventList[i].Destroy();
                        eventList.RemoveAt(i);
                        return;
                    }
                }
            }

            /// <summary>
            /// 移除全部
            /// </summary>
            public void RemoveAll()
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].Destroy();
                }
                eventList.Clear();
                this.GiroPushPool();
            }

            public void TriggerEvent(T eventData)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].TriggerEvent(eventData);
                }
            }
        }

        private class GiroEventTypeComparer : Singleton<GiroEventTypeComparer>, IEqualityComparer<GiroEventType>
        {
            public bool Equals(GiroEventType a, GiroEventType b)
            {
                return a == b;
            }
            public int GetHashCode(GiroEventType obj)
            {
                return (int)obj;
            }
        }


        #endregion
        #region 外部的访问(类，接口等)
        /// <summary> 添加一个事件
        /// </summary>
        public void AddListener<T>(GiroEventType eventType, Action<T, object[]> action,
                                    params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as GiroEventListenerEventInfos<T>).AddListener(action, args);
            }
            else
            {
                GiroEventListenerEventInfos<T> infos = PoolManager.Instance.GetObject<GiroEventListenerEventInfos<T>>();
                infos.AddListener(action, args);
                eventInfoDic.Add(eventType, infos);
            }
        }
        /// <summary> 移除某一个事件
        /// </summary>
        public void RemoveListener<T>(GiroEventType eventType, Action<T, object[]> action,
                                   bool checkArgs = false, params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as GiroEventListenerEventInfos<T>).RemoveListener(action, checkArgs, args);
            }
        }
        /// <summary> 删除某一个事件下的所有事件
        /// </summary>
        public void RemoveAllListener(GiroEventType eventType)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                eventInfoDic[eventType].RemoveAll();
            }
        }
        /// <summary>移除所有事件
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (IGiroEventListenerEventInfos infos in eventInfoDic.Values)
            {
                infos.RemoveAll();
            }
            eventInfoDic.Clear();
        }
        #endregion

        /// <summary> 触发事件
        /// </summary>
        private void TriggerAction<T>(GiroEventType eventType, T eventData)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {

                (eventInfoDic[eventType] as GiroEventListenerEventInfos<T>).TriggerEvent(eventData);
            }
        }

        #region 鼠标事件
        public void OnBeginDrag(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnBeginDrag, eventData);
        }
        public void OnDrag(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnDrag, eventData);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnEndDrag, eventData);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnClick, eventData);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnClickDown, eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnClickUp, eventData);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnMouseEnter, eventData);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            TriggerAction(GiroEventType.OnMouseExit, eventData);
        }

        #endregion
        #region 碰撞事件
        private void OnCollionEnter(Collision collision)
        {
            TriggerAction(GiroEventType.OnCollisionEnter, collision);
        }
        private void OnCollionExit(Collision collision)
        {
            TriggerAction(GiroEventType.OnCollisionExit, collision);
        }
        private void OnCollionStay(Collision collision)
        {
            TriggerAction(GiroEventType.OnCollisionStay, collision);
        }
        private void OnCollionEnter2D(Collision2D collision)
        {
            TriggerAction(GiroEventType.OnCollisionExit2D, collision);
        }
        private void OnCollionExit2D(Collision2D collision)
        {
            TriggerAction(GiroEventType.OnCollisionExit2D, collision);
        }
        private void OnCollionStay2D(Collision2D collision)
        {
            TriggerAction(GiroEventType.OnCollisionStay2D, collision);
        }


        #endregion
        #region 触发事件
        private void OnTriggerEnter(Collider other)
        {
            TriggerAction(GiroEventType.OnTriggerEnter, other);

        }
        private void OnTriggerExit(Collider other)
        {
            TriggerAction(GiroEventType.OnTriggerExit, other);
        }
        private void OnTriggerStay(Collider other)
        {
            TriggerAction(GiroEventType.OnTriggerStay, other);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerAction(GiroEventType.OnTriggerEnter2D, other);
        }
        private void OnTriggerExi2D(Collider2D other)
        {
            TriggerAction(GiroEventType.OnTriggerExit2D, other);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerAction(GiroEventType.OnTriggerStay2D, other);
        }
        #endregion



    }
}