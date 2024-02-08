using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace GiroFrame
{
    public class UIManager : ManagerBase<UIManager>
    {

        #region 内部类
        [Serializable]
        private class UILayer
        {
            public Transform root;
            public Image maskImage;
            private int count;
            public void OnShow()
            {
                count += 1;
                Update();
            }
            public void OnClose()
            {
                count -= 1;
                Update();
            }
            private void Update()
            {
                maskImage.raycastTarget = count != 0;
                int posIndex = root.childCount - 2;//倒数第二个窗口是Mask
                maskImage.transform.SetSiblingIndex(posIndex < 0 ? 0 : posIndex);
            }
        }

        #endregion
        /// <summary> 元素资源库
        /// </summary>
        public Dictionary<Type, UIElement> UIElementDic { get { return GameRoot.Instance.GameSetting.UIElementDic; } }
        [SerializeField]
        private UILayer[] UILayers;
        // UI提示窗
        [SerializeField]
        private UITips UITips;
        private const string TipsLocalizationPackName = "Tips";
        public void AddTips(string info)
        {
            UITips.AddTips(info);
        }
        public void AddTipsByLocalization(string packName, string tipsKeyName)
        {
            UITips.AddTips(LocalizationManager.Instance.GetContent<L_Text>(packName, tipsKeyName).content);
        }

        public void AddTipsByLocalization(string tipsKeyName)
        {
            UITips.AddTips(LocalizationManager.Instance.GetContent<L_Text>(TipsLocalizationPackName, tipsKeyName).content);
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="layer">层级 = -1即为不设置</param>
        /// <typeparam name="T">窗口类型，</typeparam>
        /// <returns></returns>

        public T Show<T>(int layer = -1) where T : UI_WindowBase
        {
            return Show(typeof(T), layer) as T;
        }
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="type">窗口类型</param>
        /// <param name="layer">层级 = -1即为不设置</param>
        /// <returns></returns>
        public UI_WindowBase Show(Type type, int layer = -1)
        {
            //资源库中是否存在这个type
            if (UIElementDic.ContainsKey(type))
            {
                UIElement info = UIElementDic[type];
                if (info.objInstance != null && info.objInstance.gameObject.activeSelf) return info.objInstance;
                int layerNum = layer == -1 ? info.layerNum : layer;
                // 实例化实例或者获取到实例，保证窗口存在
                if (info.objInstance != null)
                {
                    info.objInstance.gameObject.SetActive(true);
                    info.objInstance.transform.SetParent(UILayers[layerNum].root);
                    info.objInstance.transform.SetAsLastSibling();
                    info.objInstance.OnShow();
                }
                else
                {
                    UI_WindowBase window = ResManager.InstantiateForPrefab(info.prefab, UILayers[layerNum].root).GetComponent<UI_WindowBase>();
                    info.objInstance = window;
                    window.Init();
                    window.OnShow();
                }
                info.layerNum = layerNum;
                UILayers[layerNum].OnShow();
                return info.objInstance;
            }
            return null;
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="layer">窗口层级，为-1即不设置</param>
        /// <typeparam name="T">窗口类型</typeparam>
        /// <returns></returns>
        public void Close<T>() where T : UI_WindowBase
        {
            Close(typeof(T));
        }

        public void Close(Type type)
        {
            UIElement info = UIElementDic[type];
            if (info.objInstance == null || !info.objInstance.gameObject.activeSelf) return;
            //缓存则隐藏
            if (info.isCache)
            {
                info.objInstance.transform.SetAsFirstSibling();
                info.objInstance.gameObject.SetActive(false);
            }
            // 不缓存则销毁
            else
            {
                Destroy(info.objInstance.gameObject);
                info.objInstance = null;

            }

            UILayers[info.layerNum].OnClose();
        }
        public void CloseAll()
        {
            var enumerator = UIElementDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.Value.objInstance.Close();
            }
        }
    }

}
