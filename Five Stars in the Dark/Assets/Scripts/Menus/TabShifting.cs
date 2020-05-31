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
	
	public Button Graphics_Screen;
	public Button Graphics_Res;
	
	public Slider m;
	public Slider s;
	public Slider d;
	public Slider rc;
	
	public Toggle Keyboard;
	public Toggle Gamepad;
	public Toggle Wheel;
	
	Image vr;
	Image rr;
	Image cr;
	
    // Start is called before the first frame update
    void Start()
    {
        Volume.onClick.AddListener(TaskVol);
		Resolution.onClick.AddListener(TaskRes);
		Controls.onClick.AddListener(TaskCon);
		
		Keyboard.onValueChanged.AddListener(delegate { TaskResetHighlight();});
		Gamepad.onValueChanged.AddListener(delegate { TaskResetHighlight();});
		Wheel.onValueChanged.AddListener(delegate { TaskResetHighlight();});
		
		Graphics_Screen.onClick.AddListener(TaskResetHighlight);
		Graphics_Res.onClick.AddListener(TaskResetHighlight);

		SetOnOffText();
		
		Sub.onClick.AddListener(TaskSub);
		
		vr = Volume.GetComponent<Image>();
		rr = Resolution.GetComponent<Image>();
		cr = Controls.GetComponent<Image>();
		
		m.onValueChanged.AddListener (delegate { TaskResetHighlight();});
		s.onValueChanged.AddListener (delegate { TaskResetHighlight();});
		d.onValueChanged.AddListener (delegate { TaskResetHighlight();});
		rc.onValueChanged.AddListener (delegate { TaskResetHighlight();});
		
		flag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == 1) {
			
			vr.color = new Color32(99,90,90,255);
			cr.color = new Color32(255,255,255,255);
			rr.color = new Color32(255,255,255,255);
		}
		else if (flag == 2) {
			
			rr.color = new Color32(99,90,90,255);
			cr.color = new Color32(255,255,255,255);
			vr.color = new Color32(255,255,255,255);
		}
 	    else if (flag == 3) {
			
			cr.color = new Color32(99,90,90,255);
			vr.color = new Color32(255,255,255,255);
			rr.color = new Color32(255,255,255,255);
	    }
		
    }
	
	void TaskVol() {
		flag = 1;
		EventSystem.current.SetSelectedGameObject(null);
	}
	
	void TaskRes() {
		flag = 2;
		EventSystem.current.SetSelectedGameObject(null);
	}
	
	void TaskCon() {
		flag = 3;
		EventSystem.current.SetSelectedGameObject(null);
	}
	
	void TaskResetHighlight() {
		EventSystem.current.SetSelectedGameObject(null);
	}
	
	void TaskSub() {
		EventSystem.current.SetSelectedGameObject(null);
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
