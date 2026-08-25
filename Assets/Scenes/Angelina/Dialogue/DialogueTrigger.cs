using UnityEngine;

public class SelfDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.GetComponent<Rigidbody2D>() != null)
        {
            triggered = true;
            dialogue.StartDialogue();
        }
    }
}