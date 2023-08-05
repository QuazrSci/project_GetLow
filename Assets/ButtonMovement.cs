using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMovement : MonoBehaviour
{
    public List<Sprite> buttonSprites; // List of sprites to use for the buttons
    public Transform startPosition;   // Starting position of the buttons
    public Transform endPosition;     // Ending position of the buttons
    public float movementSpeed = 2f;   // Speed of the buttons' movement

    public float minAppearDelay = 1f;  // Minimum delay before button appears
    public float maxAppearDelay = 3f;  // Maximum delay before button appears
    private bool isMoving;            // Flag to track button movement status

    private Image MainImage;

    private void Start()
    {
        MainImage = GetComponent<Image>();

        // Call the function to make the button appear randomly
        RandomAppear();
    }

    void Update()
    {
        // Move the button from start to end position only if it's active
        if (isMoving)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);

            // If the button reaches the end position, hide it and make it appear randomly again
            if (Vector3.Distance(transform.position, endPosition.position) < 0.001f)
            {
                gameObject.SetActive(false);
                isMoving = false;

                // Call the function to make the button appear randomly
                RandomAppear();
            }
        }
    }

    // Function to make the button appear randomly
    private void RandomAppear()
    {
        // Calculate a random delay before the button appears
        float appearDelay = Random.Range(minAppearDelay, maxAppearDelay);

        // Use a coroutine to delay the appearance of the button
        StartCoroutine(DelayedAppear(appearDelay));
    }

    // Coroutine to set the button active after a delay
    private System.Collections.IEnumerator DelayedAppear(float delay)
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
}