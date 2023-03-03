using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // this script is for setting the strings in our dialogue type
    // and triggering the dialogue to happen
    public Dialogue dialogue;

    [SerializeField] DialogueManager dialogueManager;

    void Start()
    {

    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);

    }
}
