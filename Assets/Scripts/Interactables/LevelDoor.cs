using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour, IInteractableObject
{
    [SerializeField] private bool useWaitStars = true;
    [SerializeField] private bool isClickable = true;
    [SerializeField] private string levelToLoad;

    [SerializeField] private Player playerCursor;

    public bool UseWaitStars { get => useWaitStars; set => useWaitStars = value; }
    public bool IsClickable { get => isClickable; set => isClickable = value; }

    public void Start()
    {
        playerCursor = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void DoAction()
    {
        //Turn off the active wait stars
        if (playerCursor != null) { playerCursor.ResetWaitStars(); }

        Debug.Log($"Level Door: {levelToLoad}");
    }
}
