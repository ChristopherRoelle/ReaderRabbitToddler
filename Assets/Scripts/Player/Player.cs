using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
     
    Vector2 mousePosition = Vector2.zero;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject cursorObject;

    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        InitializeMouse();
    }

    // Update is called once per frame
    void Update()
    {
        PollMousePosition();
    }

    void InitializeMouse()
    {
        //Disable the cursor object if it doesnt exist
        if (cursorObject == null)
        {
            Debug.Log("Cursor - Object not set!");
            Cursor.visible = true;
        }
        else
        {
            //Hide the cursor
            Cursor.visible = false;
        }
    }

    void PollMousePosition()
    {
        mousePosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        if(cursorObject != null)
        {
            //Get the size of the cursor object in world space
            float cursorWidthInViewport = (cursorObject.GetComponent<SpriteRenderer>().bounds.size.x / 2f) / Screen.width;
            float cursorHeightInViewport = (cursorObject.GetComponent<SpriteRenderer>().bounds.size.y / 2f) / Screen.height;

            //Ensure that the mousePosition is within the screen space
            mousePosition.x = Mathf.Clamp(mousePosition.x, cursorWidthInViewport, 1f - cursorWidthInViewport);
            mousePosition.y = Mathf.Clamp(mousePosition.y, cursorHeightInViewport, 1f - cursorHeightInViewport);

            Vector2 clampedWorldPosition = mainCamera.ViewportToWorldPoint(mousePosition);

            //Move cursor object
            cursorObject.transform.position = clampedWorldPosition;
        }
    }
}
