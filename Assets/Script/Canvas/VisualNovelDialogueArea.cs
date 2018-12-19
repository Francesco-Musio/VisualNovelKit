using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualNovelDialogueArea : MonoBehaviour
{
    #region Delegates
    public delegate void DialogeAreaWriteEvent(string position, string[] _text);
    public DialogeAreaWriteEvent Write;

    public delegate void DialogueAreaDeleteEvent();
    public DialogueAreaDeleteEvent Delete;
    #endregion

    [Header("Text Areas")]
    [SerializeField]
    [Tooltip("Reference to the Central Text Area")]
    private Text CentralTextArea;
    //RightTextArea
    //LeftTextArea

    #region API
    /// <summary>
    /// Initialize this object and register events
    /// </summary>
    public void Init()
    {
        Write += HandleWrite;
        Delete += HandleDelete;
    }
    #endregion

    #region Delegated 
    /// <summary>
    /// Depending on the position, The text is written in one of the text areas
    /// </summary>
    /// <param name="position">central or an actor's name</param>
    /// <param name="_data">data string got from ink</param>
    private void HandleWrite(string position, string[] _data)
    {
        switch (_data[0])
        {
            case "central":
                CentralTextArea.text = _data[1];
                break;
        }
    }

    /// <summary>
    /// Delete everything from the Text Areas
    /// </summary>
    private void HandleDelete()
    {
        CentralTextArea.text = "";
    }
    #endregion
}
