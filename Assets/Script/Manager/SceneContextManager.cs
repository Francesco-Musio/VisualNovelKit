using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;
using Characters;

[RequireComponent(typeof(VisualNovelSMController))]
[RequireComponent(typeof(StoryManager))]
public class SceneContextManager : MonoBehaviour
{
    #region Delegates
    public delegate void WriteEvent(string[] _data);
    public WriteEvent WriteText;

    public delegate void DeleteEvent();
    public DeleteEvent DeleteText;
    #endregion

    /// <summary>
    /// Reference to this object's Visual Novel Controller
    /// </summary>
    private VisualNovelSMController visualNovelSMController;

    /// <summary>
    /// Reference to this object's Story Manager
    /// </summary>
    private StoryManager storyManager;

    /// <summary>
    /// Reference to this object's Character Manager
    /// </summary>
    private CharacterManager characterManager;

    [Header("Dialogue Area")]
    [SerializeField]
    [Tooltip("Reference to the Canvas with the Text Areas")]
    private VisualNovelDialogueArea visualNovelDialogueArea;

    #region API
    /// <summary>
    /// Initialize this object and start the Visual Novel State Machine
    /// </summary>
    public void Init ()
    {
        visualNovelSMController = GetComponent<VisualNovelSMController>();
        storyManager = GetComponent<StoryManager>();
        characterManager = GetComponent<CharacterManager>();

        if (storyManager != null)
        {
            storyManager.Init();
        }

        if (characterManager != null)
        {
            characterManager.Init();
        }

        if (visualNovelDialogueArea != null)
        {
            visualNovelDialogueArea.Init();
        }

        if (visualNovelSMController != null)
        {
            visualNovelSMController.Init(this, storyManager, characterManager, visualNovelDialogueArea);
        }

        WriteText += HandleWriteText;
        DeleteText += HandleDeleteText;

        if (visualNovelSMController != null)
        {
            visualNovelSMController.InitSM();
        }
    }
    #endregion API

    // TO DELETE
    #region Delegated
    private void HandleWriteText(string[] _data)
    {
        if (_data[0].Equals("central"))
        {
            visualNovelDialogueArea.Write("central", _data);
        }
        else
        {
            //controlla posizione attore e poi manda comando a canvas
        }
    }

    private void HandleDeleteText()
    {
        visualNovelDialogueArea.Delete();
    }
    #endregion
}
