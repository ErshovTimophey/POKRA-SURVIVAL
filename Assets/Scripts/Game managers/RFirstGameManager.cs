using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RFirstGameManager : MonoBehaviour
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

        if (sceneData.isRFirstLoadedFirst)
        {
            sceneData.isRFirstLoadedFirst = false;

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

        if (sceneData.numOfLibraryTask >= 2)
        {
            admission.SetActive(true);
        }
    }

    private void OnFirstGameMessageEnding()
    {
        controllingAnimator.SetBool("isOpen", true);
        sceneData.currentTask = "Задание:\nПоднимись на 2-ой этаж в R";
        task.ChangeTask(sceneData.currentTask);
    }

    private void OnApplicationQuit()
    {
        sceneData.isAtriumLoadedFirst = true;
    }
}
