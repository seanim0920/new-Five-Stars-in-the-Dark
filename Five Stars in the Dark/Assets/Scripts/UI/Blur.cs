using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Blur : MonoBehaviour
{
    PostProcessVolume m_Volume;
    DepthOfField m_Depth;
    private static float blurAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Depth = ScriptableObject.CreateInstance<DepthOfField>();
        m_Depth.enabled.Override(true);
        m_Depth.kernelSize.Override(KernelSize.VeryLarge);
        m_Depth.focalLength.Override(300f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Depth);
        m_Volume.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //max blur amount?
        m_Volume.weight = blurAmount;
        //BlurColor.a = Mathf.Sqrt(blurAmount);
        //SmallColor.a = 1 - blurAmount;
        //VisColor.a = Mathf.Pow(1-blurAmount,2) + 0.5f;
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }

    public static float getAmount()
    {
        return blurAmount;
    }
    public static void setAmount(float newAmount)
    {
        blurAmount = newAmount;
    }
}
