using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForCreditsToEnd : MonoBehaviour
{
    private bool showEnding;
    public Animator credits;
    public GameObject blackOverlay;
    public GameObject ending1;
    public GameObject ending2;
    public GameObject endText;
    public GameObject wipe;
    // Start is called before the first frame update
    void Start()
    {
        blackOverlay.GetComponent<Image>().canvasRenderer.SetAlpha(0);
        blackOverlay.SetActive(true);
    }

    private void Update()
    {
        if (credits.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !credits.IsInTransition(0) && !showEnding)
        {
            showEnding = true;
            StartCoroutine(ShowEndingAfterCredits());
        }
    }

    IEnumerator ShowEndingAfterCredits()
    {
        blackOverlay.GetComponent<Image>().CrossFadeAlpha(1.0f, 3, true);
        yield return new WaitForSeconds(3);
        if (PlaythroughManager.hasPlayedLevel("SabotageEnding"))
            ending1.SetActive(true);
        else if (PlaythroughManager.hasPlayedLevel("ExplosionEnding"))
            ending2.SetActive(true);
        endText.SetActive(true);
        blackOverlay.GetComponent<Image>().CrossFadeAlpha(0.0f, 3, true);
        yield return new WaitForSeconds(8);
        wipe.SetActive(true);
    }
}
