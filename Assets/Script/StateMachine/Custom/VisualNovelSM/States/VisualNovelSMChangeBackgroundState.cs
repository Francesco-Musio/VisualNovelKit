using UnityEngine;
using UnityEditor;
using StoryManagerNS;
using StateMachine.VisualNovelSM;

public class VisualNovelSMChangeBackgroundState : VisualNovelSMStateBase
{

    /// <summary>
    /// Total animation time
    /// Value got from the story line
    /// </summary>
    private int animationTime = 10000;
    /// <summary>
    /// Internal timer
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// miltiplies time to wait in case two animations needs to happen
    /// </summary>
    private int multiplier = 1;
    /// <summary>
    /// bool to signel when the timer can start
    /// </summary>
    private bool start = false;

    /// <summary>
    /// Get the Current line from the story manager and request to place the actors
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        animationTime = int.Parse(_currentLine.GetData()[1]);
        start = true;
        multiplier = context.ui.ChangeBackground(_currentLine.GetData());

        context.GoToWaitCallback();
    }

    /// <summary>
    /// Reset default state values
    /// </summary>
    public override void Exit()
    {
        start = false;
        timer = 0;
        animationTime = 10000;
    }

}