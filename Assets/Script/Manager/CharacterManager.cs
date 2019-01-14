using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        #region Delegates
        public delegate void PlaceActorEvent(string[] _data, out int multiplier);
        public PlaceActorEvent PlaceActor;
        #endregion

        [Header("Actors")]
        [SerializeField]
        [Tooltip("List of Actor Prefabs")]
        private List<GameObject> actorPrefabs = new List<GameObject>();
        /// <summary>
        /// List of each Actor's Actor script
        /// </summary>
        private List<Actor> actorList = new List<Actor>();
        [SerializeField]
        [Tooltip("Reference to the Active Actor on the Left. This is only for debugging purpose")]
        private Actor activeLeftActor = null;
        [SerializeField]
        [Tooltip("Reference to the Active Actor on the Right. This is only for debugging purpose")]
        private Actor activeRightActor = null;

        [Header("Actor Positions")]
        [SerializeField]
        [Tooltip("Position that the Actor's GameObject on the left should have")]
        private Vector3 rightPosition;
        [SerializeField]
        [Tooltip("Position that the Actor's GameObject on the right should have")]
        private Vector3 leftPosition;

        #region API
        /// <summary>
        /// Initialize this Object and its events
        /// </summary>
        public void Init()
        {
            foreach (GameObject actorPref in actorPrefabs)
            {
                GameObject _newActor = Instantiate(actorPref);
                Actor _actorScript = _newActor.GetComponent<Actor>();
                _newActor.name = _actorScript.GetName();
                _actorScript.Init();
                actorList.Add(_actorScript);
            }

            PlaceActor += HandlePlaceActor;
        }

        public string GetActorPosition(string _data)
        {
            if (activeLeftActor != null && activeLeftActor.GetName() == _data)
            {
                return "left";
            }
            else if (activeRightActor != null && activeRightActor.GetName() == _data)
            {
                return "right";
            }
            else
            {
                return "central";
            }
        }
        #endregion

        #region Handlers
        /// <summary>
        /// Handle the request to put actors on the scene
        /// </summary>
        /// <param name="_data">data string taken from the ink file</param>
        private void HandlePlaceActor(string[] _data, out int multiplier)
        {
            if (activeLeftActor != null && activeLeftActor.GetName() != _data[0])
            {
                multiplier = 2;
            }
            else if (activeRightActor != null && activeRightActor.GetName() != _data[1])
            {
                multiplier = 2;
            }
            else
            {
                multiplier = 1;
            }

            StartCoroutine(CHandlePlaceActor(_data));
        }
        #endregion

        private IEnumerator CHandlePlaceActor(string[] _data)
        {
            bool wait = false;

            string _leftActorName = _data[0];
            string _rightActorName = _data[1];
            int _duration = int.Parse(_data[2]);

            if (activeLeftActor != null && activeLeftActor.GetName() != _leftActorName)
            {
                activeLeftActor.RemoveActor(_duration);
                activeLeftActor = null;
                wait = true;
            }

            if (activeRightActor != null && activeRightActor.GetName() != _rightActorName)
            {
                activeRightActor.RemoveActor(_duration);
                activeRightActor = null;
                wait = true;
            }

            if (wait)
            {
                yield return new WaitForSeconds(_duration);
            }

            if (_leftActorName != "null")
            {
                foreach (Actor _actor in actorList)
                {
                    if (_actor.GetName() == _leftActorName && _actor.GetPosition() == ActorState.OffScene)
                    {
                        _actor.InsertActor(_duration, leftPosition, ActorState.Left);
                        activeLeftActor = _actor;
                    }
                }
            }

            if (_rightActorName != "null")
            {
                foreach (Actor _actor in actorList)
                {
                    if (_actor.GetName() == _rightActorName && _actor.GetPosition() == ActorState.OffScene)
                    {
                        _actor.InsertActor(_duration, rightPosition, ActorState.Right);
                        activeRightActor = _actor;
                    }
                }
            }
        }
    }
}
