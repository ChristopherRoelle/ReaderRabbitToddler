using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class POPEyes : MonoBehaviour
{
    [SerializeField] private float minTimeForBlink = 5.0f;
    [SerializeField] private float maxTimeForBlink = 10.0f;
    [SerializeField] private float blinkDuration = 0.1f;
    private float timer = 0.0f;
    private float timeToNextBlink = 0.0f;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer != null && spriteRenderer.enabled)
        {
            //Turn off the eyelids if they are already on.
            spriteRenderer.enabled = false;
        }

        SetNextBlinkTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer != null)
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
        spriteRenderer.enabled = true;

        yield return new WaitForSeconds(blinkDuration);

        //Open eyes
        spriteRenderer.enabled = false;
    }
}
