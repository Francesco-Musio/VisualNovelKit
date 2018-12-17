using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using StoryManagerNS;
using Characters;

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
        public void Init(SceneContextManager _scene, StoryManager _story, CharacterManager _characters)
        {
            this.context = new VisualNovelSMContext(_scene, _story, _characters, goToWriteDialogueCallback, goToReadLineCallback, goToPlaceActorCallback);

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

        private void goToPlaceActorCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToPlaceActor");
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
        public CharacterManager characters;

        public Action GoToWriteDialogueCallback;
        public Action GoToReadLineCallback;
        public Action GoToPlaceActorCallback;

        public VisualNovelSMContext(SceneContextManager _scene, StoryManager _story, CharacterManager _characters, Action _goToWriteDialogueCallback, Action _goToReadLineCallback, Action _goToPlaceActorCallback)
        {
            scene = _scene;
            story = _story;
            characters = _characters;

            GoToWriteDialogueCallback = _goToWriteDialogueCallback;
            GoToReadLineCallback = _goToReadLineCallback;
            GoToPlaceActorCallback = _goToPlaceActorCallback;
        }

    }

}
