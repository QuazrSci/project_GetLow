using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    public Transform startPosition;   // Starting position of the buttons
    public Transform endPosition;     // Ending position of the buttons
    public float movementSpeed;   // Speed of the buttons' movement

    //private float minAppearDelay = 1f;  // Minimum delay before button appears
    //private float maxAppearDelay = 3f;  // Maximum delay before button appears
    public bool isMoving;            // Flag to track button movement status
    public bool is_recyclable = true;
    public bool is_active=true;

    void Start()
    {
        movementSpeed = MusicManager.instance.triggrs_speed;
    }

    void Update()
    {
        // Move the button from start to end position only if it's active
        if (isMoving)
        {
            float step = movementSpeed * 10 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);

            if (is_active && transform.position.x < AcceptCircleScrpt.instance.transform.position.x - 50)
            {
                MusicManager.instance.Message("missed", 50, 50, 50);
                is_active = false;
            }

            // If the button reaches the end position, hide it and make it appear randomly again
            if (Vector3.Distance(transform.position, endPosition.position) < 0.001f)
            {
                transform.position = startPosition.position;
                is_recyclable = true;
                isMoving = false;
                is_active = true;
            }
        }
    }

    // Function to make the button appear randomly
    /*
    private void RandomAppear()
    {
        // Calculate a random delay before the button appears
        float appearDelay = Random.Range(minAppearDelay, maxAppearDelay);

        // Use a coroutine to delay the appearance of the button
        StartCoroutine(DelayedAppear(appearDelay));
    }
    */

    // Coroutine to set the button active after a delay
    /*
    private IEnumerator DelayedAppear(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Set the button active and start moving it from the start position
        gameObject.SetActive(true);
        transform.position = startPosition.position;
        isMoving = true;

        // Set a random sprite from the list of buttonSprites
        if (buttonSprites.Count > 0)
        {
            int randomIndex = Random.Range(0, buttonSprites.Count);
            MainImage.sprite = buttonSprites[randomIndex];
        }
    }
    */
}