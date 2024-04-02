using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LibraryGameManager : MonoBehaviour
{
    public VectorValue pos;
    public GameObject player;
    public Phone phone;
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public GameObject gameMessageBox;
    public GameObject controlling;
    public GameObject taskObject;
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
        if (sceneData.isLibraryLoadedFirst)
        {
            sceneData.isLibraryLoadedFirst = false;

            controllingAnimator.SetBool("isOpen", false);
            string[] phrases =
            {
                "Молодец, ты добрался!",
                "Библиотека ВШЭ - это огромное пространство для студентов",
                "Это не просто место для чтения книг, но и огромный коворкинг, где ты можешь учиться безо всяких ограничений!",
                "Для студентов тут оборудованы зарядки и более 900 посадочных мест",
                "Работает библиотека круглосуточно",
                "Только не забудь сдать верхнюю одежду в гардероб перед её посещением!",
                "А ещё здесь есть как тихие, так и громкие зоны",
                "Если нужно будет удобное место для работы - ты знаешь где его искать!"
            };
            messageTrigger.message = new Message(phrases);
            messageManager.OnGameMessageEnding += OnGameMessegaEnding;
            messageTrigger.TriggerMessage();
            sceneData.numOfLibraryTask = 3;
        }

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    private void OnGameMessegaEnding()
    {
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
