using UnityEngine;

public class RestRef : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("exhausted");
        return true;
    }
}
