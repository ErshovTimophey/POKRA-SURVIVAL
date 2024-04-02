using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLists : MonoBehaviour
{
    public Animator listAnimator;
    public GameObject controlling;
    public GameObject buttonForLists;

    private Animator controllingAnimator;
    private Animator buttonForListsAnimator;

    private void Start()
    {
        controllingAnimator = controlling.GetComponent<Animator>();
        buttonForListsAnimator = buttonForLists.GetComponent<Animator>();
    }

    public void OnListsOpening()
    {
        listAnimator.SetTrigger("isTriggered");
        controllingAnimator.SetBool("isOpen", false);
        buttonForListsAnimator.SetTrigger("isTriggered");
    }

    public void OnListsClosing()
    {
        listAnimator.SetTrigger("isTriggered");
        controllingAnimator.SetBool("isOpen", true);
        buttonForListsAnimator.SetTrigger("isTriggered");
    }
}
