using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject levelMenu;
    public GameObject RocketControls;
    // Start is called before the first frame update
    void Start()
    {
        levelMenu.SetActive(false);
        
    }

    // Update is called once per frame
    public void activateMenu()
    {
        levelMenu.SetActive(true);
        RocketControls.SetActive(false);
    }

    public void deActivateMenu()
    {
        levelMenu.SetActive(false);
        RocketControls.SetActive(true);
    }
}
