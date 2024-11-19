using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "AnimateEvent", menuName = "Dialogue System/Animate Event")]
[System.Serializable]
public class AnimateEvent : DialogueEvent
{
    public string characterName;
    public string animationName;
    private Animator animator;

    public override void Execute(float Duration, System.Action onComplete)
    {
        // Find the character GameObject
        GameObject gameObject = GameObject.Find(characterName);
        if (gameObject != null)
        {
            animator = gameObject.GetComponentInChildren<Animator>();

            if (animator != null)
            {
                // Play the animation
                animator.Play(animationName);
            }
            else
            {
                Debug.LogWarning("Animator component not found on character: " + characterName);
            }
        }
        else
        {
            Debug.LogWarning("Character GameObject not found: " + characterName);
        }

        // After the animation starts, invoke onComplete
        onComplete?.Invoke();
    }

}
