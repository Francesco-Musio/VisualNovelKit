using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    #region Delegates
    public delegate void DialogeAreaWriteEvent(string _position, string[] _data);
    public DialogeAreaWriteEvent Write;

    public delegate void DialogueAreaDeleteEvent();
    public DialogueAreaDeleteEvent Delete;

    public delegate int ChangeBackgroundEvent(string[] _target);
    public ChangeBackgroundEvent ChangeBackground;
    #endregion

    [Header("Dialogue Canvas")]
    [SerializeField]
    private VisualNovelDialogueArea visualNovelDialogueArea;

    [Header("Background Canvas")]
    [SerializeField]
    private VisualNovelLayersArea visualNovelLayersArea;

    #region API
    public void Init()
    {
        if (visualNovelDialogueArea != null)
            visualNovelDialogueArea.Init();

        if (visualNovelLayersArea != null)
            visualNovelLayersArea.Init();

        Write += HandleWrite;
        Delete += HandleDelete;
        ChangeBackground += HandleChangeBackground;
    }
    #endregion

    #region Handlers
    private void HandleWrite(string _position, string[] _data)
    {
        visualNovelDialogueArea.Write(_position, _data);
    }

    private void HandleDelete()
    {
        visualNovelDialogueArea.Delete();
    }

    private int HandleChangeBackground(string[] _target)
    {
        return visualNovelLayersArea.ChangeBackground(_target);
    }
    #endregion

}
