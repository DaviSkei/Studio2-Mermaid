using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // this script is made to hold all of the sentence strings we want to display
    // and display them thorugh logic
    public Dialogue dialogue;
    public Dialogue startDialogue;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Animator animator;
    [SerializeField] Animator inventoryAnimator;

    private Queue<string> sentences;

    private bool startOver;
    
    void Start()
    {
        sentences = new Queue<string>();
        startOver = false;
    }
    public void StartDialogue()
    {
        // this method uses a dialogue string to start the queue from 
        animator.SetBool("isOpen", true);
        
        sentences.Clear();

        if (!startOver)
        {
            foreach(string sentence in startDialogue.dialogueStrings)
            {
                sentences.Enqueue(sentence);
            }
        }

        if (startOver)
        {
            foreach (string sentence in dialogue.dialogueStrings)
            {
                sentences.Enqueue(sentence);
            }
        }
        
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            Debug.Log("No more sentences");
            EndDialogue();
            return;
        }
        if (startOver)
        {
            inventoryAnimator.SetBool("isOpen", true);
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        startOver = true;
        animator.SetBool("isOpen", false);
        inventoryAnimator.SetBool("isOpen", false);
    }
}
