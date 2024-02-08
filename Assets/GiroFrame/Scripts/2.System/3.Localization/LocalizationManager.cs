using UnityEngine;
using Sirenix.OdinInspector;
namespace GiroFrame
{
    /// <summary>
    /// 本地化管理器
    /// 持有本地化资源
    /// 提供本地化管理函数
    /// </summary>
    public class LocalizationManager : ManagerBase<LocalizationManager>
    {
        // 本地化配置资源
        public LocalizationSetting localizationSetting;

        [SerializeField]
        [OnValueChanged("UpdateLanguage")]
        private LanguageType currentLanguageType;

        public LanguageType CurrentLanguageType
        {
            get => currentLanguageType;
            set
            {
                currentLanguageType = value;
                UpdateLanguage();
            }
        }


        /// <summary>
        /// 获取当前语言设置下的内容
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name = "bagName">数据包名称</param>
        /// <param name = "contentKey">内容名称</param>
        public T GetContent<T>(string bagName, string contentKey) where T : class, ILanguage_Content
        {
            return localizationSetting.GetContent<T>(bagName, contentKey, currentLanguageType);
        }
        private void UpdateLanguage()
        {
#if UNITY_EDITOR
            GameRoot.InitForEditor();
#endif
            // 触发更新语言 事件
            EventManager.EventTrigger("UpdateLanguage");
        }
    }
}