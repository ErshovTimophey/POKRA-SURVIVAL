using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanteenPlatformerGameManager : MonoBehaviour
{
    public GameObject controlling;

    private Animator controllingAnimator;

    public SceneData sceneData;

    private void Start()
    {
        controllingAnimator = controlling.GetComponent<Animator>();
        controllingAnimator.SetBool("isOpen", true);
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
