using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    public void ChangeTask(string newTask)
    {
        taskText.text = "";
        foreach (char letter in newTask)
        {
            taskText.text += letter;
        }
    }
}
