using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMWriteDialogueState : VisualNovelSMStateBase
{

    /// <summary>
    /// Get the info on the current line from the story manager and the write it in the dialogue area
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        context.scene.WriteText(_currentLine.GetData());
    }

    /// <summary>
    /// Wait for user's input and then let the SM read another line
    /// </summary>
    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            context.GoToReadLineCallback();
        }
    }

    /// <summary>
    /// While exiting the stage, clean the Dialogue Area
    /// </summary>
    public override void Exit()
    {
        context.scene.DeleteText();
    }
}
