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
            if(menuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        overlayPanel.SetActive(true);
        //menuOverlayGroup.interactable = true;
        menuOverlayGroup.blocksRaycasts = true;
        menuOverlayGroup.alpha = 1f;
        menuOpen = true;
        RRSystem.Instance.SetMenuOpen(menuOpen);

        if (playerCursor != null)
        {
            playerCursor.HideSpriteCursor();
        }
    }

    public void CloseMenu()
    {
        overlayPanel.SetActive(false);
        //menuOverlayGroup.interactable = false;
        menuOverlayGroup.blocksRaycasts = false;
        menuOverlayGroup.alpha = 0f;
        menuOpen = false;
        RRSystem.Instance.SetMenuOpen(menuOpen);

        if (playerCursor != null )
        {
            playerCursor.ShowSpriteCursor();
        }
    }
}
