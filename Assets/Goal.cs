using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goal", menuName = "ScriptableObjects/Goal", order = 3)]
public class Goal : ScriptableObject
{
    public string _name;
    public string description;

    
}

public abstract class GoalLogic 
{
    public abstract void Start();

    public abstract bool FailCondition();

    public abstract void Reward();
}

public class Goal_IsProfitLower : GoalLogic
{
    public override void Start() 
    {
        TimeController.instance.OnFinishDay += OnFinishDay;
    }

    void OnFinishDay()
    {
        if (FailCondition())


        TimeController.instance.OnFinishDay -= OnFinishDay;
    }

    public override bool FailCondition()
    {
        var a = Investiments.instance.GetTotalInvested();
        var b = Investiments.instance.GetTotalValueInvested();

        return a - b < 4;
    }

    public override void Reward()
    {

    }
}
