using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableObject
{
    bool UseWaitStars { get; set; }
    bool IsClickable { get; set; }

    public void DoAction();

}
