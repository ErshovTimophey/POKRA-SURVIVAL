using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public MessageManager messageManager;
    public Message message;
    public GameObject controlling;

    public void TriggerMessage()
    {
        messageManager.StartMessage(message);
    }

    public void TriggerMessageAndHideControlling()
    {
        messageManager.StartMessage(message);
        controlling.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
