using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnMenu : MonoBehaviour
{
	public Button skip;
    // Start is called before the first frame update
    void Start()
    {
        skip.onClick.AddListener(TaskSkip);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
		if(transform.position.x <= -3000) {
			SceneManager.LoadScene("Menu");
			
		}
    }
	
	void TaskSkip(){
		SceneManager.LoadScene("Menu");
	}
}
