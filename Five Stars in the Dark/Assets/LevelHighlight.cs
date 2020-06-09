using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelHighlight : MonoBehaviour
{
    public Slider slider;
    ColorBlock colorsDefault;
    ColorBlock colorsPassed;
    Button button;
    Text subtitle;
    int childrenAmount;
    int childIndex;
    string[] levels = {"Tutorial", "Level 1", "Level 2", "Level 3", "Level 4", "Level 4.5"};
    // Start is called before the first frame update
    void Start()
    {
        childrenAmount = transform.parent.childCount;
        childIndex = transform.GetSiblingIndex();
        button = GetComponent<Button>();
        subtitle = transform.GetChild(0).GetComponent<Text>();
        colorsDefault = button.colors;
        colorsPassed = button.colors;
        colorsPassed.normalColor = button.colors.pressedColor;
        colorsPassed.highlightedColor = button.colors.pressedColor;

        if (!PlaythroughManager.hasPlayedLevel(levels[childIndex]))
        {
            if (childIndex == 0 || PlaythroughManager.hasPlayedLevel(levels[childIndex-1]))
            {
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/locationincomplete"); //image for incomplete level, make sure this doesnt change
            } else
            {
                button.enabled = false;
                transform.GetChild(1).gameObject.SetActive(true); //black overlay
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectDelegate()
    {
        if (button.enabled)
        {
            print("selected");
            //anims.CrossFade("MenuButton", 0.3f);
            subtitle.enabled = true;
            for (int i = 0; i < childIndex; ++i)
            {
                transform.parent.GetChild(i).GetComponent<Button>().colors = colorsPassed;
            }
            for (int i = childIndex; i < childrenAmount; ++i)
            {
                transform.parent.GetChild(i).GetComponent<Button>().colors = colorsDefault;
            }
            if (childrenAmount <= 0) return;
            slider.value = (float)childIndex / (childrenAmount - 1);
        }
    }

    public void OnDeselectDelegate()
    {
        //anims.CrossFade("Start", 0.3f);
        subtitle.enabled = false;
        StartCoroutine(checkIfBackIsSelectedCoroutine());
    }

    private IEnumerator checkIfBackIsSelectedCoroutine() //will gray out buttons and slider if back is selected. must wait a bit before currentselectedgameobject changes. a lil dirty but whatevs
    {
        yield return new WaitForEndOfFrame();
        if (EventSystem.current.currentSelectedGameObject.CompareTag("Selected"))
        {
            for (int i = 0; i < childrenAmount; ++i)
            {
                transform.parent.GetChild(i).GetComponent<Button>().colors = colorsDefault;
            }
            slider.value = 0;
        }
    }
}
