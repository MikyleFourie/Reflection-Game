using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnToFaceTarget : MonoBehaviour
{

    public GameObject target;
    public GameObject agent;

    public void TurnTowardsTarget(string agentName, string targetName, float duration, System.Action onComplete)
    {
        StartCoroutine(RotateToFaceTarget(agentName, targetName, duration, onComplete));
    }

    public IEnumerator RotateToFaceTarget(string agentName, string targetName, float duration, System.Action onComplete)
    {
        target = GameObject.Find(targetName);
        agent = GameObject.Find(agentName);
        Vector3 startRotation = transform.eulerAngles; // Starting rotation of the object.
        Vector3 targetDirection = (target.transform.position - agent.transform.position).normalized; // Direction to the player.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // Calculate target rotation.

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Gradually interpolate the rotation towards the target.
            agent.transform.rotation = Quaternion.Slerp(
                Quaternion.Euler(startRotation),
                targetRotation,
                elapsedTime / duration
            );

            elapsedTime += Time.deltaTime; // Increment elapsed time.
            yield return null; // Wait for the next frame.
        }

        // Ensure the final rotation is exactly towards the player after the loop.
        agent.transform.rotation = targetRotation;
        onComplete?.Invoke();

    }
}
