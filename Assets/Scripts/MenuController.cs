using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Player playerCursor;

    private CanvasGroup menuOverlayGroup;
    
    [SerializeField] private bool menuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCursor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menuOverlayGroup = GetComponent<CanvasGroup>();
        CloseMenu();
    }

    private void Update()
    {
        //Check for input on the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Make sure that the exit menu isnt opened.
            if (!RRSystem.Instance.IsExitMenuOpen())
            {
                if (menuOpen)
                {
                    CloseMenu();
                }
                else
                {
                    OpenMenu();
                }
            }
        }
    }

    public void OpenMenu()
    {
        //Dont allow this window to open if the exit menu is up already.
        if (!RRSystem.Instance.IsExitMenuOpen())
        {
            overlayPanel.SetActive(true);
            //menuOverlayGroup.interactable = true;
            menuOverlayGroup.blocksRaycasts = true;
            menuOverlayGroup.alpha = 1f;
            menuOpen = true;
            RRSystem.Instance.SetMainMenuOpen(menuOpen);

            if (playerCursor != null)
            {
                playerCursor.HideSpriteCursor();
            }
        }
    }

    public void CloseMenu()
    {
        overlayPanel.SetActive(false);
        //menuOverlayGroup.interactable = false;
        menuOverlayGroup.blocksRaycasts = false;
        menuOverlayGroup.alpha = 0f;
        menuOpen = false;
        RRSystem.Instance.SetMainMenuOpen(menuOpen);

        if (playerCursor != null && RRSystem.Instance.AreAllMenusClosed())
        {
            playerCursor.ShowSpriteCursor();
        }
    }
}
