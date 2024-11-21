using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UpdateHintTextEvent", menuName = "Dialogue System/Update Hint Event")]
[System.Serializable]
public class UpdateHintTextEvent : DialogueEvent
{
    GameObject hintTMP;
    public string hintText;
    public override void Execute(float Duration, System.Action onComplete)
    {
        hintTMP = GameObject.FindWithTag("Hint");
        hintTMP.GetComponent<TextMeshProUGUI>().text = hintText;

        onComplete?.Invoke();
    }
}
