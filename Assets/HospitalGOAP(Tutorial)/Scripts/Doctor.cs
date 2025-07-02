using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : GAgent
{
    void Start()
    {
        SubGoal s1 = new SubGoal("rested", 1, false);
        goals.Add(s1,1);
        
        Invoke("GetTired", Random.Range(10,20));
    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke("GetTired", Random.Range(10,20));
    }
}
