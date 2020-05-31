using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnce : MonoBehaviour
{
    public Animator[] titleAnim;
    public static bool titleAnimPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        titleAnim = transform.parent.GetComponentsInChildren<Animator>();
        disableAnims();
        titleAnimPlayed = true;
    }

    void disableAnims()
    {
        if (titleAnimPlayed)
            foreach (Animator anim in titleAnim)
            {
                anim.enabled = false;
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
