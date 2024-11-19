using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public void ProcessEvents(DialogueEvent[] events, System.Action onAllEventsComplete)
    {
        StartCoroutine(ProcessEventQueue(events, onAllEventsComplete));
    }

    private IEnumerator ProcessEventQueue(DialogueEvent[] events, System.Action onAllEventsComplete)
    {
        foreach (DialogueEvent dialogueEvent in events)
        {
            bool eventCompleted = false;

            dialogueEvent.Execute(dialogueEvent.Duration, () => eventCompleted = true);

            // Wait until the current event finishes
            yield return new WaitUntil(() => eventCompleted);
        }

        // Notify when all events are complete
        onAllEventsComplete?.Invoke();
    }
}
