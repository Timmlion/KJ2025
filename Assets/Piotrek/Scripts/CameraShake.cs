using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Shake settings
    [SerializeField] private float shakeDuration = 0.5f; // How long the shake lasts
    [SerializeField] private float shakeMagnitude = 0.1f; // How much the camera moves
    [SerializeField] private float dampingSpeed = 1.0f; // How quickly the shake diminishes

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition; // Save the camera's starting position
    }

    /// <summary>
    /// Call this method to trigger a camera shake.
    /// </summary>
    public void TriggerShake(float duration = -1, float magnitude = -1)
    {
        StopAllCoroutines(); // Stop any ongoing shake
        StartCoroutine(Shake(duration > 0 ? duration : shakeDuration, magnitude > 0 ? magnitude : shakeMagnitude));
    }
    private void Update()
    {
        // Check for the '9' key press and trigger the shake
        if (Input.GetKeyDown(KeyCode.Alpha9)) // '9' on the main keyboard
        {
            TriggerShake(); // Call the shake method with default settings
        }
    }
    /// <summary>
    /// The coroutine that performs the shaking effect.
    /// </summary>
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Random position within a small range
            Vector3 randomOffset = new Vector3(
                Random.Range(-1f, 1f) * magnitude,
                Random.Range(-1f, 1f) * magnitude,
                0f // Assuming shake is on X and Y only
            );

            transform.localPosition = initialPosition + randomOffset;

            elapsed += Time.deltaTime;

            // Gradually reduce the magnitude for a smooth finish
            magnitude = Mathf.Lerp(magnitude, 0f, Time.deltaTime * dampingSpeed);

            yield return null; // Wait for the next frame
        }

        // Reset the camera to its initial position
        transform.localPosition = initialPosition;
    }
}