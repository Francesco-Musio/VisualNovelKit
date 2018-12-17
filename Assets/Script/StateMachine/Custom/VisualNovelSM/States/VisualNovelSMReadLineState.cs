using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMReadLineState : VisualNovelSMStateBase {

    public override void Enter()
    {
        LineElement _currentLine = context.story.ReadLine();
        switch (_currentLine.GetId())
        {
            case 0:
                context.GoToWriteDialogueCallback();
                break;
        }

    }

}
