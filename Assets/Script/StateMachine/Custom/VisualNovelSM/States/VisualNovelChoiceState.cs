using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelChoiceState : VisualNovelSMStateBase
{

    public override void Enter()
    {
        ChoiceElement _currentChoice = context.story.GetCurrentChoice();
        // ui manager fa apparire pulsanti per ogni scelta.
    }

}
