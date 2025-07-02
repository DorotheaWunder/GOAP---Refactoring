using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class GoHomeRef : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        Destroy(this.gameObject);
        return true;
    }
}
