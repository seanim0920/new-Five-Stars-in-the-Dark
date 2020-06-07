using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class subtitleText : MonoBehaviour
{
    private static Text subText;
    private bool italics = false;
    private static bool oride = false;
    // Start is called before the first frame update
    void Start()
    {
        ConstructLevelFromMarkers.subtitleMessage = "";
        subText = GetComponent<Text>();
        oride = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!oride)
        {
            if (SettingsManager.toggles[3])
            {
                if (!subText.enabled)
                {
                    subText.enabled = true;
                }
                if(ConstructLevelFromMarkers.subtitleMessage.Length > 0)
                {
                    if (char.ToLower(ConstructLevelFromMarkers.subtitleMessage[2]) == 'i')
                    {
                        if (!string.Equals(subText.text, ConstructLevelFromMarkers.subtitleMessage.Substring(5).Trim('"')))
                        {
                            
                            // Debug.Break();
                            subText.fontStyle = italics ? FontStyle.Italic : FontStyle.Normal;
                            subText.text = matchColorandTrimQuotes(ConstructLevelFromMarkers.subtitleMessage);
                            Debug.Log("subtitle time: " + ConstructLevelFromMarkers.levelDialogue.time);
                            // Debug.Log("subText: " + subText.text);
                            // Debug.Log("markers text: " + ConstructLevelFromMarkers.subtitleMessage.Substring(5).Trim('"'));
                        }
                    }
                    else
                    {
                        if (!string.Equals(subText.text, ConstructLevelFromMarkers.subtitleMessage.Substring(4).Trim('"')))
                        {
                            
                            // Debug.Break();
                            subText.fontStyle = italics ? FontStyle.Italic : FontStyle.Normal;
                            subText.text = matchColorandTrimQuotes(ConstructLevelFromMarkers.subtitleMessage);
                            Debug.Log("subtitle time: " + ConstructLevelFromMarkers.levelDialogue.time);
                            // Debug.Log("subText: " + subText.text);
                            // Debug.Log("markers text: " + ConstructLevelFromMarkers.subtitleMessage.Substring(4).Trim('"'));
                        }
                    }
                }
                
                if (!ConstructLevelFromMarkers.levelDialogue.isPlaying)
                {
                    subText.text = "";
                }
            }
            else
            {
                subText.enabled = false;
            }
        }
    }

    public static IEnumerator changeSubtitleCoroutine(string data, float duration)
    {
        oride = true;
        subText.text = data;
        yield return new WaitForSeconds(duration);
        subText.text = "";
        oride = false;
    }

    string matchColorandTrimQuotes(string message)
    {
        if (message.Length >= 5)
        {
            //string[] tokens = message.Trim().Split(new char[0], System.StringSplitOptions.RemoveEmptyEntries);
            if (message[0] == '<' && char.ToLower(message[1]) == 'y')
            {
                subText.color = Color.yellow;
            }
            else
            {
                subText.color = Color.white;
            }
            if (char.ToLower(message[2]) == 'i')
            {
                italics = true;
                return message.Substring(5).Trim('"');
            }
            else
            {
                italics = false;
                return message.Substring(4).Trim('"');
            }
        }
        return "";
    }
}