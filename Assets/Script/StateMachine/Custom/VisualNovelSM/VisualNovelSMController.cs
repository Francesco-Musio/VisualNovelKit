using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using StoryManagerNS;

namespace StateMachine.VisualNovelSM
{

    /// <summary>
    /// Controller for the Game SM
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class VisualNovelSMController : MonoBehaviour
    {
        #region Delegates
        public delegate void VNSMEvent();
        public VNSMEvent InitSM;
        #endregion 

        /// <summary>
        /// Reference to SM context
        /// </summary>
        protected VisualNovelSMContext context;

        /// <summary>
        /// Reference to the Game SM
        /// </summary>
        protected Animator VisalNovelSM;

        /// <summary>
        /// Initialize the SM and Setup of every State
        /// </summary>
        /// <param name="_context"></param>
        public void Init(SceneContextManager _context, StoryManager _story)
        {
            this.context = new VisualNovelSMContext(_context, _story, goToWriteDialogueCallback, goToReadLineCallback);

            this.VisalNovelSM = GetComponent<Animator>();

            foreach(StateMachineBehaviour _current in VisalNovelSM.GetBehaviours<StateMachineBehaviour>())
            {
                IState _state = _current as IState;
                if (_state != null)
                {
                    _state.Setup(this.context);
                }
            }

            InitSM += HandleInitSM;
        }

        #region Delegated
        private void HandleInitSM()
        {
            this.VisalNovelSM.SetTrigger("StartSM");
        }
        #endregion

        #region Callbacks
        private void goToWriteDialogueCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToWriteDialogue");
        }

        private void goToReadLineCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToReadLine");
        }
        #endregion

    }

    /// <summary>
    /// Context of the SM
    /// </summary>
    public class VisualNovelSMContext: ISMContext
    {

        public SceneContextManager scene;
        public StoryManager story;

        public Action GoToWriteDialogueCallback;
        public Action GoToReadLineCallback;

        public VisualNovelSMContext(SceneContextManager _scene, StoryManager _story, Action _goToWriteDialogueCallback, Action _goToReadLineCallback)
        {
            scene = _scene;
            story = _story;

            GoToWriteDialogueCallback = _goToWriteDialogueCallback;
            GoToReadLineCallback = _goToReadLineCallback;
        }

    }

}
