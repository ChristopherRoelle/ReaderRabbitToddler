using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POP : MonoBehaviour, IInteractableObject
{
    [SerializeField] private bool useWaitStars = false;
    [SerializeField] private bool isClickable = true;
    
    [SerializeField] private Player playerCursor;
    [SerializeField] private MenuController menuObject;

    [SerializeField] private GameObject speechBubble;
    [SerializeField] private float speechBubbleDelay = 0.25f;

    private Coroutine speechBubbleRoutine;

    public bool UseWaitStars { get => useWaitStars; set => useWaitStars = value; }
    public bool IsClickable { get => isClickable; set => isClickable = value; }

    public void Start()
    {
        playerCursor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        speechBubble.SetActive(false);
    }

    public void ShowSpeechBubble() {
        //Only open if the coroutine isnt running and all menus are closed
        if (speechBubbleRoutine == null && RRSystem.Instance.AreAllMenusClosed())
        {
            speechBubbleRoutine = StartCoroutine(WaitForSpeechBubble());
        }
    }
    public void HideSpeechBubble() {

        if (speechBubbleRoutine != null)
        {
            StopCoroutine(WaitForSpeechBubble());
            speechBubbleRoutine = null;
        }
        speechBubble.SetActive(false); 
    }

    private IEnumerator WaitForSpeechBubble()
    {
        yield return new WaitForSeconds(speechBubbleDelay);
        speechBubble.SetActive(true);
    }

    public void DoAction()
    {
        //Turn off the active wait stars
        if(playerCursor != null) { playerCursor.ResetWaitStars(); }

        if (menuObject != null)
        {
            menuObject.OpenMenu();
            HideSpeechBubble();
        }
    }
}
