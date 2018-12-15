using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.VisualNovelSM
{

    /// <summary>
    /// Controller for the Game SM
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class VisualNovelSMController : MonoBehaviour
    {
        /// <summary>
        /// Reference to SM context
        /// </summary>
        protected VisualNovelSMContext context;

        /// <summary>
        /// Reference to the Game SM
        /// </summary>
        protected Animator InGameSM;

        /// <summary>
        /// Initialize the SM and Setup of every State
        /// </summary>
        /// <param name="_context"></param>
        public void Init(SceneContextManager _context)
        {
            this.context = new VisualNovelSMContext(_context);

            this.InGameSM = GetComponent<Animator>();

            foreach(StateMachineBehaviour _current in InGameSM.GetBehaviours<StateMachineBehaviour>())
            {
                IState _state = _current as IState;
                if (_state != null)
                {
                    _state.Setup(this.context);
                }
            }

            this.InGameSM.SetTrigger("StartSM");
        }

    }

    /// <summary>
    /// Context of the SM
    /// </summary>
    public class VisualNovelSMContext: ISMContext
    {

        public SceneContextManager context;

        public VisualNovelSMContext(SceneContextManager _context)
        {
            context = _context;
        }

    }

}
