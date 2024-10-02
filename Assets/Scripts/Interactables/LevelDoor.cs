using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour, IInteractableObject
{
    [SerializeField] private bool useWaitStars = true;
    [SerializeField] private bool isClickable = true;
    [SerializeField] private string levelToLoad;

    [SerializeField] private GameObject playerCursor;

    public bool UseWaitStars { get => useWaitStars; set => useWaitStars = value; }
    public bool IsClickable { get => isClickable; set => isClickable = value; }

    public void Start()
    {
        playerCursor = GameObject.FindGameObjectWithTag("Player");
    }

    public void DoAction()
    {
        Debug.Log($"Level Door: {levelToLoad}");
    }
}
