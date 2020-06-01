using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Masterkey : MonoBehaviour
{
    public Animator[] titleAnim;
    public Button start;
    public Button play;
    public Button level;
    public Button levelBack;
    public Button audioTab;
    public static bool egg = false;
    public static bool lvl = false;
    public static bool settings = false;
    public static string sceneName = "Tutorial";
    public Button tutorial;
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;

	public static bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        titleAnim = transform.parent.GetComponentsInChildren<Animator>();
        if (played)
            foreach (Animator anim in titleAnim)
            {
                anim.enabled = false;
            }
        played = true;

        start.onClick.AddListener(PrepareInstructions);
        level.onClick.AddListener(PrepareLevelSelect);
        levelBack.onClick.AddListener(PrepareTitle);

        tutorial.onClick.AddListener(() => sceneName = "Tutorial");
        level1.onClick.AddListener(() => sceneName = "Level 1");
        level2.onClick.AddListener(() => sceneName = "Level 2");
        level3.onClick.AddListener(() => sceneName = "Level 3");
        level4.onClick.AddListener(() => sceneName = "Level 4");
        level5.onClick.AddListener(() => sceneName = "Level 4.5");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PrepareInstructions()
    {
        egg = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(play.gameObject);
    }

    void PrepareLevelSelect()
    {
        lvl = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(tutorial.gameObject);
    }

    public void PrepareSettings()
    {
        settings = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioTab.gameObject);
    }

    public void PrepareTitle()
    {
        egg = false;
        lvl = false;
        settings = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(start.gameObject);
    }
}
