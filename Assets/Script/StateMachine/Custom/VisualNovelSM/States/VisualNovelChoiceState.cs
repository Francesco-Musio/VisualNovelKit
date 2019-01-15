using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelChoiceState : VisualNovelSMStateBase
{

    public override void Enter()
    {
        ChoiceElement _currentChoice = context.story.GetCurrentChoice();
        context.ui.CreateChoices(_currentChoice.GetChoices());
    }

    public override void Exit()
    {
        context.ui.ResetChoices();
    }

}
