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

    // Start is called before the first frame update
    void Start()
    {
        retry.onClick.AddListener(() => {
            loadPanel.SetActive(true);
            LoadScene.LoadLevelAsyncByBuildIndex(currentLevelBuildIndex);
        });
        next.onClick.AddListener(() => {
            loadPanel.SetActive(true);
            LoadScene.LoadLevelAsyncByBuildIndex(currentLevelBuildIndex + 1);
        });
        menu.onClick.AddListener(() => LoadScene.Loader("Menu"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
