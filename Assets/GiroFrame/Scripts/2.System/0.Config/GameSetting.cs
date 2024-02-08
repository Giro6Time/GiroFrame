using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;
namespace GiroFrame
{
    /// <summary>
    /// 框架层面的游戏设置
    /// 对象池缓存设置、UI元素的设置
    /// </summary>
    [CreateAssetMenu(fileName = "GameSetting", menuName = "GiroFrame/Config/GameSetting")]

    public class GameSetting : ConfigBase
    {
        [LabelText("对象池设置")]
        [DictionaryDrawerSettings(KeyLabel = "类型", ValueLabel = "皆可")]
        public Dictionary<Type, bool> cacheDic;
        [LabelText("UI窗口设置")]
        [DictionaryDrawerSettings(KeyLabel = "类型", ValueLabel = "UI窗口数据")]
        public Dictionary<Type, UIElement> UIElementDic = new Dictionary<Type, UIElement>();

#if UNITY_EDITOR

        /// <summary>
        /// 编译前执行函数
        /// </summary>
        [Button(Name = "初始化游戏配置", ButtonHeight = 50)]
        [GUIColor(0, 1, 0)]
        public void InitForEditor()
        {
            PoolAttributeOnEditor();
            UIElementAttributeOnEditor();
        }

        /// <summary>
        /// 将带有Pool特性的类型加入缓存池字典
        /// </summary>
        private void PoolAttributeOnEditor()
        {
            cacheDic.Clear();
            //获取所有程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //遍历程序集
            foreach (Assembly asms in assemblies)
            {
                //遍历程序集下的每一个类型
                Type[] types = asms.GetTypes();
                foreach (Type type in types)
                {
                    PoolAttribute pool = type.GetCustomAttribute<PoolAttribute>();

                    if (pool != null)
                    {
                        cacheDic.Add(type, true);
                    }
                }
            }

        }

        private void UIElementAttributeOnEditor()
        {
            UIElementDic.Clear();
            //获取所有程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type baseType = typeof(UI_WindowBase);
            //遍历程序集
            foreach (Assembly asms in assemblies)
            {

                //遍历程序集下的每一个类型
                Type[] types = asms.GetTypes();
                foreach (Type type in types)
                {

                    if (baseType.IsAssignableFrom(type)
                    && !type.IsAbstract)
                    {
                        UIElementAttribute attribute = type.GetCustomAttribute<UIElementAttribute>();
                        if (attribute != null)
                        {
                            UIElementDic.Add(type, new UIElement()
                            {
                                isCache = attribute.isCache,
                                prefab = Resources.Load<GameObject>(attribute.resPath),
                                layerNum = attribute.layerNum
                            });
                        }


                    }
                }
            }
        }
    }
#endif

}
