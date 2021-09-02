using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class subtitleText : MonoBehaviour
{
    private static Text subText;
    private string lastMessage = "";
    private static float changeDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        ConstructLevelFromMarkers.subtitleMessage = "";
        subText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeDuration <= 0)
        {
            if (SettingsManager.toggles[3])
            {
                if (!subText.enabled)
                {
                    subText.enabled = true;
                }
                if (ConstructLevelFromMarkers.subtitleMessage.Length >= 5)
                {
                    styleAndTrimQuotes(ConstructLevelFromMarkers.subtitleMessage);
                } else
                {
                    subText.text = "";
                }

                if (ConstructLevelFromMarkers.levelDialogue != null && !ConstructLevelFromMarkers.levelDialogue.isPlaying)
                {
                    subText.text = "";
                }
            }
            else
            {
                subText.enabled = false;
            }
        } else
        {
            changeDuration -= Time.deltaTime;
        }
    }

    public static void changeSubtitle(string data, float duration)
    {
        subText.text = data;
        changeDuration = duration;
    }

    void styleAndTrimQuotes(string message)
    {
        if (string.Equals(lastMessage, message)) return;
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
            subText.fontStyle = FontStyle.Italic;
            subText.text = message.Substring(5).Trim('"');
        }
        else
        {
            subText.fontStyle = FontStyle.Normal;
            subText.text = message.Substring(4).Trim('"');
        }
        lastMessage = message;
    }
}