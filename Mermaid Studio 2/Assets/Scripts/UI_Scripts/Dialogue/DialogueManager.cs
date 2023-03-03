using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New DialogueManager", menuName = "DialogueManager")]
public class DialogueManager : ScriptableObject
{
    // this script is made to hold all of the sentence strings we want to display

    private Queue<string> sentences;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation");

    }
}
