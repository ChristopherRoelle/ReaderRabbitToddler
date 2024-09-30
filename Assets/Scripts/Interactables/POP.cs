using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POP : MonoBehaviour, IInteractableObject
{
    [SerializeField] private bool useWaitStars = false;
    [SerializeField] private bool isClickable = true;

    [SerializeField] private GameObject menuObject;

    public bool UseWaitStars { get => useWaitStars; set => useWaitStars = value; }
    public bool IsClickable { get => isClickable; set => isClickable = value; }

    public void DoAction()
    {
        if (menuObject != null)
        {
            menuObject.SetActive(!menuObject.activeSelf);
        }
    }
}
