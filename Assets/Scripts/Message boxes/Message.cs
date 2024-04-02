using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Message
{
    [TextArea]
    public string[] phrases;

    public Message(string[] phrases)
    {
        this.phrases = phrases;
    }
}
