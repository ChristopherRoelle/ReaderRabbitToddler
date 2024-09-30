using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    float waitStarDelayInSeconds = 0.5f;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject cursorObject;    
    
    IInteractableObject collisionObjectInterface;
    Vector2 mousePosition = Vector2.zero;

    bool mouseCursorFound = false;
    Collider2D cursorColider;
    List<SpriteRenderer> waitStars;

    private Coroutine starCoroutine;
    private bool allowWaitStars = true;

    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        InitializeMouse();

        //Get Wait Stars if they exist
        if(mouseCursorFound)
        {
            GetWaitStars();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PollMousePosition();

        //Check for mouse click
        if (Input.GetMouseButtonDown(0) && collisionObjectInterface != null)
        {
            ClickedInteractable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionObjectInterface = collision.GetComponent<IInteractableObject>();

        if (collisionObjectInterface != null)
        {
            Debug.Log("Has interface!");

            if (starCoroutine == null && collisionObjectInterface.UseWaitStars)
            {
                starCoroutine = StartCoroutine(EnableStarsGradually());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (starCoroutine != null)
        {
            //Reset the coroutine
            StopCoroutine(starCoroutine);
            starCoroutine = null;
        }

        //Turn off any stars that were active
        TurnOffAllWaitStars();

        //re-enable waitStars if they were off
        allowWaitStars = true;

        //Clear last triggered object
        collisionObjectInterface = null;
    }

    /// <summary>
    /// Configures the mouse.
    /// </summary>
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
            //Get the circle collider
            try
            {
                cursorColider = cursorObject.GetComponent<Collider2D>();

                //Hide the cursor
                Cursor.visible = false;

                mouseCursorFound = true;
            }
            catch
            {
                Debug.Log("Cursor - No collider found!");
                Cursor.visible = true;
            }
        }
    }

    /// <summary>
    /// Determines the mouse position in world-space and sets the cursor object to this position.
    /// </summary>
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

    /// <summary>
    /// Handles clicking an interactable.
    /// </summary>
    private void ClickedInteractable()
    {
        if (collisionObjectInterface.IsClickable)
        {
            TurnOffAllWaitStars();
            allowWaitStars = false;
            collisionObjectInterface.DoAction();
        }
    }

    /// <summary>
    /// Gets all child waitStars on the cursor object.
    /// </summary>
    private void GetWaitStars()
    {
        waitStars = new List<SpriteRenderer>();

        foreach(Transform child in this.transform)
        {
            SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
            waitStars.Add(childRenderer);
        }

        //Set them to inactive if there are stars
        TurnOffAllWaitStars();
    }

    /// <summary>
    /// Coroutine that turns on each waitStar with delay between the stars
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableStarsGradually()
    {
        int starsEnabled = 0;

        //Only perform if more than 1 wait star is in the list
        if (waitStars.Count > 0 && allowWaitStars)
        {
            foreach (var waitStar in waitStars)
            {
                if (allowWaitStars)
                {
                    //enable current star
                    waitStar.enabled = true;
                    starsEnabled++;

                    //Wait 'x' seconds before next
                    yield return new WaitForSeconds(waitStarDelayInSeconds);
                }
                else
                {
                    //Stop the coroutine, stars aren't allowed.
                    StopCoroutine(EnableStarsGradually());
                }
            }
        }

        //If all stars have loaded, lets load the new scene from the trigger, needs to check that we didnt click.
        if (starsEnabled == waitStars.Count && allowWaitStars)
        {
            collisionObjectInterface.DoAction();
        }
    }

    /// <summary>
    /// Turns off all active waitStars on the cursor
    /// </summary>
    private void TurnOffAllWaitStars()
    {

        if (waitStars.Count > 0)
        {
            foreach (var waitStar in waitStars)
            {
                waitStar.enabled = false;
            }
        }
    }

    private void LoadLevel(string level)
    {
        if (!string.IsNullOrEmpty(level))
        {
            TurnOffAllWaitStars();
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.Log("Level - No Level to load!");
        }
    }
}
