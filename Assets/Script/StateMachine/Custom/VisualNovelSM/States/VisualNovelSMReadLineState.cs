using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMReadLineState : VisualNovelSMStateBase {

    /// <summary>
    /// Read a line from the story file and then switch to a different state based on the line id
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.ReadLine();
        switch (_currentLine.GetId())
        {
            case 0:
                context.GoToWriteDialogueCallback();
                break;

            case 1:
                context.GoToPlaceActorCallback();
                break;
        }

    }

}
