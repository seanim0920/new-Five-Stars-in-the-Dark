using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabShifting : MonoBehaviour
{
	//1 Volume, 2 Resolution, 3 Controls
	public int flag = 1;
	
	public Button Volume;
	public Button Resolution;
	public Button Controls;
	
	public Button Sub;
	public Text onOff;

    public GameObject backButton;
    public GameObject volSel;
    public GameObject resSel;

    public Toggle Keyboard;
	public Toggle Gamepad;
	public Toggle Wheel;
	
    // Start is called before the first frame update
    void Start()
    {
        Volume.onClick.AddListener(TaskVol);
		Resolution.onClick.AddListener(TaskRes);
		Controls.onClick.AddListener(TaskCon);

		SetOnOffText();
		
		Sub.onClick.AddListener(TaskSub);
		
		flag = 1;
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	void TaskVol()
    {
        Volume.enabled = false;
        Resolution.enabled = true;
        Controls.enabled = true;
        EventSystem.current.SetSelectedGameObject(volSel);
    }
	
	void TaskRes()
    {
        Volume.enabled = true;
        Resolution.enabled = false;
        Controls.enabled = true;
        EventSystem.current.SetSelectedGameObject(resSel);
    }
	
	void TaskCon()
    {
        Volume.enabled = true;
        Resolution.enabled = true;
        Controls.enabled = false;
        EventSystem.current.SetSelectedGameObject(backButton);
    }
	
	void TaskSub() {
		SetOnOffText();
	}

	void SetOnOffText()
	{
		if(SettingsManager.toggles[3])
			onOff.text = "ON";
		else
			onOff.text = "OFF";
	}
}
