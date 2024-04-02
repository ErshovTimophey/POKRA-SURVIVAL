using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class NSecondGameManager : MonoBehaviour
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
    public GameObject buttonToLibraryMessageBoxTrigger;
    public GameObject buttonToLibraryTrigger;
    public GameObject mark;
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
        task.ChangeTask(sceneData.currentTask);
        phoneButton.interactable = false;
        gameMessageBox.SetActive(true);
        controllingAnimator.SetBool("isOpen", true);

        // Когда у игрока активировано задание достать пропуск
        if (sceneData.numOfLibraryTask == 1)
        {
            phoneMessageBox.SetActive(false);
            buttonToLibraryMessageBoxTrigger.SetActive(false);
        }

        // Когда у игрока есть пропуск
        if (sceneData.numOfLibraryTask >= 2)
        {
            buttonToLibraryMessageBoxTrigger.SetActive(false);
            buttonToLibraryTrigger.SetActive(true);
            admission.SetActive(true);
        }
    }

    private void OnFirstLibraryEnteringMessageEnding()
    {
        phoneButton.interactable = true;
        mark.SetActive(true);
        phoneMessageBox.SetActive(true);
        phone.OnGameMessageEnding += OnPhoneMessageEnding;
    }

    public void OnFirstLibraryEntering()
    {
        controllingAnimator.SetBool("isOpen", false);
        buttonToLibraryMessageBoxTrigger.SetActive(false);
        messageManager.OnGameMessageEnding += OnFirstLibraryEnteringMessageEnding;
        messageTrigger.TriggerMessage();
    }

    private void OnPhoneMessageEnding()
    {
        phoneButton.interactable = false;
        mark.SetActive(false);
        phoneMessageBox.SetActive(false);
        sceneData.currentTask = "Задание:\nПолучить пропуск";
        task.ChangeTask(sceneData.currentTask);
        controllingAnimator.SetBool("isOpen", true);
        sceneData.numOfLibraryTask = 1;
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
