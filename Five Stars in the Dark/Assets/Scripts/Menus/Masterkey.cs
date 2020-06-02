using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Masterkey : MonoBehaviour
{
    public GameObject[] menuButtons;
    public GameObject[] panels;
    public GameObject mainpanel;
    public Animator wipe;

    private Animator mainpanelAnim;
    private bool preventSpamClick = false;
    private int lastSelectedPanel = 0;
    // Start is called before the first frame update
    void Start()
    {
        menuButtons[lastSelectedPanel].GetComponent<AudioSource>().mute = true;
        EventSystem.current.SetSelectedGameObject(menuButtons[lastSelectedPanel]);
        StartCoroutine(deactivatePanelsCoroutine());
        mainpanelAnim = mainpanel.GetComponent<Animator>();
    }

    public void ReturnToTitle()
    {
        if (preventSpamClick)
        {
            preventSpamClick = false;

            foreach (Button button in panels[lastSelectedPanel].GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }

            menuButtons[lastSelectedPanel].GetComponent<AudioSource>().mute = true;
            wipe.CrossFade("Wipe_Anim_Down", 0.6f);
            mainpanelAnim.CrossFade("PanelIn", 0.6f);
            GameObject.FindWithTag("Selected").GetComponent<Button>().enabled = false;
            EventSystem.current.SetSelectedGameObject(menuButtons[lastSelectedPanel]);
            StartCoroutine(deactivatePanelsCoroutine());
        }
    }
    public void ShowScreen(int panel)
    {
        if (!preventSpamClick)
        {
            preventSpamClick = true;

            wipe.CrossFade("Wipe_Anim_Up", 0.3f);
            mainpanelAnim.CrossFade("PanelOut", 0.3f);
            StopAllCoroutines(); //so the panel isn't disabled if going back immediately after the transition
            panels[panel].SetActive(true);
            lastSelectedPanel = panel;

            foreach (Button button in panels[lastSelectedPanel].GetComponentsInChildren<Button>())
            {
                button.interactable = true;
            }

            //changes the selected gameobject to whichever button is tagged "selected" in the currently active screen
            GameObject.FindWithTag("Selected").GetComponent<Button>().enabled = true;
            EventSystem.current.SetSelectedGameObject(GameObject.FindWithTag("Selected"));
        }
    }
    private IEnumerator deactivatePanelsCoroutine()
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject panel in panels) panel.SetActive(false);
        menuButtons[lastSelectedPanel].GetComponent<AudioSource>().mute = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
