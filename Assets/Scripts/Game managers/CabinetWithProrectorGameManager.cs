using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetWithProrectorGameManager : MonoBehaviour
{
    public MessageManager messageManager;
    public MessageTrigger messageTrigger;
    public GameObject gameMessageBox;
    public LevelChanger levelChanger;

    public SceneData sceneData;

    private void Start()
    {
        gameMessageBox.SetActive(true);
        messageTrigger.TriggerMessage();
        messageManager.OnGameMessageEnding += OnMessageEnding;
    }

    private void OnMessageEnding()
    {
        sceneData.numOfTaskInRSecond = 2;
        levelChanger.FadeOnLevel();
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
