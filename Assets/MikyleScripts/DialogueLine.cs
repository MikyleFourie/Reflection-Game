using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;    // Who is speaking (NPC, player)
    [TextArea(3, 10)]
    public string dialogueText;   // The actual line spoken
}

