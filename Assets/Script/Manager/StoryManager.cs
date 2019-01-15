using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System.IO;

/**
 * Line to print in the middle:
 *  0 $ central $ message
 *  
 * Place Actor:
 *  1 $ LeftActorName $ RightActorName $ TransitionTime
 *  
 * Change Background:
 *  2 $ BackgroundName $ TransitionTime
 *  
 * Wait:
 *  3 $ TimeToWait
 *  
 * Change Emotion:
 *  4 $ TargetActor $ TargetStateID $ TransitionTime
 */

namespace StoryManagerNS
{
    public class StoryManager : MonoBehaviour
    {
        #region Delegates
        public delegate bool StoryEvent();
        public StoryEvent ReadLine;
        public StoryEvent ReadChoice;
        #endregion

        [Header("Ink File")]
        [SerializeField]
        [Tooltip("Story File")]
        private TextAsset inkJSONAsset;
        [SerializeField]
        private Story story;

        [Header("Parsing Options")]
        [SerializeField]
        [Tooltip("Character used to separate different parts of the command string in the json file")]
        private char delimiter = '$';

        /// <summary>
        /// last read line
        /// </summary>
        private LineElement currentLine = null;

        private ChoiceElement currentChoice = null;

        #region API
        /// <summary>
        /// Initialize this object
        /// </summary>
        public void Init()
        {
            if (inkJSONAsset != null)
            {
                story = new Story(inkJSONAsset.text);
            }

            ReadLine += HandleReadLine;
            ReadChoice += HandleReadChoice;
        }
        #endregion

        #region Handler
        /// <summary>
        /// Read the next Line from the story
        /// </summary>
        /// <returns>Line Elements with the just read informations</returns>
        private bool HandleReadLine()
        {
            if (story.canContinue)
            {

                string _rawtext = story.Continue();
                _rawtext = _rawtext.Trim();

                LineElement _lineElement = ScriptableObject.CreateInstance(typeof(LineElement)) as LineElement;
                _lineElement.Init(_rawtext.Split(delimiter));

                currentLine = _lineElement;

                return true;
            }

            return false;
        }

        private bool HandleReadChoice()
        {
            if (!story.canContinue && story.currentChoices.Count > 0)
            {
                ChoiceElement _choiceElement = ScriptableObject.CreateInstance(typeof(ChoiceElement)) as ChoiceElement;
                _choiceElement.Init(story.currentChoices);

                currentChoice = _choiceElement;

                return true;
            }

            return false;
        }
        #endregion

        #region Getters
        public LineElement GetCurrentLine()
        {
            return currentLine;
        }

        public ChoiceElement GetCurrentChoice()
        {
            return currentChoice;
        }
        #endregion
        
    }

    public class LineElement : ScriptableObject
    {

        /// <summary>
        /// id of this line's command
        /// </summary>
        private int id;

        /// <summary>
        /// array with the command options
        /// </summary>
        private string[] data;

        /// <summary>
        /// Initialize this object
        /// </summary>
        /// <param name="_rawdata"></param>
        public void Init(string[] _rawdata)
        {
            data = new string[_rawdata.Length - 1];

            for (int i = 0; i < _rawdata.Length; i++)
            {
                if (i == 0)
                {
                    id = int.Parse(_rawdata[0]);
                }
                else
                {
                    data[i - 1] = _rawdata[i];
                }
            }
        }

        #region Getters
        public int GetId()
        {
            return id;
        }

        public string[] GetData()
        {
            return data;
        }
        #endregion

        #region Setters
        public void SetTimer(int value)
        {
            data[data.Length - 1] = value.ToString();
        }
        #endregion
    }

    public class ChoiceElement : ScriptableObject
    {
        private List<Choice> choices = new List<Choice>();

        #region API
        public void Init(List<Choice> _choices)
        {
            choices = _choices;
        }
        #endregion

        #region Getters
        public List<Choice> GetChoices()
        {
            return choices;
        }
        #endregion
    }
}



