using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMChangeEmotionState : VisualNovelSMStateBase
{
    /// <summary>
    /// Duration of the animation
    /// </summary>
    private int animationTime = 10000;

    /// <summary>
    /// Multiplier of the animation TIme
    /// </summary>
    private int multiplier = 1;

    /// <summary>
    /// Call the change state event and then go to wait state
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        string[] _data = _currentLine.GetData();
        animationTime = int.Parse(_data[_data.Length - 1]);

        context.characters.ChangeState(_data, out multiplier);

        _currentLine.SetTimer(animationTime * multiplier);
        context.GoToWaitCallback();
    }

    /// <summary>
    /// Reset variables
    /// </summary>
    public override void Exit()
    {
        animationTime = 10000;
        multiplier = 1;
    }

}
