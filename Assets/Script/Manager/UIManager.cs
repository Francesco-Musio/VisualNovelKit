using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class UIManager : MonoBehaviour
{
    #region Delegates
    public delegate void DialogeAreaWriteEvent(string _position, string[] _data);
    public DialogeAreaWriteEvent Write;

    public delegate void DialogueAreaDeleteEvent();
    public DialogueAreaDeleteEvent Delete;

    public delegate int ChangeBackgroundEvent(string[] _target);
    public ChangeBackgroundEvent ChangeBackground;

    public delegate void ChoiceAreaCreateEvent(List<Choice> _choices);
    public ChoiceAreaCreateEvent CreateChoices;
    #endregion

    [Header("Dialogue Canvas")]
    [SerializeField]
    private VisualNovelDialogueArea visualNovelDialogueArea;

    [Header("Background Canvas")]
    [SerializeField]
    private VisualNovelLayersArea visualNovelLayersArea;

    [Header("Choice Canvas")]
    [SerializeField]
    private VisualNovelChoiceArea visualNovelChoiceArea;

    #region API
    /// <summary>
    /// Initialize this object
    /// </summary>
    public void Init()
    {
        if (visualNovelDialogueArea != null)
            visualNovelDialogueArea.Init();

        if (visualNovelLayersArea != null)
            visualNovelLayersArea.Init();

        if (visualNovelChoiceArea != null)
            visualNovelChoiceArea.Init();

        Write += HandleWrite;
        Delete += HandleDelete;
        ChangeBackground += HandleChangeBackground;
        CreateChoices += HandleCrateChoices;
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Write text on the Dialogue Area
    /// </summary>
    /// <param name="_position">position of the actor</param>
    /// <param name="_data">string array obtained from the ink file</param>
    private void HandleWrite(string _position, string[] _data)
    {
        visualNovelDialogueArea.Write(_position, _data);
    }

    /// <summary>
    /// Delete all Text from the Dialogue area
    /// </summary>
    private void HandleDelete()
    {
        visualNovelDialogueArea.Delete();
    }

    /// <summary>
    /// TO MODIFY
    /// </summary>
    /// <param name="_target"></param>
    /// <returns></returns>
    private int HandleChangeBackground(string[] _target)
    {
        return visualNovelLayersArea.ChangeBackground(_target);
    }

    private void HandleCrateChoices (List<Choice> _choices)
    {
        visualNovelChoiceArea.CreateChoices(_choices);
    }
    #endregion

}
