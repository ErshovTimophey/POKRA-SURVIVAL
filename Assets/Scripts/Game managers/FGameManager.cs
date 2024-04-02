using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FGameManager : MonoBehaviour
{
    public VectorValue pos;
    public GameObject player;
    public Phone phone;
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public MessageManager messageManagerWithChosing;
    public MessageTrigger messageTriggerWithChosing;
    public GameObject gameMessageBox;
    public GameObject gameMessageBoxWithChosing;
    public GameObject controlling;
    public GameObject taskObject;
    public GameObject buttonBackTrigger;
    public GameObject buttonToCryingGuyTrigger;
    public GameObject admission;
    public GameObject cryingGuy;
    public GameObject happyGuy;

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

        // Когда игрок первый раз входит в F
        if (sceneData.numOfFTask == 0)
        {
            sceneData.numOfFTask = 1;

            sceneData.currentTask = "Задание:\nПоговори с плачущим мальчиком";
            task.ChangeTask(sceneData.currentTask);
            buttonBackTrigger.SetActive(false);
        }

        // После окончания задания в столовой
        if (sceneData.numOfFTask == 3)
        {
            buttonToCryingGuyTrigger.SetActive(false);
            if (sceneData.isFGuyHappy)
            {
                cryingGuy.SetActive(false);
                happyGuy.SetActive(true);
            }
        }

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    public void OnTalkToCryingGuy()
    {
        buttonToCryingGuyTrigger.SetActive(false);
        messageManager.OnGameMessageEnding += OnFirstTalkToCanteenGuyEnding;
        controllingAnimator.SetBool("isOpen", false);
        messageTrigger.TriggerMessage();
    }

    private void OnFirstTalkToCanteenGuyEnding()
    {
        gameMessageBoxWithChosing.SetActive(true);
        messageTriggerWithChosing.TriggerMessage();
    }

    public void OnChosingEndingBad()
    {
        gameMessageBoxWithChosing.SetActive(false);
        string[] phrases =
        {
            "Парень:\nБольше ничего тебе не скажу, токсик!"
        };
        messageTrigger.message = new Message(phrases);
        messageManager.OnGameMessageEnding += OnSecondTalkToCanteenGuyEnding;
        messageTrigger.TriggerMessage();
    }

    public void OnChosingEndingGood()
    {
        gameMessageBoxWithChosing.SetActive(false);
        sceneData.isFGuyHappy = true;
        string[] phrases =
        {
            "Парень:\nДа я на занятии... *плак-плак*... и там этот потерялся...",
            "Ты:\nРасскажи, что ты потерял на занятии?",
            "Парень:\nЯ потерял свой ноутбук...",
            "Ты:\nКак давно это произошло?",
            "Парень:\nВот буквально минут 10 назад...",
            "Ты:\nМожет, твой ноутбук всё ещё лежит там?",
            "Парень:\nНет... я там только что был, там его уже нет...",
            "Ты:\nЯ слышал, что в Вышке есть комната администрации, куда можно отдать найденные забытые вещи",
            "Ты:\nМожет, твой ноутбук кто-то нашёл и принёс в эту комнату?",
            "Парень:\nОй, точно! Я об этом не подумал как-то...",
            "Парень:\nСпасибо за помощь, попробую там посмотреть!",
            "Ты:\nДа не за что!",
            "Парень:\nЕсли он там, то теперь я наконец-то смогу выбрать себе майнор и НИС",
            "Ты:\nА что это за майнор и НИС?",
            "Парень:\nМайнор - это предмет, который никак не связан с твоей основной программой обучения",
            "Парень:\nС его помощью студенты расширяют кругозор и изучают новые инетересные для них сферы",
            "Парень:\nМайноры начинаются во 2-ом курсе и длятся 2 учебных года",
            "Парень:\nА НИС - это дополнительный предмет по твоей основной специальности",
            "Парень:\nНИС и майнор обязательно нужно выбирать каждому студенту",
            "Ты:\nУх ты, звучит прикольно, спасибки!",
            "Парень:\nВсегда пожалуйста, удачи!",
        };
        messageTrigger.message = new Message(phrases);
        messageManager.OnGameMessageEnding += OnSecondTalkToCanteenGuyEnding;
        messageTrigger.TriggerMessage();
    }

    private void OnSecondTalkToCanteenGuyEnding()
    {
        sceneData.numOfFTask = 3;
        buttonToCryingGuyTrigger.SetActive(false);
        gameMessageBoxWithChosing.SetActive(false);
        controllingAnimator.SetBool("isOpen", true);
        buttonBackTrigger.SetActive(true);

        if (sceneData.isFGuyHappy)
        {
            cryingGuy.SetActive(false);
            happyGuy.SetActive(true);
        }
        
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

        // Проверка на все задания
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
