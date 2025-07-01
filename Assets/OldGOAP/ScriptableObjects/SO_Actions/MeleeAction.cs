using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAction", menuName = "GOAP/Actions/MeleeAction")]
public class MeleeAction : SO_Action
{
    public override bool PerformAction(GOAP_Agent agent)
    {
        Debug.Log("SLISH-SLASH");
        

        //------- wait until animation is done
        return true;
    }
}
