using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetWithCuratorGameManager : MonoBehaviour
{
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public GameObject gameMessageBox;
    public LevelChanger levelChanger;

    public SceneData sceneData;

    private void Start()
    {
        if (sceneData.isCuratorSceneLoadedFirst)
        {
            sceneData.isCuratorSceneLoadedFirst = false;
            gameMessageBox.SetActive(true);
            levelChanger.levelToLoad = 6;
            messageTrigger.TriggerMessage();
            messageManager.OnGameMessageEnding += OnFirstMessageEnding;
        }
        else
        {
            messageManager.OnGameMessageEnding += OnSecondMessageEnding;
            gameMessageBox.SetActive(true);
            string[] phrases =
            {
                "Отлично! А теперь давайте я расскажу про кое-что поподробнее",
                "В Вышке существует СОП - это студенческая оценка преподавания",
                "Все студенты обязаны оценивать в СОПе своих учителей перед каждой сессий",
                "В противном случае студент получает дисциплинарку и может быть даже отчислен",
                "Ещё важно помнить: чтобы не получить пересдачу, предмет надо закрыть хотя бы на 4/10 балла!",
                "Любой элемент в формуле оценивания по предмету может весить до 70%",
                "Ну и конечно Пётр I к Вышке никак не причастен. ВШЭ - это относительно молодой и стремительно развивающийся вуз!",
                "Кстати, Вышка славится своей внеучебной жизнью! Здесь есть куча спортивных и творческих секций, например клуб дебатов или танцы",
                "Что ж, основные вещи мы узнали. Теперь предлагаю вам самим поизучать стены нашего вуза",
                "Предлагаю вам посетить Столовую, Библиотеку и корпус F",
                "До встречи!"
            };
            messageTrigger.message = new Message(phrases);
            levelChanger.levelToLoad = 3;
            messageTrigger.TriggerMessage();
        }
        
    }

    private void OnFirstMessageEnding()
    {
        levelChanger.FadeOnLevel();
    }

    private void OnSecondMessageEnding()
    {
        sceneData.numOfTaskInRSecond = 5;
        levelChanger.levelToLoad = 3;
        levelChanger.FadeOnLevel();
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
