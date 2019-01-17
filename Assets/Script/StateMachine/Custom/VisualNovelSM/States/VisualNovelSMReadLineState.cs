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
        if (context.story.ReadLine())
        {
            LineElement _currentLine = context.story.GetCurrentLine();
            switch (_currentLine.GetId())
            {
                case 0:
                    context.GoToWriteDialogueCallback();
                    break;

                case 1:
                    context.GoToPlaceActorCallback();
                    break;

                case 2:
                    context.GoToChangeBackgroundCallback();
                    break;

                case 3:
                    context.GoToWaitCallback();
                    break;

                case 4:
                    context.GoToChangeEmotionCallback();
                    break;
            }
        }
        else if (context.story.ReadChoice())
        {
            context.GoToChoiceState();
        }
        else
        {
            Debug.Log("END");
        }
        
    }

}
