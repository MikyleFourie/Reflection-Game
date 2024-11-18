using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoadEvent", menuName = "Dialogue System/Scene Load Event")]
[System.Serializable]
public class SceneLoadEvent : DialogueEvent
{
    public string nextLevel;

    public override void Execute()
    {
        // Load the next scene after the fade out
        SceneManager.LoadScene(nextLevel);
    }
}
