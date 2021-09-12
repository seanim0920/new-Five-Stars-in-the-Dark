using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class OverlayStatic : MonoBehaviour
{
    private static Image noise;
    private PostProcessVolume quickStaticVolume;
    private static Grain m_Grain;
    // Start is called before the first frame update
    void Start()
    {
        noise = GetComponent<Image>();
        // Create an instance of a vignette
        m_Grain = ScriptableObject.CreateInstance<Grain>();
        m_Grain.lumContrib.Override(1f);
        m_Grain.size.Override(2f);
        m_Grain.enabled.Override(true);
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        quickStaticVolume = PostProcessManager.instance.QuickVolume(4, 100f, m_Grain);
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(quickStaticVolume, true, true);
    }

    public static void turnOnStatic()
    {
        m_Grain.intensity.Override(1f);
        noise.enabled = true;
    }

    public static void turnOffStatic()
    {
        m_Grain.intensity.Override(0f);
        noise.enabled = false;
    }
}
