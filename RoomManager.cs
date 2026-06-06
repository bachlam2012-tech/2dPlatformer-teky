using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static RoomManager instance;
    [SerializeField] private Transform player;
    [Header("Main Scene Name (Never Unload)")]
    [SerializeField] private string mainSceneName = "Mainscene";
    private string currentRoomScene = "";

    private bool isLoading = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadRoom("Lvl1", "Spawn_start");
    }
    public void setPlayer(Transform playerTranform)
    {
        player = playerTranform;
    }
    public void LoadRoom(string roomSceneName, string spawnPointName)
    {
        StartCoroutine(LoadRoomCoroutine(roomSceneName, spawnPointName));
    }
    private IEnumerator LoadRoomCoroutine(string newRoom, string spawnPointName)
    {
        isLoading = true;
        // if (screenFader.instance != null)
        // {
        //     yield return ScreenFader.instance.FadeOutFast();
        // }
        if (newRoom == currentRoomScene)
            yield break;
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(newRoom, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;
        GameObject spawnObj = GameObject.Find(spawnPointName);
        if (spawnObj != null && player != null)
        {
            player.position = spawnObj.transform.position;
        }
        if (!string.IsNullOrEmpty(currentRoomScene) && currentRoomScene != mainSceneName)
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(currentRoomScene);
            while (!unloadOp.isDone)
                yield return null;
        }
        currentRoomScene = newRoom; ;
        // if (ScreenFader.instance != null)
        // {
        //     yield return ScreenFader.instance.FadeInFast();
        // }
        isLoading = true;
    }


}
