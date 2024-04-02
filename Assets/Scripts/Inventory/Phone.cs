using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public delegate void MessageEnding();
    public event MessageEnding OnGameMessageEnding;

    public Animator animator;
    public GameObject phoneMessageBox;

    public void OnPhoneOpen()
    {
        animator.SetBool("isOpen", true);
        phoneMessageBox.SetActive(true);
    }

    public void ClosePhone()
    {
        animator.SetBool("isOpen", false);
        OnGameMessageEnding?.Invoke();
    }
}
