using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterkeyFailScreen : MonoBehaviour
{
    public Button retry;
	public Button menu;
    public static string currentLevel = "Level 1";
    public GameObject loadPanel;
	
    // Start is called before the first frame update
     void Start()
    {		
		retry.onClick.AddListener(() => {
            loadPanel.SetActive(true);
            LoadScene.LoadLevelAsyncByName(currentLevel);
        });
		menu.onClick.AddListener(() => LoadScene.Loader("Menu"));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
