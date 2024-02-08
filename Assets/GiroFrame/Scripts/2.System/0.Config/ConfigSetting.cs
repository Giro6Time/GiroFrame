using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace GiroFrame
{
    /// <summary>
    /// 所有游戏中（非框架）配置，游戏运行时只有一个
    /// 包含所有的配置文件
    /// </summary>
    [CreateAssetMenu(fileName = "ConfigSetting", menuName = "GiroFrame/ConfigSetting")]
    public class ConfigSetting : ConfigBase
    {
        /// <summary>
        /// 所有配置的容器
        /// <配置类型的名称,<id,具体位置>>
        /// </summary>
        [DictionaryDrawerSettings(KeyLabel = "类型", ValueLabel = "列表")]
        public Dictionary<string, Dictionary<int, ConfigBase>> configDic;

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="configTypeName">具体的配置名称</param>
        /// <param name="id">ID</param>
        /// <typeparam name="T">具体的配置类型</typeparam>
        /// <returns></returns>
        public T GetConfig<T>(string configTypeName, int id) where T : ConfigBase
        {
            if (!configDic.ContainsKey(configTypeName))//检查类型
            {
                throw new System.Exception("Giro:配置设置中不包含这个Key: " + configTypeName + ",里在干什莫？？？");
            }
            if (!configDic[configTypeName].ContainsKey(id))//检查ID
            {
                throw new System.Exception("Giro:你的这个配置" + configTypeName + "里面没有这个ID: " + id + " 阿，你是铸币吗？？");
            }
            return configDic[configTypeName][id] as T;
        }
    }
}
