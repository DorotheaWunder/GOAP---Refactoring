using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    public Text states;

    void LateUpdate()
    {
        System.Collections.Generic.Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";

        foreach (System.Collections.Generic.KeyValuePair<string, int> s in worldstates)
        {
            states.text += s.Key + ", " + s.Value + "\n";
        }
    }
}
