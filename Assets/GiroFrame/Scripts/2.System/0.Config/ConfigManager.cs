using UnityEngine;
namespace GiroFrame
{
    public class ConfigManager : ManagerBase<ConfigManager>
    {
        [SerializeField]
        private ConfigSetting configSetting;


        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="configTypeName"></param>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetConfig<T>(string configTypeName, int id) where T : ConfigBase
        {
            return configSetting.GetConfig<T>(configTypeName, id);
        }

    }
}