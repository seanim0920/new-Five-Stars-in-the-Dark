using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixedNavigation : MonoBehaviour
{
    public Button buttonBack;
    Button button; 
    Navigation customNav;
    void Start()
    {
        button = GetComponent<Button>();
        customNav = button.navigation;
        customNav.mode = Navigation.Mode.Explicit;
        customNav.selectOnRight = button.FindSelectableOnRight();
        print(button.FindSelectableOnLeft().name);
        customNav.selectOnDown = buttonBack;
        button.navigation = customNav;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
