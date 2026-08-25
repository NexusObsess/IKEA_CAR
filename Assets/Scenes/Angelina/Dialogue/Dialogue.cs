using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public string characterName;
    public string[] lines;

    public float textSpeed = 0.05f;

    private int index;
    private bool dialogueActive = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);

        index = 0;
        dialogueActive = true;

        nameText.text = characterName;
        dialogueText.text = "";

        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (!dialogueActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;

            dialogueText.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueActive = false;
            gameObject.SetActive(false);
        }
    }
}