using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadCurrentLevelFromInstructions : MonoBehaviour
{
    public LoadScene localLoadScene;
    int beginningIndex = 3;
    int endIndex = 13;
    int sceneToLoad = 3;
    // Start is called before the first frame update
    void Start()
    {
        for (sceneToLoad = beginningIndex; sceneToLoad <= endIndex; ++sceneToLoad)
        {
            if (!PlaythroughManager.hasPlayedLevel(sceneToLoad))
            {
                --sceneToLoad;
                if (sceneToLoad <= 3) sceneToLoad = 3;
                break;
            }
        }

        Debug.Log("start will load scene " + sceneToLoad);

        GetComponent<Button>().onClick.AddListener(() => {
            localLoadScene.LoadLevelFromMenu(sceneToLoad);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
