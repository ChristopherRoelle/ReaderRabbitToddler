using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RRSystem : MonoBehaviour 
{
    //Singleton
    public static RRSystem Instance { get; private set; }

    private bool menuOpened;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance); //Preserve the system on scene change
        }
        else
        {
            //Remove the duplicate system
            Destroy(gameObject);
        }
    }

    public void SetMenuOpen(bool _menuOpen)
    {
        menuOpened = _menuOpen;
    }

    public bool IsMenuOpen()
    {
        return menuOpened;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
