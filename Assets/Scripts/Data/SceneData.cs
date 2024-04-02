using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneData : ScriptableObject
{
    public bool isAtriumLoadedFirst = true;
    public int numOfTaskInNFirst = 0;
    public bool isRFirstLoadedFirst = true;
    public bool isCuratorSceneLoadedFirst = true;
    public int numOfTaskInRSecond = 0;
    public bool isLibraryLoadedFirst = true;

    public int numOfCanteenTask = 0;
    public int numOfLibraryTask = 0;
    public int numOfFTask = 0;

    public bool isFGuyHappy = false;

    public string currentTask;
}
