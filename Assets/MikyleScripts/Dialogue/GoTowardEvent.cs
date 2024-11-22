using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoTowardEvent", menuName = "Dialogue System/GoToward Event")]
[System.Serializable]
public class GoTowardEvent : DialogueEvent
{
    EventManager eventManager;
    public string agent;
    public string target;


    public override void Execute(float Duration, System.Action onComplete)
    {
        eventManager = GameObject.FindObjectOfType<EventManager>();

        if (eventManager == null)
        {
            Debug.LogError("Could Not Find eventManager for GoTowards");
        }
        else
        {
            Debug.Log("Found eventManager");
            MoveTowardsTarget moveTowardsTarget = eventManager.GetComponent<MoveTowardsTarget>();
            moveTowardsTarget.GoTowardsTarget(agent, target, Duration, onComplete);
        }



    }
}
