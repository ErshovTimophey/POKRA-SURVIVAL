using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFirstGameManager : MonoBehaviour
{
    public VectorValue pos;
    public GameObject player;
    public Phone phone;
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public GameObject gameMessageBox;
    public GameObject phoneMessageBox;
    public GameObject controlling;
    public GameObject taskObject;
    public GameObject buttonToCanteenTrigger;
    public GameObject buttonToN2Trigger;
    public GameObject admission;

    private Animator controllingAnimator;
    private Button phoneButton;
    private Task task;

    public SceneData sceneData;

    private void Start()
    {
        controllingAnimator = controlling.GetComponent<Animator>();
        phoneButton = phone.GetComponent<Button>();
        task = taskObject.GetComponent<Task>();

        player.transform.localPosition = pos.value;
        Debug.Log(pos.value);
        task.ChangeTask(sceneData.currentTask);
        phoneButton.interactable = false;

        if (sceneData.numOfTaskInNFirst == 0)
        {
            sceneData.numOfTaskInNFirst = 1;

            gameMessageBox.SetActive(true);
            phoneMessageBox.SetActive(false);

            messageManager.OnGameMessageEnding += OnFirstGameMessageEnding;
            controllingAnimator.SetBool("isOpen", false);
            messageTrigger.TriggerMessage();
        }
        else
        {
            controllingAnimator.SetBool("isOpen", true);
        }

        // Когда у игрока активно тройное задание
        if (sceneData.numOfTaskInNFirst == 2)
        {
            buttonToCanteenTrigger.SetActive(true);
            buttonToN2Trigger.SetActive(true);
        }

        if (sceneData.numOfLibraryTask == 1 || sceneData.numOfLibraryTask == 2)
        {
            buttonToCanteenTrigger.SetActive(false);
        }

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    private void OnFirstGameMessageEnding()
    {
        controllingAnimator.SetBool("isOpen", true);
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
