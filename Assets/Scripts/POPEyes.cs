using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class POPEyes : MonoBehaviour
{
    [SerializeField] private float minTimeForBlink = 3.0f;
    [SerializeField] private float maxTimeForBlink = 5.0f;
    [SerializeField] private float blinkDuration = 0.2f;
    private float timer = 0.0f;
    private float timeToNextBlink = 0.0f;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        if (image == null) { Debug.LogError("POPEyes::Missing <IMAGE> Component"); }

        if (image != null && image.enabled)
        {
            //Turn off the eyelids if they are already on.
            image.enabled = false;
        }

        SetNextBlinkTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (image != null)
        {
            timer += Time.deltaTime;

            if (timer >= timeToNextBlink)
            {
                StartCoroutine(Blink());
                SetNextBlinkTime();
            }
        }
    }

    /// <summary>
    /// Determines the next blink duration.
    /// </summary>
    void SetNextBlinkTime()
    {
        timer = 0.0f;
        timeToNextBlink = Random.Range(minTimeForBlink, maxTimeForBlink);
    }

    /// <summary>
    /// Performs the blink
    /// </summary>
    /// <returns></returns>
    IEnumerator Blink()
    {
        //Close eyes
        image.enabled = true;

        yield return new WaitForSeconds(blinkDuration);

        //Open eyes
        image.enabled = false;
    }
}
