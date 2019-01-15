using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;
using Characters;

[RequireComponent(typeof(VisualNovelSMController))]
[RequireComponent(typeof(StoryManager))]
public class SceneContextManager : MonoBehaviour
{

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

    /// <summary>
    /// Reference to this object's UI Manager
    /// </summary>
    private UIManager uIManager;

    #region API
    /// <summary>
    /// Initialize this object and start the Visual Novel State Machine
    /// </summary>
    public void Init ()
    {
        visualNovelSMController = GetComponent<VisualNovelSMController>();
        storyManager = GetComponent<StoryManager>();
        characterManager = GetComponent<CharacterManager>();
        uIManager = GetComponent<UIManager>();

        if (storyManager != null)
        {
            storyManager.Init();
        }

        if (characterManager != null)
        {
            characterManager.Init();
        }

        if (uIManager != null)
        {
            uIManager.Init();
        }

        if (visualNovelSMController != null)
        {
            visualNovelSMController.Init(this, storyManager, characterManager, uIManager);
        }

        if (visualNovelSMController != null)
        {
            visualNovelSMController.InitSM();
        }
    }
    #endregion
}
