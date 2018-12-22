using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        #region Delegates
        public delegate void PlaceActorEvent(string[] _data);
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

        #region Delegated
        /// <summary>
        /// Handle the request to put actors on the scene
        /// </summary>
        /// <param name="_data">data string taken from the ink file</param>
        private void HandlePlaceActor(string[] _data)
        {

            if (activeLeftActor != null && activeLeftActor.GetName() != _data[0])
            {
                activeLeftActor.RemoveActor(int.Parse(_data[2]));
                activeLeftActor = null;
            }

            if (activeRightActor != null && activeRightActor.GetName() != _data[1])
            {
                activeRightActor.RemoveActor(int.Parse(_data[2]));
                activeRightActor = null;
            }

            if (_data[0] != "null")
            {
                foreach (Actor _actor in actorList)
                {
                    if (_actor.GetName() == _data[0] && _actor.GetPosition() == ActorState.OffScene)
                    {
                        _actor.InsertActor(int.Parse(_data[2]), leftPosition, ActorState.Left);
                        activeLeftActor = _actor;
                    }
                }
            }

            if (_data[1] != "null")
            {
                foreach (Actor _actor in actorList)
                {
                    if (_actor.GetName() == _data[1] && _actor.GetPosition() == ActorState.OffScene)
                    {
                        _actor.InsertActor(int.Parse(_data[2]), rightPosition, ActorState.Right);
                        activeRightActor = _actor;
                    }
                }
            }

        }
        #endregion
    }
}
