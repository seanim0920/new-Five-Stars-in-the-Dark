using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTutorialIfPlayerNew : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            if (PlaythroughManager.hasPlayedLevel(PlaythroughManager.currentLevelIndex + 1))
            {
                transform.parent.parent.GetComponent<LoadScene>().LoadLevelFromMenu("Menu");
            } else
            {
                transform.parent.parent.GetComponent<LoadScene>().LoadLevelFromMenu("Tutorial");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
