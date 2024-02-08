using System.Collections.Generic;
namespace GiroFrame
{
    /// <summary>
    /// 状态机控制器
    /// </summary>
    [Pool]
    public class StateMachine
    {
        public int CurrStateType { get; private set; } = -1;
        private StateBase currStateObj;

        //宿主
        private IStateMachineOwner owner;
        /// <summary>
        /// 所有的状态
        /// </summary>
        /// <typeparam name="int">状态枚举的值</typeparam>
        /// <typeparam name="StateBase">具体的状态</typeparam>
        /// <returns></returns>
        private Dictionary<int, StateBase> stateDic = new Dictionary<int, StateBase>();
        public void Init(IStateMachineOwner owner)
        {
            this.owner = owner;
        }
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="T">具体要切换到的状态脚本类型</param>
        /// <param name="newState">新状态</param>
        /// <param name="reCurrstate">新状态和当前状态一致的情况下，是否还要切换</param>
        /// <returns></returns>
        public bool ChangeState<T>(int newState, bool reCurrstate = false) where T : StateBase, new()
        {
            //状态一致，且不需要刷新状态则，切换失败
            if (newState == CurrStateType && !reCurrstate) { return false; }
            //退出当前状态
            if (currStateObj != null)
            {
                currStateObj.Exit();
                currStateObj.RemoveUpdate(currStateObj.Update);
                currStateObj.RemoveLateUpdate(currStateObj.LateUpdate);
                currStateObj.RemoveFixedUpdate(currStateObj.FixedUpdate);
            }
            //进入新状态
            currStateObj = GetState<T>(newState);
            CurrStateType = newState;
            currStateObj.Enter();
            currStateObj.OnUpdate(currStateObj.Update);
            currStateObj.OnLateUpdate(currStateObj.LateUpdate);
            currStateObj.OnFixedUpdate(currStateObj.FixedUpdate);
            return true;
        }
        private StateBase GetState<T>(int stateType) where T : StateBase, new()
        {
            if (stateDic.ContainsKey(stateType)) return stateDic[stateType];
            StateBase state = ResManager.Load<T>();
            state.Init(owner, stateType, this);
            stateDic.Add(stateType, state);
            return state;
        }
        /// <summary>
        /// 停止工作
        /// 把所有状态都释放，但是StateMachine未来还会工作　
        /// </summary>
        public void Stop()
        {
            CurrStateType = -1;
            ///处理当前状态的额外逻辑
            currStateObj.Exit();
            currStateObj.RemoveUpdate(currStateObj.Update);
            currStateObj.RemoveLateUpdate(currStateObj.LateUpdate);
            currStateObj.RemoveFixedUpdate(currStateObj.FixedUpdate);
            currStateObj = null;
            //处理缓存中的所有状态的逻辑
            var enumerator = stateDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.Value.UnInit();
            }
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Destroy()
        {
            Stop();
            //放弃所有资源的引用
            owner = null;
            CurrStateType = -1;
            //放进对象池
            this.GiroPushPool();
        }
    }

}