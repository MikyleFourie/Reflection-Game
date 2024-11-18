using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneFadeEvent", menuName = "Dialogue System/Scene Fade Event")]
[System.Serializable]
public class SceneFadeEvent : DialogueEvent
{
    //public float fadeDuration = 3f;

    public override void Execute()
    {
        // Assuming you have a SceneFader class that handles the fade out
        SceneFader sceneFader = FindObjectOfType<SceneFader>();
        if (sceneFader != null)
        {
            sceneFader.StartFadeOut();
        }
        else
        {
            Debug.LogError("SceneFader not found in the scene");
        }
    }
}
