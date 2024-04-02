using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanteenGameManager : MonoBehaviour
{
    public VectorValue pos;
    public GameObject player;
    public Phone phone;
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public GameObject gameMessageBox;
    public GameObject controlling;
    public GameObject taskObject;
    public GameObject buttonBackTrigger;
    public GameObject buttonToCanteenGuyTrigger;
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

        // Когда игрок первый раз входит в столовую
        if (sceneData.numOfCanteenTask == 0)
        {
            sceneData.numOfCanteenTask = 1;

            sceneData.currentTask = "Задание:\nПоговори с парнем";
            task.ChangeTask(sceneData.currentTask);
            buttonBackTrigger.SetActive(false);
        }

        // После окончания платформера
        if (sceneData.numOfCanteenTask == 2)
        {
            buttonToCanteenGuyTrigger.SetActive(false);
            controllingAnimator.SetBool("isOpen", false);
            string[] phrases =
            {
                "Парень:\nОй, спасибо тебе большое! Удачи!"
            };
            messageTrigger.message = new Message(phrases);
            messageManager.OnGameMessageEnding += OnSecondTalkToCanteenGuyEnding;
            messageTrigger.TriggerMessage();
        }

        // После окончания задания в столовой
        if (sceneData.numOfCanteenTask == 3)
        {
            buttonToCanteenGuyTrigger.SetActive(false);
        }

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    public void OnTalkToCanteenGuy()
    {
        buttonToCanteenGuyTrigger.SetActive(false);
        messageManager.OnGameMessageEnding += OnFirstTalkToCanteenGuyEnding;
        controllingAnimator.SetBool("isOpen", false);
        messageTrigger.TriggerMessage();
    }

    private void OnFirstTalkToCanteenGuyEnding()
    {
        SceneManager.LoadScene(8);
    }

    private void OnSecondTalkToCanteenGuyEnding()
    {
        sceneData.numOfCanteenTask = 3;
        buttonToCanteenGuyTrigger.SetActive(false);
        controllingAnimator.SetBool("isOpen", true);
        
        string newTask = "Задание:";
        int k = 1;
        if (sceneData.numOfCanteenTask != 3)
        {
            newTask += $"\n{k++}) Посетить столовую";
        }
        if (sceneData.numOfLibraryTask != 3)
        {
            newTask += $"\n{k++}) Посетить библиотеку";
        }
        if (sceneData.numOfFTask != 3)
        {
            newTask += $"\n{k++}) Посетить корпус F";
        }
        sceneData.currentTask = newTask;
        task.ChangeTask(sceneData.currentTask);

        // Проверка на все задания....
        if (sceneData.numOfCanteenTask == 3 && sceneData.numOfFTask == 3 && sceneData.numOfLibraryTask == 3)
        {
            gameMessageBox.SetActive(true);
            controllingAnimator.SetBool("isOpen", false);
            string[] phrases =
            {
                "Что ж, ты посетил все основные локации...",
                "Теперь тебя ждёт финальное испытание!",
                "Предлагаю тебе пройти тестик. Считай, что это твой экзамен по БЖД",
                "Проверим, насколько хорошо ты усвоил то, что тебе рассказывали :)",
                "Удачи!"
            };
            messageTrigger.message = new Message(phrases);
            messageManager.OnGameMessageEnding += OnTestStarting;
            messageTrigger.TriggerMessage();
        }
    }

    private void OnTestStarting()
    {
        SceneManager.LoadScene(12);
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
