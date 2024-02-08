using System;
namespace GiroFrame
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    /// <summary>
    /// UI元素的特性
    /// 每个UI窗口都应该添加
    /// </summary>
    public class UIElementAttribute : Attribute
    {
        public bool isCache;
        public string resPath;
        public int layerNum;
        private string resoursePath;
        public UIElementAttribute(bool isCache, string resPath, int layerNum)
        {
            this.isCache = isCache;
            this.resPath = resPath;
            this.layerNum = layerNum;
        }
    }
}
