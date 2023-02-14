using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

/**
\brief Controls the logic and the output of the given dialogue.
Responsible for showing the dialogue, interaction with the dialogue box,
and checking for quests after the given dialogue.
*/
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    /// Output of the character's name
    public Text nameText;
    /// Holder of the given text used at TypeSentence()
    public Text dialogueText;

    /// Responsible for opening/closing the dialogue box in-game.
    public Animator animator;

    private Queue<string> senteces;
    /// Follows the dialogue system when active.
    /// Used to check if the player is far from the dialogueTrigger when true.
    public bool IsRunning = false;
    /// Informs at the end of the dialogue if a there's an existing quest to
    /// be given.
    private bool Quest;
    /// Current dialogueTrigger. Used to indicate the active trigger.
    /// Used as sequence of dialogues as hierarchy of child gameObjects.
    private GameObject dialogueTrigger;

    private Dialogue definedDialogue;

    private Player player;

    /// DialogueTrigger passed when received a LinearStoryController
    /// at this trigger
    private DialogueTrigger trigger;



    private void Awake()
    {

    }

    void Start()
    {
        senteces = new Queue<string>();
        dialogueBox.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        //Keyboard skip text keys.
        if (Input.GetKeyDown(player.playerControls.DialogueSkip) || Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        this.trigger = trigger;

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
        if (senteces.Count == 0)
        {
            EndDialogue();
            if (Quest)
            {
                dialogueTrigger.GetComponent<QuestGiver>().SetQuest(definedDialogue);
            }
            /*else
            {*/
                definedDialogue.isDone = true;
            //}
            return;
        }

        string sentence = senteces.Dequeue();
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
        animator.SetBool("IsOpen", false);
        IsRunning = false;
        trigger.SetActiveTrigger(false);

        if (trigger.getLSC() == null) return;

        Debug.Log("Returning to given LinearStoryController");
        trigger.getLSC().RunEventList();


    }

    public void CheckQuest(bool HasQuest, GameObject childDialogue)
    {
        dialogueTrigger = childDialogue;
        Quest = HasQuest;
    }

}
