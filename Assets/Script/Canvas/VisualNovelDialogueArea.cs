using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualNovelDialogueArea : MonoBehaviour
{
    #region Delegates
    public delegate void DialogeAreaWriteEvent(string _position, string[] _data);
    public DialogeAreaWriteEvent Write;

    public delegate void DialogueAreaDeleteEvent();
    public DialogueAreaDeleteEvent Delete;
    #endregion

    [Header("Text Areas")]
    [SerializeField]
    [Tooltip("Reference to the Central Text Area")]
    private Text CentralTextArea;
    [SerializeField]
    [Tooltip("Reference to the Left Text Area")]
    private Text LeftTextArea;
    [SerializeField]
    [Tooltip("Reference to the Right Text Area")]
    private Text RightTextArea;

    [Header("Name Areas")]
    [SerializeField]
    [Tooltip("Reference to the Left Name Area")]
    private Image LeftNameArea;
    [SerializeField]
    [Tooltip("Reference to the Right Name Area")]
    private Image RightNameArea;

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

    #region Handlers 
    /// <summary>
    /// Depending on the position, The text is written in one of the text areas
    /// </summary>
    /// <param name="position">central or an actor's name</param>
    /// <param name="_data">data string got from ink</param>
    private void HandleWrite(string _position, string[] _data)
    {
        // !! per destra e sinistra, data1 è il soprannome e data2 è il testo
        switch (_position)
        {
            case "central":
                CentralTextArea.text = _data[1];
                CentralTextArea.gameObject.SetActive(true);
                break;
            case "left":
                LeftNameArea.GetComponentInChildren<Text>().text = _data[1];
                RightTextArea.text = _data[2];
                RightTextArea.gameObject.SetActive(true);
                LeftNameArea.gameObject.SetActive(true);
                break;
            case "right":
                RightNameArea.GetComponentInChildren<Text>().text = _data[1];
                LeftTextArea.text = _data[2];
                LeftTextArea.gameObject.SetActive(true);
                RightNameArea.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Delete everything from the Text Areas
    /// </summary>
    private void HandleDelete()
    {
        CentralTextArea.gameObject.SetActive(false);
        RightTextArea.gameObject.SetActive(false);
        LeftTextArea.gameObject.SetActive(false);
        RightNameArea.gameObject.SetActive(false);
        LeftNameArea.gameObject.SetActive(false);

        CentralTextArea.text = "";
        RightTextArea.text = "";
        LeftTextArea.text = "";
        RightNameArea.GetComponentInChildren<Text>().text = "";
        LeftNameArea.GetComponentInChildren<Text>().text = "";
    }
    #endregion
}
