namespace GiroFrame
{
    public interface IStateMachineOwner { }
    /// <summary>
    /// 状态基类
    /// </summary>
    public abstract class StateBase
    {
        protected StateMachine stateMachine;
        /// <summary>
        /// 初始化状态
        /// </summary>
        /// <param name="owner">宿主</param>
        /// <param name="stateType">状态机类型枚举的值</param>
        /// <param name="stateMachine">所属状态机</param>
        public virtual void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        /// <summary>
        /// 反初始化
        /// 不再使用时调用，放回对象池
        /// </summary>
        public virtual void UnInit()
        {
            this.GiroPushPool();    
        }
        /// <summary>
        /// 状态进入
        /// </summary>
        public virtual void Enter() { }
        /// <summary>
        /// 状态退出
        /// </summary>
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }


    }
}