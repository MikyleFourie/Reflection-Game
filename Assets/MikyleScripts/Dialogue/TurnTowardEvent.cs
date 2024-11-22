using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnTowardEvent", menuName = "Dialogue System/TurnToward Event")]
[System.Serializable]
public class TurnTowardEvent : DialogueEvent
{
    EventManager eventManager;
    public string agent;
    public string target;


    public override void Execute(float Duration, System.Action onComplete)
    {
        eventManager = GameObject.FindObjectOfType<EventManager>();

        if (eventManager == null)
        {
            Debug.LogError("Could Not Find eventManager for TurnTowards");
        }
        else
        {
            Debug.Log("Found eventManager");
            TurnToFaceTarget turnToFace = eventManager.GetComponent<TurnToFaceTarget>();
            turnToFace.TurnTowardsTarget(agent, target, Duration, onComplete);
        }


    }

}
