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
        public void Init(SceneContextManager _scene, StoryManager _story, CharacterManager _characters, UIManager _ui)
        {
            this.context = new VisualNovelSMContext(_scene, _story, _characters, _ui);
            context.ActionsInit(goToWriteDialogueCallback, goToReadLineCallback, goToPlaceActorCallback, goToChangeBackgroundCallback);

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
        /// <summary>
        /// Used to switch from ReadLineState to WriteLineState
        /// </summary>
        private void goToWriteDialogueCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToWriteDialogue");
        }

        /// <summary>
        /// Used to switch from ReadLineState to PlaceActorState
        /// </summary>
        private void goToPlaceActorCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToPlaceActor");
        }

        /// <summary>
        /// Used to return to ReadLineState
        /// </summary>
        private void goToReadLineCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToReadLine");
        }

        private void goToChangeBackgroundCallback()
        {
            this.VisalNovelSM.SetTrigger("GoToChangeBackground");
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
        public UIManager ui;

        public Action GoToWriteDialogueCallback;
        public Action GoToReadLineCallback;
        public Action GoToPlaceActorCallback;
        public Action GoToChangeBackgroundCallback;

        /// <summary>
        /// Initialize this object
        /// </summary>
        /// <param name="_scene"></param>
        /// <param name="_story"></param>
        /// <param name="_characters"></param>
        /// <param name="_dialogues"></param>
        public VisualNovelSMContext(SceneContextManager _scene, StoryManager _story, CharacterManager _characters, UIManager _ui)
        {
            scene = _scene;
            story = _story;
            characters = _characters;
            ui = _ui;
        }

        /// <summary>
        /// Setup the Actions avaiable in this context
        /// </summary>
        /// <param name="_goToWriteDialogueCallback"></param>
        /// <param name="_goToReadLineCallback"></param>
        /// <param name="_goToPlaceActorCallback"></param>
        public void ActionsInit(Action _goToWriteDialogueCallback, Action _goToReadLineCallback, Action _goToPlaceActorCallback, Action _goToChangeBackgroundCallback)
        {
            GoToWriteDialogueCallback = _goToWriteDialogueCallback;
            GoToReadLineCallback = _goToReadLineCallback;
            GoToPlaceActorCallback = _goToPlaceActorCallback;
            GoToChangeBackgroundCallback = _goToChangeBackgroundCallback;
        }

    }

}
