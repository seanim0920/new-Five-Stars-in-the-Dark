using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour
{
    public AudioSource audio;
    public Animator anims;
    // Start is called before the first frame update
    void Start()
    {
        anims = GetComponent<Animator>();
    }

    public void OnPointerEnterDelegate()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnSelectDelegate()
    {
        //anims.CrossFade("MenuButton", 0.3f);
        audio.Play();
    }

    public void OnDeselectDelegate()
    {
        //anims.CrossFade("Start", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
