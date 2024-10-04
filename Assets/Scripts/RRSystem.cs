using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RRSystem : MonoBehaviour 
{
    //Singleton
    public static RRSystem Instance { get; private set; }

    private bool mainMenuOpened;
    private bool exitMenuOpened;

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

    public void SetMainMenuOpen(bool _menuOpen)
    {
        mainMenuOpened = _menuOpen;
    }

    public bool IsMainMenuOpen()
    {
        return mainMenuOpened;
    }

    public void SetExitMenuOpen(bool _exitMenuOpen)
    {
        exitMenuOpened = _exitMenuOpen;
    }

    public bool IsExitMenuOpen()
    {
        return exitMenuOpened;
    }

    /// <summary>
    /// Checks if any Menus are open
    /// </summary>
    /// <returns>
    /// True - All Menus are closed. 
    /// False - One of the Menus are open.
    /// </returns>
    public bool AreAllMenusClosed()
    {
        return !mainMenuOpened && !exitMenuOpened;
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();

        //Stop the game if in Debug
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
