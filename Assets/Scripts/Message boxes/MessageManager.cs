using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public delegate void MessageEnding();
    public event MessageEnding OnGameMessageEnding;

    public TextMeshProUGUI messageText;
    public Animator animator;

    private Queue<string> phrases= new();

    public void StartMessage(Message message)
    {
        animator.SetBool("isOpen", true);
        phrases.Clear();
        foreach (string phrase in message.phrases)
        {
            phrases.Enqueue(phrase);
        }
        DisplayNextPhare();
    }

    public void DisplayNextPhare()
    {
        if (phrases.Count == 0)
        {
            EndMessage();
            return;
        }
        string phrase = phrases.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypePhrase(phrase));
    }

    private IEnumerator TypePhrase(string phrase)
    {
        messageText.text = "";
        foreach (char letter in phrase.ToCharArray())
        {
            messageText.text += letter;
            yield return null;
        }
    }

    private void EndMessage()
    {
        animator.SetBool("isOpen", false);
        OnGameMessageEnding?.Invoke();
    }
}
