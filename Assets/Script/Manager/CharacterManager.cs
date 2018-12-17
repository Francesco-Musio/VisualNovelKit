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
        private List<GameObject> actorPrefabs = new List<GameObject>();
        private List<Actor> actorList = new List<Actor>();
        [SerializeField]
        private Actor activeLeftActor = null;
        [SerializeField]
        private Actor activeRightActor = null;

        [Header("Actor Positions")]
        [SerializeField]
        private Vector3 rightPosition;
        [SerializeField]
        private Vector3 leftPosition;

        #region API
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
        #endregion

        #region Delegated
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
