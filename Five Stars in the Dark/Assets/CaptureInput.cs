using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CaptureInput : MonoBehaviour
{
    static readonly KeyCode[] _keyCodes =
        System.Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k < (int)KeyCode.Mouse0)) //should work with controller button presses too
            .ToArray();

    private bool isChecking = false;
    //public InputKey input; //

    public void setChecking()
    {
        isChecking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChecking && Input.anyKeyDown)
            for (int i = 0; i < _keyCodes.Length; i++)
                if (Input.GetKeyDown(_keyCodes[i])) { }
                    //input = _keyCodes[i];
    }
}
