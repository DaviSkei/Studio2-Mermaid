using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// [CreateAssetMenu (fileName ="New DialogueManager", menuName = "DialogueManager")]
public class DialogueManager : MonoBehaviour
{
    // this script is made to hold all of the sentence strings we want to display
    // and display them thorugh logic

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Animator animator;

    private Queue<string> sentences;
    
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        // this method uses a dialogue string to start the queue from 
        animator.SetBool("isOpen", true);
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
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
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
