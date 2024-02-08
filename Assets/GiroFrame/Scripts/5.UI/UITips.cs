using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GiroFrame
{
    public class UITips : MonoBehaviour
    {
        [SerializeField]
        private Text infoText;
        [SerializeField]
        private Animator animator;
        private Queue<string> tipQue = new Queue<string>();
        private bool isShow = false;

        /// <summary>
        /// 添加提示
        /// </summary>
        /// <param name="info"></param>
        public void AddTips(string info)
        {
            tipQue.Enqueue(info);
            ShowTips();
        }

        private void ShowTips()
        {
            if (tipQue.Count > 0 && !isShow)
            {
                infoText.text = tipQue.Dequeue();
                animator.Play("Show", 0, 0);
            }
        }
        #region 动画事件
        private void StartTips()
        {
            isShow = true;
        }
        private void EndTips()
        {
            isShow = false;
            ShowTips();
        }
        #endregion
    }
}