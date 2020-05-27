using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class buttonPress : MonoBehaviour
{
    public Button instructionsButton;
    public Button playButton;
    public bool state = false;
    public bool pressed = false;
    string[] activeForceAndEffect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SteeringWheelInput.checkConnected())
        {
            LogitechGSDK.LogiPlaySpringForce(0, 0, 70, 25);
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            if (rec.lY < 0 && !pressed)
            {
                pressed = true;
                if (!state)
                {
                    instructionsButton.onClick.Invoke();
                    state = true;
                }
                else playButton.onClick.Invoke();
            }
            else
            {
                pressed = false;
            }
        }
    }

    void OnApplicationQuit()
    {
    }
}
