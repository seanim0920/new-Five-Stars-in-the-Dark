using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Masterkey : MonoBehaviour
{
    public AudioSource transitionSfx;
    public GameObject[] menuButtons;
    public GameObject[] panels;
    public GameObject mainpanel;
    public Animator wipe;

    public Animator mainpanelAnim;
    private bool preventSpamClick = true; //set to true by default so when returning to menu the wipe animation can play
    private int lastSelectedPanel = 0;
    private bool panelsDeactivated = true;
    // Start is called before the first frame update
    void Start()
    {
        menuButtons[lastSelectedPanel].GetComponent<AudioSource>().mute = true;
        EventSystem.current.SetSelectedGameObject(menuButtons[lastSelectedPanel]);
        StartCoroutine(deactivatePanelsCoroutine());
        preventSpamClick = false;
    }

    public void ReturnToTitle()
    {
        if (preventSpamClick)
        {
            print("should play wipe");
            preventSpamClick = false;

            //plays whoosh backwards
            transitionSfx.pitch = -0.8f;
            transitionSfx.timeSamples = transitionSfx.clip.samples - 2;
            transitionSfx.Play();

            foreach (Button button in panels[lastSelectedPanel].GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }
            foreach (Button button in mainpanel.GetComponentsInChildren<Button>())
            {
                button.interactable = true;
            }

            foreach (Image image in mainpanel.GetComponentsInChildren<Image>()) image.enabled = true;
            StopAllCoroutines(); //so the panel isn't disabled if going back immediately after the transition

            menuButtons[lastSelectedPanel].GetComponent<AudioSource>().mute = true;
            wipe.CrossFade("Wipe_Anim_Down", 0.6f);
            mainpanelAnim.CrossFade("PanelIn", 0.6f);
            if (GameObject.FindWithTag("Selected"))
                GameObject.FindWithTag("Selected").GetComponent<Button>().enabled = false;
            EventSystem.current.SetSelectedGameObject(menuButtons[lastSelectedPanel]);
            StartCoroutine(deactivatePanelsCoroutine());
        }
    }
    public void ShowScreen(int panel)
    {
        if (!preventSpamClick && panelsDeactivated)
        {
            preventSpamClick = true;
            panelsDeactivated = false;

            //plays whoosh
            transitionSfx.pitch = 1;
            transitionSfx.time = 0;
            transitionSfx.Play();

            wipe.CrossFade("Wipe_Anim_Up", 0.3f);
            mainpanelAnim.CrossFade("PanelOut", 0.3f);
            StopAllCoroutines(); //so the panel isn't disabled if going back immediately after the transition
            panels[panel].SetActive(true);
            lastSelectedPanel = panel;

            foreach (Button button in panels[lastSelectedPanel].GetComponentsInChildren<Button>())
            {
                button.interactable = true;
            }
            foreach (Button button in mainpanel.GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }

            //changes the selected gameobject to whichever button is tagged "selected" in the currently active screen
            GameObject.FindWithTag("Selected").GetComponent<Button>().enabled = true;
            EventSystem.current.SetSelectedGameObject(GameObject.FindWithTag("Selected"));
            StartCoroutine(deactivateMainPanelCoroutine());
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    private IEnumerator deactivatePanelsCoroutine()
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject panel in panels) panel.SetActive(false);
        menuButtons[lastSelectedPanel].GetComponent<AudioSource>().mute = false;
        panelsDeactivated = true;
    }

    private IEnumerator deactivateMainPanelCoroutine()
    {
        yield return new WaitForSeconds(2);
        foreach (Image image in mainpanel.GetComponentsInChildren<Image>()) image.enabled = false;
    }
}
