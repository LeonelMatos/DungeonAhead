using UnityEngine;

/**
\brief Component to create a dialogue in-editor and run it in-game.
*/
public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Automatically checked depending if there's a quest component in gameobject")]
    public bool HasQuest;
    [Tooltip("Obsolete (may or may not work)")]
    public bool isCloseToDialogue = true;
    private Transform playerPosition;
    [Tooltip("Defines the collider's size")]
    public float triggerSize = 1.5f;

    /// Template of dialogue
    public Dialogue dialogue;
    private Player player;
    private KeyCode interact;

    /// Next trigger to run a dialogue, as a hierarchy of children gameObjects.
    private DialogueTrigger childDialogue;

    /// Defined distance until far enough to break the dialogue.
    float distance = 5f;

    private LinearStoryController lsc;

    private bool activeTrigger = false;

    public void SetActiveTrigger(bool value)
    {
        activeTrigger = value;
    }

    public LinearStoryController getLSC()
    {
        return lsc;
    }

    private void Awake()
    {
        if (!TryGetComponent(out BoxCollider2D collider))
        {
            gameObject.AddComponent<BoxCollider2D>();
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<BoxCollider2D>().size *= new Vector2(triggerSize, triggerSize);
        }

    }

    private void Start()
    {
        interact = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().interact;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        if (TryGetComponent(out QuestGiver questGiver))
            HasQuest = true;
        else
            HasQuest = false;
    }

    public void TriggerDialogue(Dialogue dialogue, DialogueTrigger childDialogue)
    {
        FindObjectOfType<DialogueManager>().CheckQuest(HasQuest, childDialogue.gameObject);

        Debug.Log("LinearStoryController found, sending to DialogueManager");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
    }

    public void SetDialogue()
    {
        if (dialogue.isDone)
        {
            for (int i = 0; i < transform.childCount; i++) //Can get out of bounds (unchecked)
            {
                if (!childDialogue.TryGetComponent<DialogueTrigger>(out DialogueTrigger trigger)) continue;

                childDialogue = transform.GetChild(i).GetComponent<DialogueTrigger>();

                if (!childDialogue.dialogue.isDone)
                {
                    TriggerDialogue(childDialogue.dialogue, childDialogue);
                    break;
                }
                else if (childDialogue.dialogue.isDone && i == transform.childCount)
                    break;
            }
        }
        else
            TriggerDialogue(dialogue, this);
    }

    /// Used at LinearStoryController. Stores the sent lsc.
    public void SetDialogue(LinearStoryController lsc)
    {
        this.lsc = lsc;

        if (dialogue.isDone)
        {
            for (int i = 0; i < transform.childCount; i++) //Can get out of bounds (unchecked)
            {
                if (!childDialogue.TryGetComponent<DialogueTrigger>(out DialogueTrigger trigger)) continue;

                childDialogue = transform.GetChild(i).GetComponent<DialogueTrigger>();

                if (!childDialogue.dialogue.isDone)
                {
                    TriggerDialogue(childDialogue.dialogue, childDialogue);
                    break;
                }
                else if (childDialogue.dialogue.isDone && i == transform.childCount)
                    break;
            }
        }

        else
            TriggerDialogue(dialogue, this);
    }

    private bool IsOnTrigger; //Checks for player's position on trigger
    private Collider2D Collider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider = collision;
        IsOnTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsOnTrigger = false;
    }

    private void Update()
    {
        //Checks if a LinearStoryController was sent, so checking for position won't be necessary
        if (lsc != null)
            ///\todo This is incorrect. The if will run each frame even if the dialogueManager is not being used.
            if (playerPosition != null && isCloseToDialogue == true /*&& FindObjectOfType<DialogueManager>().IsRunning == true*/)
            {

                distance = Vector2.Distance(playerPosition.position, this.transform.position);
                //Debug.Log(distance);

                if (distance >= 3.0f && distance <= 4.0f)
                {
                    FindObjectOfType<DialogueManager>().EndDialogue();
                    Debug.Log("Stopped dialogue");
                    activeTrigger = false;
                }
            }

        if (Collider != null)
        {
            if (!activeTrigger)
                if (Collider.gameObject.tag == "Player" && IsOnTrigger && Input.GetKey(interact))
                {
                    SetDialogue();
                    activeTrigger = true;
                }
        }
    }
}
