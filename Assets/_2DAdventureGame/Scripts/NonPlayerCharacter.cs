using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [TextArea]
    public string dialogueText = "Привіт, мандрівнику!";

    public void DisplayDialogue()
    {
        if (UIHandler.instance != null)
        {
            UIHandler.instance.DisplayDialogue(dialogueText);
        }
    }
}