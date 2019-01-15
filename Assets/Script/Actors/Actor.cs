using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Characters.Animations;

namespace Characters
{
    /// <summary>
    /// Enum to signal the position of this actor in the scene
    /// </summary>
    public enum ActorState {
        OffScene,
        Left,
        Right
    }
    
    [RequireComponent(typeof(ActorAnimationController))]
    public class Actor : MonoBehaviour
    {
        #region Delegates
        public delegate void ActorRemoveEvent(int _duration);
        public ActorRemoveEvent RemoveActor;

        public delegate void ActorInsertEvent(int _duration, Vector3 _target, ActorState _newPosition);
        public ActorInsertEvent InsertActor;

        public delegate void ActorChangeState(int _newState, int _duration);
        public ActorChangeState ChangeState;
        #endregion

        [Header("General Info")]
        [SerializeField]
        [Tooltip("Name of the actor. This is used to identify this actor")]
        private string actorName = "Actor";

        [Header("Current State")]
        [SerializeField]
        [Tooltip("Reference to this object's graphics")]
        private GameObject graphics;
        [SerializeField]
        [Tooltip("Current emotion of this actor")]
        private string currentEmotion;
        [SerializeField]
        [Tooltip("Current position of this actor")]
        private ActorState position;

        [Header("Emotions List")]
        [SerializeField]
        [Tooltip("List of emotions that the actor can assume")]
        private Sprite[] emotions;

        /// <summary>
        /// Reference to the animation controller of this actor
        /// </summary>
        private ActorAnimationController actorAnimationCtrl;

        #region API
        /// <summary>
        /// Initialize this object
        /// Set current emotion at 0, disable the object and initialize events
        /// </summary>
        public void Init()
        {
            if (actorName == "Actor")
            {
                actorName = this.gameObject.name;
            }

            position = ActorState.OffScene;

            SpriteRenderer spriteRenderer = graphics.GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = emotions[0];
            
            Color _tempColor = spriteRenderer.color;
            _tempColor.a = 0;
            spriteRenderer.color = _tempColor;
            
            currentEmotion = spriteRenderer.sprite.name;

            this.transform.position = new Vector3(1000, 1000, 1000);
            this.gameObject.SetActive(false);

            actorAnimationCtrl = GetComponent<ActorAnimationController>();
            if (actorAnimationCtrl != null)
            {
                actorAnimationCtrl.Init(spriteRenderer);
            }

            RemoveActor += HandleRemoveActor;
            InsertActor += HandleInsertActor;
            ChangeState += HandleChangeState;
        }
        #endregion

        #region Getters
        public string GetName()
        {
            return actorName;
        }

        public ActorState GetPosition()
        {
            return position;
        }
        #endregion

        #region Handlers
        /// <summary>
        /// Remove this actor from the scene
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        private void HandleRemoveActor(int _duration)
        {
            StartCoroutine(CRemoveActor(_duration));
        }

        /// <summary>
        /// Insert the actor in the scene
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_target">target position for the actor</param>
        /// <param name="_newPosition">new ActorState</param>
        private void HandleInsertActor(int _duration, Vector3 _target, ActorState _newPosition)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(CInsertActor(_duration, _target, _newPosition));
        }

        /// <summary>
        /// Changes the current emotion of the actor
        /// Start coroutine if this actor is active in scene
        /// </summary>
        /// <param name="newState">id of the new emotion</param>
        private void HandleChangeState(int _newState, int _duration)
        {
            if (emotions.Length > _newState && _newState >= 0)
            {
                if (position == ActorState.OffScene)
                    graphics.GetComponent<SpriteRenderer>().sprite = emotions[_newState];
                else
                    StartCoroutine(CChangeState(_newState, _duration));
            }
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Coroutine that animate the actor's exit and disable the object
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <returns></returns>
        private IEnumerator CRemoveActor (int _duration)
        {
            Vector3 _target = new Vector3();

            if (position == ActorState.Left)
            {
                _target = this.transform.position - new Vector3(10, 0, 0);
            }
            else if (position == ActorState.Right)
            {
                _target = this.transform.position + new Vector3(10, 0, 0);
            }

            yield return actorAnimationCtrl.HorizontalTransitionFadeOut(_target, _duration);
            
            this.gameObject.SetActive(false);
            this.position = ActorState.OffScene;
            this.transform.position = new Vector3(1000, 1000, 1000);
            yield return null;
        }

        /// <summary>
        /// Coroutine that insert the actor in the scene
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_target">Target position of the actor</param>
        /// <param name="_newPosition">new ActorState of this actor</param>
        /// <returns></returns>
        private IEnumerator CInsertActor(int _duration,  Vector3 _target, ActorState _newPosition)
        {
            
            if (_newPosition == ActorState.Left)
            {
                this.transform.position = _target - new Vector3(10, _target.y, _target.z);
            }
            else if (_newPosition == ActorState.Right)
            {
                this.transform.position = _target + new Vector3(10, _target.y, _target.z);
            }

            yield return actorAnimationCtrl.HorizontalTransitionFadeIn(_target, _duration);

            this.position = _newPosition;
            yield return null;
        }

        /// <summary>
        /// Coroutine that change the emotion of this actor
        /// </summary>
        /// <param name="_newState">target emotion id</param>
        /// <param name="_duration">duration of the animation</param>
        /// <returns></returns>
        private IEnumerator CChangeState(int _newState, int _duration)
        {
            //crea nuovo oggetto grafica figlio
            GameObject _newGraphics = GameObject.Instantiate(graphics, graphics.transform.position, graphics.transform.rotation, transform);
            SpriteRenderer _newSpr = _newGraphics.GetComponent<SpriteRenderer>();
            Color _tempColor = _newSpr.color;
            _tempColor.a = 0;
            _newSpr.color = _tempColor;
            _newSpr.sprite = emotions[_newState];

            //fade out grafica principale
            actorAnimationCtrl.FadeOut(_duration);

            //fade in grafica secondaria
            yield return actorAnimationCtrl.FadeIn(_duration, _newSpr);

            //elimino la vecchia grafica primaria
            GameObject.Destroy(graphics);

            //setto la nuova grafica primaria
            graphics = _newGraphics;
            graphics.name = "Graphics";
            actorAnimationCtrl.SetSpriteRenderer(_newSpr);

            currentEmotion = _newSpr.sprite.name;

            yield return null;

        }
        #endregion

    }
}
