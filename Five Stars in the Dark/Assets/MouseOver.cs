using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour
{
    public AudioSource hoverAudio;
    public Animator anims;
    // Start is called before the first frame update
    void Start()
    {
        //anims = GetComponent<Animator>();
    }

    public void OnPointerEnterDelegate()
    {
        if (GetComponent<Selectable>().interactable)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExitDelegate()
    {
        if (GetComponent<Selectable>().interactable)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSelectDelegate()
    {
        //anims.CrossFade("MenuButton", 0.3f);
        hoverAudio.Play();
    }

    public void OnDeselectDelegate()
    {
        hoverAudio.mute = false;
        //anims.CrossFade("Start", 0.3f);
    }

    public void printCurrent()
    {
        StartCoroutine(printCoroutine());
    }

    private IEnumerator printCoroutine() //will gray out buttons and slider if back is selected. must wait a bit before currentselectedgameobject changes. a lil dirty but whatevs
    {
        yield return new WaitForEndOfFrame();
        print(EventSystem.current.currentSelectedGameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
