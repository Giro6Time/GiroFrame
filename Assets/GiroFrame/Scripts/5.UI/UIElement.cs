﻿using UnityEngine;
using Sirenix.OdinInspector;
namespace GiroFrame
{
    public class UIElement
    {
        [LabelText("是否需要缓存")]
        public bool isCache;
        [LabelText("预制体")]
        public GameObject prefab;
        [LabelText("UI层级")]
        public int layerNum;
        /// <summary>
        /// 这个游戏的窗口对象
        /// </summary>
        [HideInInspector]
        public UI_WindowBase objInstance;

    }
}
