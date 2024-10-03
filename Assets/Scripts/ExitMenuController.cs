using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuController : MonoBehaviour
{
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

    public void OpenMenu()
    {
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
        menuOverlayGroup.blocksRaycasts = false;
        menuOverlayGroup.alpha = 0f;
        menuOpen = false;
        RRSystem.Instance.SetMenuOpen(menuOpen);

        if (playerCursor != null)
        {
            playerCursor.ShowSpriteCursor();
        }
    }
}
