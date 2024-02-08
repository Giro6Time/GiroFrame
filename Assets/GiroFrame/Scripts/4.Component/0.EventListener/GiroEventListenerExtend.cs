using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace GiroFrame
{
    public static class GiroEventListenerExtend
    {
        #region  工具函数
        private static GiroEventListener GetOrAddGiroEventListener(Component com)
        {
            GiroEventListener lis = com.GetComponent<GiroEventListener>();
            if (lis == null) return com.gameObject.AddComponent<GiroEventListener>();
            else return lis;
        }
        public static void AddEventListener<T>(this Component com, GiroEventType eventType, Action<T, object[]> action,
                                    params object[] args)
        {
            GiroEventListener lis = GetOrAddGiroEventListener(com);
            lis.AddListener(eventType, action, args);
        }

        public static void RemoveEventListener<T>(this Component com, GiroEventType eventType, Action<T, object[]> action,
                                   bool checkArgs = false, params object[] args)
        {
            GiroEventListener lis = GetOrAddGiroEventListener(com);
            lis.RemoveListener(eventType, action, checkArgs, args);
        }

        public static void RemoveAllListener(this Component com, GiroEventType eventType)
        {
            GiroEventListener lis = GetOrAddGiroEventListener(com);
            lis.RemoveAllListener(eventType);
        }
        public static void RemoveAllListener(this Component com)
        {
            GiroEventListener lis = GetOrAddGiroEventListener(com);
            lis.RemoveAllListener();
        }
        #endregion

        #region 鼠标相关事件
        public static void OnMouseEnter(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnMouseEnter, action, args);
        }
        public static void OnMouseExit(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnMouseExit, action, args);
        }
        public static void OnClick(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {

            com.AddEventListener(GiroEventType.OnClick, action, args);
            
        }
        public static void OnClickUp(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnClickUp, action, args);
        }
        public static void OnClickDown(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnClickDown, action, args);
        }
        public static void OnDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnDrag, action, args);
        }
        public static void OnBeginDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnBeginDrag, action, args);
        }
        public static void OnEndDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnEndDrag, action, args);
        }

        public static void RemoveClick(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnClick, action, checkArgs, args);
        }
        public static void RemoveClickUp(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnClickUp, action, checkArgs, args);
        }
        public static void RemoveClickDown(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnClickDown, action, checkArgs, args);
        }
        public static void RemoveDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnDrag, action, checkArgs, args);
        }
        public static void RemoveBeginDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnBeginDrag, action, checkArgs, args);
        }
        public static void RemoveEndDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnEndDrag, action, checkArgs, args);
        }

        #endregion

        #region 碰撞相关事件
        public static void OnCollionEnter(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnCollisionEnter, action, args);
        }
        public static void OnCollisionExit(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnCollisionExit, action, args);
        }
        public static void OnCollionStay(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnCollisionStay, action, args);
        }
        public static void RemoveCollisionEnter(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnCollisionEnter, action, checkArgs, args);
        }
        public static void RemoveCollisionExit(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnCollisionExit, action, checkArgs, args);
        }
        public static void RemoveCollisionStay(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnCollisionStay, action, checkArgs, args);
        }
        public static void OnCollionEnter2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnCollisionEnter2D, action, args);
        }
        public static void OnCollisionExit2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnCollisionExit2D, action, args);
        }
        public static void OnCollionStay2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnCollisionStay2D, action, args);
        }
        public static void RemoveCollisionEnter2D(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnCollisionEnter2D, action, checkArgs, args);
        }
        public static void RemoveCollisionExit2D(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnCollisionExit2D, action, checkArgs, args);
        }
        public static void RemoveCollisionStay2D(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnCollisionStay2D, action, checkArgs, args);
        }

        #endregion

        #region 触发相关事件


        public static void OnTriggerEnter(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnTriggerEnter, action, args);
        }
        public static void OnTriggerExit(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnTriggerExit, action, args);
        }
        public static void OnTriggerStay(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnTriggerStay, action, args);
        }
        public static void RemoveTriggerEnter(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnTriggerEnter, action, checkArgs, args);
        }
        public static void RemoveTriggerExit(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnTriggerExit, action, checkArgs, args);
        }
        public static void RemoveTriggerStay(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnTriggerStay, action, checkArgs, args);
        }
        public static void OnTriggerEnter2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnTriggerEnter2D, action, args);
        }
        public static void OnTriggerExit2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnTriggerExit, action, args);
        }
        public static void OnTriggerStay2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(GiroEventType.OnTriggerStay2D, action, args);
        }
        public static void RemoveTriggerEnter2D(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnTriggerEnter2D, action, checkArgs, args);
        }
        public static void RemoveTriggerExit2D(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnTriggerExit2D, action, checkArgs, args);
        }
        public static void RemoveTriggerStay2D(this Component com, Action<Collision, object[]> action, bool checkArgs, params object[] args)
        {
            com.RemoveEventListener(GiroEventType.OnTriggerStay2D, action, checkArgs, args);
        }
        #endregion
    }
}