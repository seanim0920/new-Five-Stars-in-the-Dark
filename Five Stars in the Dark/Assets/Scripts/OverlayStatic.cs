using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class OverlayStatic : MonoBehaviour
{
    private Image noise;
    private PostProcessVolume quickStaticVolume;
    private Grain m_Grain;
    private static bool overlayOn = false;
    // Start is called before the first frame update
    void Start()
    {
        noise = GetComponent<Image>();
        // Create an instance of a vignette
        m_Grain = ScriptableObject.CreateInstance<Grain>();
        m_Grain.colored.Override(false);
        m_Grain.lumContrib.Override(0.2f);
        m_Grain.size.Override(3f);
        m_Grain.enabled.Override(true);
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        quickStaticVolume = PostProcessManager.instance.QuickVolume(4, 100f, m_Grain);
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(quickStaticVolume, true, true);
    }

    private void Update()
    {
        noise.enabled = overlayOn;
        if (overlayOn) m_Grain.intensity.Override(0.9f);
        else m_Grain.intensity.Override(0f);
    }

    public static void turnOnStatic()
    {
        overlayOn = true;
    }

    public static void turnOffStatic()
    {
        overlayOn = false;
    }
}
