using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtriumGameManager : MonoBehaviour
{
    public VectorValue pos;
    public GameObject player;
    public GameObject gameMessageBox;
    public GameObject phoneMessageBox;
    public GameObject inventory;
    public GameObject controlling;
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public Phone phone;
    public GameObject phoneMark;
    public GameObject taskObject;
    public GameObject table;
    public GameObject buttonForTableTrigger;
    public GameObject buttonToFTrigger;
    public GameObject admission;

    private Button phoneButton;
    private Animator taskAnimator;
    private Animator controllingAnimator;
    private Task task;

    public SceneData sceneData;

    private void Start()
    {
        phoneButton = phone.GetComponent<Button>();
        taskAnimator = taskObject.GetComponent<Animator>();
        controllingAnimator = controlling.GetComponent<Animator>();
        task = taskObject.GetComponent<Task>();

        player.transform.localPosition = pos.value;
        task.ChangeTask(sceneData.currentTask);

        if (sceneData.isAtriumLoadedFirst)
        {
            sceneData.numOfTaskInNFirst = 0;
            sceneData.isRFirstLoadedFirst = true;
            sceneData.isCuratorSceneLoadedFirst = true;
            sceneData.numOfTaskInRSecond = 0;
            sceneData.isLibraryLoadedFirst = true;
            sceneData.numOfCanteenTask = 0;
            sceneData.numOfLibraryTask = 0;
            sceneData.numOfFTask = 0;
            sceneData.isFGuyHappy = false;
            sceneData.isAtriumLoadedFirst = false;
            pos.value = new Vector3(1.759875f, -3.612306f, 0);
            player.transform.localPosition = pos.value;
            messageManager.OnGameMessageEnding += OnFirstGameMessageEnding;
            gameMessageBox.SetActive(true);
            phoneMessageBox.SetActive(false);
            inventory.SetActive(false);
            controlling.SetActive(false);
            taskObject.SetActive(false);
            messageTrigger.TriggerMessage();
        }
        else
        {
            inventory.SetActive(true);
            controlling.SetActive(true);
            controllingAnimator.enabled = false;
            taskObject.SetActive(true);
            taskAnimator.enabled = false;
            phoneButton.interactable = false;
            phoneMark.SetActive(false);
        }

        if (sceneData.numOfLibraryTask == 1)
        {
            table.SetActive(true);
            buttonForTableTrigger.SetActive(true);
        }

        if (sceneData.numOfTaskInRSecond == 6)
        {
            buttonToFTrigger.SetActive(true);
        }

        if (sceneData.numOfLibraryTask == 1 || sceneData.numOfLibraryTask == 2)
        {
            buttonToFTrigger.SetActive(false);
        }

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    private void OnFirstGameMessageEnding()
    {
        phone.OnGameMessageEnding += OnFirstPhoneMessageEnding;
        phoneMessageBox.SetActive(true);
        inventory.SetActive(true);
        phoneButton.interactable = true;
    }

    private void OnFirstPhoneMessageEnding()
    {
        phoneMark.SetActive(false);
        phoneButton.interactable = false;

        messageManager.OnGameMessageEnding -= OnFirstGameMessageEnding;
        messageManager.OnGameMessageEnding += OnSecondGameMessageEnding;

        string[] phrases =
        {
            "Как ты понял, сейчас тебе надо попасть в корпус R",
            "Сзади тебя нарисованы стрелки, по которым ты можешь ориентироваться",
            "А ещё с этого момента на экране будет отображаться твоё текущее задание!"
        };
        messageTrigger.message = new Message(phrases);
        messageTrigger.TriggerMessage();
    }

    private void OnSecondGameMessageEnding()
    {
        // phone.OnGameMessageEnding += OnFirstPhoneMessageEnding;
        controlling.SetActive(true);
        controllingAnimator.SetBool("isOpen", true);
        taskObject.SetActive(true);
        taskAnimator.SetBool("isOpen", true);
        sceneData.currentTask = "Задание:\nПопади в корпус R";
        task.ChangeTask(sceneData.currentTask);
    }

    public void OnAdmissionGetting()
    {
        buttonForTableTrigger.SetActive(false);
        gameMessageBox.SetActive(true);
        controllingAnimator.SetBool("isOpen", false);
        string[] phrases =
        {
            "Вот, бери свой пропуск",
            "По нему ты можешь попасть в любой корпус ВШЭ в любом городе, зайти в общежитие и в библиотеку"
        };
        messageTrigger.message = new Message(phrases);
        messageManager.OnGameMessageEnding += OnAdmissionGettingEnding;
        messageTrigger.TriggerMessage();
    }

    private void OnAdmissionGettingEnding()
    {
        sceneData.numOfLibraryTask = 2;
        sceneData.currentTask = "Задание:\nЗайди в библиотеку";
        task.ChangeTask(sceneData.currentTask);
        controllingAnimator.SetBool("isOpen", true);
        admission.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
