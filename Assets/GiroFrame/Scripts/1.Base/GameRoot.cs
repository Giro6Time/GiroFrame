using UnityEngine;
using UnityEditor;

namespace GiroFrame
{
    public class GameRoot : SingletonMono<GameRoot>
    {



        /// <summary> 框架设置
        /// </summary>
        [SerializeField]
        private GameSetting gameSetting;
        public GameSetting GameSetting { get { return gameSetting; } }

        /// <summary><see cref="Awake"/>
        /// </summary>
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            base.Awake();
            DontDestroyOnLoad(gameObject);

            InitManager();
        }

        ///<summary>初始化所有管理器
        /// </summary>
        private void InitManager()
        {
            ManagerBase[] managers = GetComponents<ManagerBase>();
            foreach (ManagerBase manager in managers)
            {
                manager.Init();
            }


        }
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void InitForEditor()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            if (Instance == null && GameObject.Find("GameRoot") != null)
            {
                Instance = GameObject.Find("GameRoot").GetComponent<GameRoot>();
                EventManager.Clear();
                Instance.InitManager();
                Instance.gameSetting.InitForEditor();
                UI_WindowBase[] window = Instance.transform.GetComponentsInChildren<UI_WindowBase>();
                foreach (UI_WindowBase win in window)
                {
                    win.OnShow();
                }
            }
            
        }
#endif
    }


}