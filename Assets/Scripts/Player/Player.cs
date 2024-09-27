using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
     
    Vector2 mousePosition = Vector2.zero;

    [SerializeField]
    float waitStarDelayInSeconds = 0.5f;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject cursorObject;

    bool mouseCursorFound = false;
    Collider2D cursorColider;
    List<SpriteRenderer> waitStars;

    private Coroutine starCoroutine;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.tag}");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(starCoroutine == null)
        {
            starCoroutine = StartCoroutine(EnableStarsGradually());
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
        DisableAllWaitStars();
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

    private void GetWaitStars()
    {
        waitStars = new List<SpriteRenderer>();

        foreach(Transform child in this.transform)
        {
            SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
            waitStars.Add(childRenderer);
        }

        //Set them to inactive if there are stars
        DisableAllWaitStars();
    }

    private IEnumerator EnableStarsGradually()
    {
        if (waitStars.Count > 0)
        {
            foreach (var waitStar in waitStars)
            {
                //enable current star
                waitStar.enabled = true;

                //Wait 'x' seconds before next
                yield return new WaitForSeconds(waitStarDelayInSeconds);
            }
        }

        //If all stars have loaded, lets load the new scene from the trigger
        LoadLevel("");
    }

    private void DisableAllWaitStars()
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
            DisableAllWaitStars();
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.Log("Level - No Level to load!");
        }
    }
}
