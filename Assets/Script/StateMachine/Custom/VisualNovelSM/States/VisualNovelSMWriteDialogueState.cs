using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMWriteDialogueState : VisualNovelSMStateBase
{

    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        context.scene.WriteText(_currentLine.GetData());
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            context.GoToReadLineCallback();
        }
    }

    public override void Exit()
    {
        context.scene.DeleteText();
    }
}
