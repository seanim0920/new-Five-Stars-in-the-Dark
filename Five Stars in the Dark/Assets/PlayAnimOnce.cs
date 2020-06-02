using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnce : MonoBehaviour
{
    public Masterkey ScreenManager;
    public Animator[] titleAnim;
    public static bool titleAnimPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        titleAnim = GetComponentsInChildren<Animator>();
        disableAnims();
        titleAnimPlayed = true;
    }

    void disableAnims()
    {
        if (titleAnimPlayed)
        {
            print("play wipe!");
            foreach (Animator anim in titleAnim)
            {
                anim.enabled = false;
            }
            GetComponent<Animator>().enabled = true;
            ScreenManager.ReturnToTitle();
        }
    }

    private void OnEnable()
    {
        disableAnims();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
