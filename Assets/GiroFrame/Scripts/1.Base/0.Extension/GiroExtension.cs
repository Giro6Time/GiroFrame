using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
// using UnityEditor.Audio;

using System;
using System.Reflection;

namespace GiroFrame
{

    /// <summary>
    /// JKFrame的主要拓展方法
    /// </summary>

    public static class GiroExtension
    {
        #region  通用
        /// <summary>
        /// 获取特性
        /// </summary>
        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType().GetCustomAttribute<T>();
        }
        /// <summary>
        /// 获取特性
        /// </summary>
        /// <param name="type">特性所在的类型</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this object obj, Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>();
        }
        /// <summary>
        /// 数组完全相等对比，包括数量，每个元素的位置等等
        /// </summary>
        public static bool ArrayEquals(this object[] objs, object[] others)
        {
            if (others == null || objs.GetType() != others.GetType())
            {
                return false;
            }
            if (objs.Length != others.Length)
            {
                return false;
            }
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] != others[i])
                    return false;
            }
            return true;
        }
        #endregion

        #region 资源管理
        /// <summary>GameObject 放入对象池
        /// </summary>
        /// <param name="go"></param>
        public static void GiroGameObjectPushPool(this GameObject go)
        {
            PoolManager.Instance.PushGameObject(go);
        }
        public static void GiroGameObjectPushPool(this Component com)
        {
            PoolManager.Instance.PushGameObject(com.gameObject);
        }
        /// <summary>
        /// 普通类放进池子
        /// </summary>
        /// <param name="obj"></param>
        public static void GiroPushPool(this object obj)
        {

            PoolManager.Instance.PushObject(obj);
        }
        #endregion

        #region 本地化
        /// <summary>
        /// 从本地化系统修改内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="packName"></param>
        /// <param name="contentKey"></param>
        public static void GiroLocalSet(this Text text, string packName, string contentKey)
        {
            text.text = LocalizationManager.Instance.GetContent<L_Text>(packName, contentKey).content;
        }
        /// <summary>
        /// 从本地化系统修改内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="packName"></param>
        /// <param name="contentKey"></param>
        public static void GiroLocalSet(this TMP_Text text, string packName, string contentKey)
        {
            text.text = LocalizationManager.Instance.GetContent<L_Text>(packName, contentKey).content;
        }
        /// <summary>
        /// 从本地化系统修改内容
        /// </summary>
        /// <param name="image"></param>
        /// <param name="packName"></param>
        /// <param name="contentKey"></param>
        public static void GiroLocalSet(this Image image, string packName, string contentKey)
        {
            image.sprite = LocalizationManager.Instance.GetContent<L_Image>(packName, contentKey).content;
        }
        /// <summary>
        /// 从本地化系统修改内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="packName"></param>
        /// <param name="contentKey"></param>
        public static void GiroLocalSet(this AudioSource audioSource, string packName, string contentKey)
        {
            audioSource.clip = LocalizationManager.Instance.GetContent<L_Audio>(packName, contentKey).content;
        }
        /// <summary>
        /// 从本地化系统修改内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="packName"></param>
        /// <param name="contentKey"></param>
        public static void GiroLocalSet(this VideoPlayer videoPlayer, string packName, string contentKey)
        {
            videoPlayer.clip = LocalizationManager.Instance.GetContent<L_Vedio>(packName, contentKey).content;
        }
        #endregion

        #region Mono
        /// <summary>
        ///添加Update监听 
        /// </summary>
        public static void OnUpdate(this object obj, Action action)
        {
            MonoManager.Instance.AddUpdateListener(action);
        }
        /// <summary>
        ///移除Update监听 
        /// </summary>
        public static void RemoveUpdate(this object obj, Action action)
        {
            MonoManager.Instance.RemoveUpdateListener(action);
        }
        /// <summary>
        ///添加FixedUpdate监听 
        /// </summary>
        public static void OnFixedUpdate(this object obj, Action action)
        {
            MonoManager.Instance.AddFixedUpdateListener(action);
        }
        /// <summary>
        ///移除FixedUpdate监听 
        /// </summary>
        public static void RemoveFixedUpdate(this object obj, Action action)
        {
            MonoManager.Instance.RemoveFixedUpdateListener(action);
        }
        /// <summary>
        ///添加LateUpdate监听 
        /// </summary>
        public static void OnLateUpdate(this object obj, Action action)
        {
            MonoManager.Instance.AddLateUpdateListener(action);
        }
        /// <summary>
        ///移除LateUpdate监听 
        /// </summary>
        public static void RemoveLateUpdate(this object obj, Action action)
        {
            MonoManager.Instance.RemoveLateUpdateListener(action);
        }
        public static Coroutine StartCoroutine(this object obj, IEnumerator routine)
        {
            return MonoManager.Instance.StartCoroutine(routine);
        }
        public static void StopAllCoroutines(this object obj)
        {
            MonoManager.Instance.StopAllCoroutines();
        }
        public static void StopCoroutin(this object obj, Coroutine routine)
        {
            MonoManager.Instance.StopCoroutin(routine);
        }
        public static void StopCoroutin(this object obj, IEnumerator routine)
        {
            MonoManager.Instance.StopCoroutin(routine);
        }
        #endregion
    }
}