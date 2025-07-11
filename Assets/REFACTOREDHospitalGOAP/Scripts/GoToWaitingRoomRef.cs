using UnityEngine;

public class GoToWaitingRoomRef : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
        GWorld.Instance.AddPatient(this.gameObject);
        beliefs.ModifyState("atHospital", 1);
        return true;
    }
}
