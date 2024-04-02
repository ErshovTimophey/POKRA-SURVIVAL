using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RSecondGameManager : MonoBehaviour
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
    public Button buttonToR201;
    public GameObject buttonToR201Trigger;
    public GameObject buttonToR202Trigger;
    public GameObject buttonToR203Trigger;
    public GameObject buttonToR204Trigger;
    public GameObject buttonForLists;
    public LevelChanger levelToR201Changer;
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

        // Стандартное состояние сцены
        player.transform.localPosition = pos.value;
        task.ChangeTask(sceneData.currentTask);
        phoneButton.interactable = false;
        controllingAnimator.SetBool("isOpen", true);
        buttonToR201.onClick.RemoveAllListeners();
        buttonToR201.onClick.AddListener(levelToR201Changer.FadeOnLevel);
        gameMessageBox.SetActive(true);

        // Первое попадание в сцену
        if (sceneData.numOfTaskInRSecond == 0)
        {
            sceneData.numOfTaskInRSecond = 1;

            gameMessageBox.SetActive(false);
            phoneMessageBox.SetActive(false);

            sceneData.currentTask = "Задание:\nЗайди в R201";
            task.ChangeTask(sceneData.currentTask);
        }

        // Состояние сцены после речи проректора
        if (sceneData.numOfTaskInRSecond == 2)
        {
            sceneData.numOfTaskInRSecond = 3;

            buttonToR201Trigger.SetActive(false);

            gameMessageBox.SetActive(true);
            
            messageManager.OnGameMessageEnding += OnFirstGameMessageEnding;
            string[] phrases =
            {
                "Теперь посмотри на списки и топай в свою аудиторию",
                "Тебя, кстати, зовут Тимофей Волков"
            };
            messageTrigger.message = new Message(phrases);
            controllingAnimator.SetBool("isOpen", false);
            messageTrigger.TriggerMessage();
        }

        // Состояние сцены, когда у игрока активировано задание посмотреть списки
        if (sceneData.numOfTaskInRSecond == 4)
        {
            buttonToR201Trigger.SetActive(true);
            buttonToR202Trigger.SetActive(true);
            buttonToR203Trigger.SetActive(true);
            buttonToR204Trigger.SetActive(true);

            string[] phrases =
            {
                "Упс, ты пришёл не туда"
            };
            messageTrigger.message = new Message(phrases);
            messageManager.OnGameMessageEnding += OnWrongCabinetMessage;

            buttonToR201.onClick.RemoveAllListeners();
            buttonToR201.onClick.AddListener(messageTrigger.TriggerMessageAndHideControlling);

            buttonForLists.SetActive(true);
            controllingAnimator.SetBool("isOpen", true);
        }

        // Состояние сцены сразу после сцены с куратором
        if (sceneData.numOfTaskInRSecond == 5)
        {
            sceneData.numOfTaskInRSecond = 6;

            buttonToR201Trigger.SetActive(false);
            buttonToR202Trigger.SetActive(false);
            buttonToR203Trigger.SetActive(false);
            buttonToR204Trigger.SetActive(false);

            controllingAnimator.SetBool("isOpen", false);

            string[] phrases =
            {
                "Теперь у тебя есть сразу 3 задания - можешь выполнять их в любом порядке"
            };
            messageTrigger.message = new Message(phrases);
            messageTrigger.TriggerMessage();

            messageManager.OnGameMessageEnding += OnCuratorSceneEnding;
        }

        // Состояние сцены после завершения задания с куратором
        if (sceneData.numOfTaskInRSecond == 6)
        {
            buttonToR201Trigger.SetActive(false);
            buttonToR202Trigger.SetActive(false);
            buttonToR203Trigger.SetActive(false);
            buttonToR204Trigger.SetActive(false);
        }

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    private void OnFirstGameMessageEnding()
    {
        buttonToR201Trigger.SetActive(true);
        buttonToR202Trigger.SetActive(true);
        buttonToR203Trigger.SetActive(true);
        buttonToR204Trigger.SetActive(true);

        string[] phrases =
        {
            "Упс, ты пришёл не туда"
        };
        messageTrigger.message = new Message(phrases);
        messageManager.OnGameMessageEnding += OnWrongCabinetMessage;

        buttonToR201.onClick.RemoveAllListeners();
        buttonToR201.onClick.AddListener(messageTrigger.TriggerMessageAndHideControlling);

        buttonForLists.SetActive(true);
        controllingAnimator.SetBool("isOpen", true);
        sceneData.currentTask = "Задание:\nПопасть в свою аудторию";
        task.ChangeTask(sceneData.currentTask);
        sceneData.numOfTaskInRSecond = 4;
    }

    private void OnWrongCabinetMessage()
    {
        controllingAnimator.SetBool("isOpen", true);
    }

    private void OnCuratorSceneEnding()
    {
        controllingAnimator.SetBool("isOpen", true);
        sceneData.currentTask = "Задание:\n1) Посетить Столовую\n2) Посетить Библиотеку\n3) Посетить корпус F";
        task.ChangeTask(sceneData.currentTask);
        sceneData.numOfTaskInNFirst = 2;
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
