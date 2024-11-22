using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTowardsTarget : MonoBehaviour
{
    public GameObject target; // Assign the target object in the Inspector or dynamically.
    public GameObject agent; // Assign the target object in the Inspector or dynamically.

    public void GoTowardsTarget(string agentName, string targetName, float duration, System.Action onComplete)
    {
        StartCoroutine(MoveTowardsTargetCoroutine(agentName, targetName, duration, onComplete));
    }

    // Coroutine to move the game object towards the target over a set duration.
    public IEnumerator MoveTowardsTargetCoroutine(string agentName, string targetName, float duration, System.Action onComplete)
    {
        target = GameObject.Find(targetName);
        agent = GameObject.Find(agentName);
        Vector3 startPosition = agent.transform.position; // Initial position of the object.
        Vector3 targetPosition = target.transform.position; // Final position to move towards.

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Linearly interpolate between the start and target positions.
            agent.transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration
            );

            elapsedTime += Time.deltaTime; // Increment elapsed time.
            yield return null; // Wait for the next frame.
        }

        // Ensure the final position is exactly the target's position after the loop.
        agent.transform.position = targetPosition;
        onComplete?.Invoke();
    }
}
