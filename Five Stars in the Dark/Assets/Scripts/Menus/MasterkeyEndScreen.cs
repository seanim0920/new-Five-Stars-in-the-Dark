using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterkeyEndScreen : MonoBehaviour
{
    public Button retry;
    public Button next;
    public Button menu;
    public static int currentLevelBuildIndex = 1;
    public GameObject loadPanel;
    public GameObject Wipe;

    // Start is called before the first frame update
    void Start()
    {
        retry.onClick.AddListener(() => {
            disableButtons();
            GetComponent<LoadScene>().LoadLevelFromMenuWithoutWipe(currentLevelBuildIndex, 3);
            Wipe.SetActive(true);
        });
        next.onClick.AddListener(() => {
            disableButtons();
            GetComponent<LoadScene>().LoadLevelFromMenuWithoutWipe(currentLevelBuildIndex+1, 3);
            Wipe.SetActive(true);
        });
        menu.onClick.AddListener(() => {
            disableButtons();
            GetComponent<LoadScene>().LoadLevelFromMenuWithoutWipe("Menu", 3);
            Wipe.SetActive(true);
        });
    }

    void disableButtons()
    {
        next.interactable = false;
        retry.interactable = false;
        menu.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
