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
    private Text CentralTextArea;
    //RightTextArea
    //LeftTextArea

    #region API
    public void Init()
    {
        Write += HandleWrite;
        Delete += HandleDelete;
    }
    #endregion

    #region Delegated 
    private void HandleWrite(string position, string[] _data)
    {
        switch (_data[0])
        {
            case "central":
                CentralTextArea.text = _data[1];
                break;
        }
    }

    private void HandleDelete()
    {
        CentralTextArea.text = "";
    }
    #endregion
}
