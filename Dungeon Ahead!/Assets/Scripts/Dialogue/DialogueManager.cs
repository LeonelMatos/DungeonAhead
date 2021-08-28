using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> senteces;

    public bool IsRunning = false;
    private bool Quest;
    private GameObject dialogueTrigger;

    private Dialogue definedDialogue;

    private void Awake() {
        
    }

    void Start() {
        senteces = new Queue<string>();
        dialogueBox.SetActive(true);

    }

    public void StartDialogue (Dialogue dialogue)
    {
        definedDialogue = dialogue;
        animator.SetBool("IsOpen", true);
        IsRunning = animator.GetBool("IsOpen");
        //Debug.Log("Starting conversation with " + dialogue.name); //Runs multiple times

        dialogue.isDone = false;    //Update
        nameText.text = dialogue.name;

        senteces.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            senteces.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(senteces.Count == 0)
        {
            EndDialogue();
            if (Quest)
            {
                dialogueTrigger.GetComponent<QuestGiver>().SetQuest(definedDialogue);
            }
            else
            {
                definedDialogue.isDone = true; //Update    //Set an option to see if I want to play the dialogue
            }                                              //just once
            return;
        }

        string sentence = senteces.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        IsRunning = false;
        
    }

    public void CheckQuest(bool HasQuest, GameObject childDialogue)
    {
        dialogueTrigger = childDialogue;
        Quest = HasQuest;
    }

}
