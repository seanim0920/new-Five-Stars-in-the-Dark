using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAnimation : MonoBehaviour
{
    public Image panel;
    Sprite[] frames;
    float waitTime = 0.07f;
    // Start is called before the first frame update
    void Start()
    {
        frames = Resources.LoadAll<Sprite>("LoadingGif");
        print("frames amount " + frames.Length);
        StartCoroutine(loopFrames());
    }

    IEnumerator loopFrames()
    {
        int i = 0;
        while (true)
        {
            i = i + 1;
            if (i >= frames.Length) i = 0;
            panel.sprite = frames[i];
            yield return new WaitForSeconds(waitTime);
            print(frames[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
