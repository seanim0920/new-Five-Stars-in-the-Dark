using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MasterkeyFailScreen : MonoBehaviour
{
    public Button retry;
	public Button menu;
    public static string currentLevel = "Level 1";
    public GameObject loadPanel;
    public GameObject Wipe;

    // Start is called before the first frame update
    void Start()
    {
        retry.onClick.AddListener(() => {
            disableButtons();
            Wipe.SetActive(true);
            GetComponent<LoadScene>().LoadLevelFromMenuWithoutWipe(currentLevel, 3);
        });
        menu.onClick.AddListener(() => {
            disableButtons();
            Wipe.SetActive(true);
            GetComponent<LoadScene>().LoadLevelFromMenuWithoutWipe("Menu", 3);
        });
    }

    void disableButtons()
    {
        retry.interactable = false;
        menu.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
