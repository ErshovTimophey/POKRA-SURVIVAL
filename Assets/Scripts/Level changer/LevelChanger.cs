using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    public int levelToLoad;
    public Vector3 positionToLoad;
    public VectorValue playerStorage;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void FadeOnLevel()
    {
        animator.SetTrigger("fade");
    }

    public void OnFadeComplete()
    {
        playerStorage.value = positionToLoad;
        SceneManager.LoadScene(levelToLoad);
    }
}
