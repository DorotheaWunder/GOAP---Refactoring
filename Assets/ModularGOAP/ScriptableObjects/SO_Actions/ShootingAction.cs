using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootingAction", menuName = "GOAP/Actions/ShootingAction")]
public class ShootingAction : SO_Action
{
    
    public override bool PerformAction(GOAP_Agent agent)
    {
        //try to stay in shooting range, shoot projectile
        Debug.Log("PENG PENG PENG");
        return true;
    }
}
