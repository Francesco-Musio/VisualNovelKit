using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    /// <summary>
    /// Abstract Class for an SM State
    /// </summary>
    public abstract class StateMachineStateBase : StateMachineBehaviour, IState
    {

        /// <summary>
        /// Setup of this State
        /// </summary>
        /// <param name="_context"></param>
        public abstract void Setup(ISMContext _context);

        /// <summary>
        /// Actions that are going to be executed on State Enter
        /// </summary>
        public virtual void Enter() {}

        /// <summary>
        /// Actions that are going to be executed on State Update
        /// </summary>
        public virtual void Tick() { }

        /// <summary>
        /// Actions that are going to be executed on State Exit
        /// </summary>
        public virtual void Exit()  {}

        #region Overrides
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Enter();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Tick();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Exit();
        }
        #endregion
    }

    /// <summary>
    /// Interface of an SM State
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Setup of this State
        /// </summary>
        /// <param name="_context"></param>
        void Setup(ISMContext _context);

        /// <summary>
        /// Actions that are going to be executed on State Enter
        /// </summary>
        void Enter();

        /// <summary>
        /// Actions that are going to be executed on State Update
        /// </summary>
        void Tick();

        /// <summary>
        /// Actions that are going to be executed on State Exit
        /// </summary>
        void Exit();
    }

    /// <summary>
    /// Interface for an SM State's Context
    /// </summary>
    public interface ISMContext
    {

    }

}
