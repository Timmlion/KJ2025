using System.Collections;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Transform leftPupil;
    public Transform rightPupil;
    public float moveSpeed = 5f;
    public float waitTime = 2f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isReturning = false;

    void Start()
    {
        initialPosition = leftPupil.localPosition;
        targetPosition = initialPosition;
        StartCoroutine(MoveEyes());
    }

    IEnumerator MoveEyes()
    {
        while (true)
        {
            targetPosition = new Vector3(
                Random.Range(-0.3f, 0.3f),
                0.1f,
                Random.Range(-0.3f, 0.3f)
            );

            yield return MoveToPosition(targetPosition);
            yield return new WaitForSeconds(waitTime);
            isReturning = true;
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        float timeElapsed = 0f;
        Vector3 startingPositionLeft = leftPupil.localPosition;
        Vector3 startingPositionRight = rightPupil.localPosition;

        while (timeElapsed < 1f)
        {
            leftPupil.localPosition = Vector3.Lerp(startingPositionLeft, target, timeElapsed);
            rightPupil.localPosition = Vector3.Lerp(startingPositionRight, target, timeElapsed);

            timeElapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        leftPupil.localPosition = target;
        rightPupil.localPosition = target;
    }
}