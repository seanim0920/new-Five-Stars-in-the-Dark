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
    public Button instructionBack;
    public static bool egg = false;
    public static bool lvl = false;
    public static string sceneName = "Level 1";
    public Button tutorial;
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;

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

        start.onClick.AddListener(TaskStart);
        level.onClick.AddListener(TaskLvl);
        levelBack.onClick.AddListener(TaskTitle);
        instructionBack.onClick.AddListener(TaskTitle);

        tutorial.onClick.AddListener(() => sceneName = "Tutorial");
        level1.onClick.AddListener(() => sceneName = "Tutorial");
        level2.onClick.AddListener(() => sceneName = "Level 2");
        level3.onClick.AddListener(() => sceneName = "Level 3");
        level4.onClick.AddListener(() => sceneName = "Level 4");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TaskStart()
    {
        egg = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(play.gameObject);
    }

    void TaskLvl()
    {
        lvl = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(level1.gameObject);
    }

    public void TaskTitle()
    {
        lvl = false;
        egg = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(start.gameObject);
    }
}
