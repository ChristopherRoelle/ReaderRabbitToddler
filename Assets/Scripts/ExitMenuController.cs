using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Player playerCursor;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dialogClip;

    private CanvasGroup menuOverlayGroup;

    [SerializeField] private bool menuOpen = false;
    private bool menuDialogHasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCursor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menuOverlayGroup = GetComponent<CanvasGroup>();

        CloseMenu();
    }

    private void Update()
    {
        //Allow pressing escape to close the Exit Menu
        if (RRSystem.Instance.IsExitMenuOpen())
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CloseMenu();
            }
        }
    }

    public void OpenMenu()
    {
        //Stop audio if its playing
        StopAudioSource();

        menuOverlayGroup.blocksRaycasts = true;
        menuOverlayGroup.alpha = 1f;
        menuOpen = true;
        RRSystem.Instance.SetExitMenuOpen(menuOpen);

        if (playerCursor != null)
        {
            playerCursor.HideSpriteCursor();
        }

        //Play dialog
        if (audioSource != null && dialogClip != null && !menuDialogHasPlayed)
        {
            audioSource.PlayOneShot(dialogClip);
            menuDialogHasPlayed = true;
        }
    }

    public void CloseMenu()
    {
        //Stop audio if its playing
        StopAudioSource();

        menuDialogHasPlayed = false;
        menuOverlayGroup.blocksRaycasts = false;
        menuOverlayGroup.alpha = 0f;
        menuOpen = false;
        RRSystem.Instance.SetExitMenuOpen(menuOpen);

        if (playerCursor != null && RRSystem.Instance.AreAllMenusClosed())
        {
            playerCursor.ShowSpriteCursor();
        }
    }

    public void StopAudioSource()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
