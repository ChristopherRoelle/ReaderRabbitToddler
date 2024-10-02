using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverlayClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuController menuController;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (menuController != null)
        {
            if (eventData.pointerEnter == this.gameObject)
            {
                menuController.CloseMenu();
            }
        }
        else
        {
            Debug.LogError("OverlayClickHandler::Missing Reference <MenuController>");
        }
    }
}
