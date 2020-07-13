using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private GameObject wipe;
    private void Start()
    {
        wipe = Resources.Load<GameObject>("Prefabs/Menus/Wipe");
    }
    public void LoadLevelFromMenu(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        StartCoroutine(loadAfterWipe(load));
    }

    public void LoadLevelFromMenu(int buildIndex)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
        StartCoroutine(loadAfterWipe(load));
    }
    public void LoadLevelFromMenuWithoutWipe(int buildIndex, float wait)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
        StartCoroutine(loadAfterWait(load, wait));
    }
    public void LoadLevelFromMenuWithoutWipe(string sceneName, float wait)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        StartCoroutine(loadAfterWait(load, wait));
    }

    IEnumerator loadAfterWipe(AsyncOperation load)
    {
        load.allowSceneActivation = false;
        GameObject transition = Instantiate(wipe, wipe.transform.position, wipe.transform.rotation, GameObject.FindGameObjectWithTag("Canvas").transform);
        transition.GetComponent<Animator>().Play("Wipe_Anim_Down");
        while (transition.GetComponent<RectTransform>().anchoredPosition.x < -790)
        {
            AudioListener.volume *= 0.75f;
            yield return new WaitForSeconds(0);
        }
        AudioListener.volume *= 0f;

        transition.GetComponent<Animator>().speed = 0;
        yield return new WaitForSeconds(1);
        foreach (AudioSource audio in GameObject.FindObjectsOfType<AudioSource>())
        {
            audio.volume = 0;
        }
        AudioListener.volume = 1f;
        load.allowSceneActivation = true;
    }

    IEnumerator loadAfterWait(AsyncOperation load, float wait)
    {
        load.allowSceneActivation = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(wait);
        load.allowSceneActivation = true;
    }

    public static AsyncOperation Loader(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public static AsyncOperation Loader(int buildIndex)
    {
        return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
    }

    public static AsyncOperation LoadNextScene()
    {
        return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public static AsyncOperation LoadNextSceneWithoutWipe()
    {
        return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
