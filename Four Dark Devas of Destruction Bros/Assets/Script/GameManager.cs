using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentSceneIndex = 0 ;
    public int playerLives;
    public int startLives;
    public Vector3 checkpoint;
    public GameObject playerPf;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;   
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogError("[GameManager] Attempted to create a second  instance of GameManager");
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpoint = new Vector3(0, 0, 0);
        instance.playerLives = instance.startLives;
    }
    public void PlayerDeath()
    {
        if (playerLives > 0)
        {
            playerLives -= 1;
            Destroy(playerPf);
            Instantiate(playerPf, checkpoint, playerPf.transform.rotation);
        }
        else
        {
            SceneManager.LoadScene("Game_over");
        }
    }
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene finished loading");
        currentSceneIndex = scene.buildIndex;
    }
    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }
    public void LoadPreviousScene()
    {
        LoadLevel(currentSceneIndex - 1);
    }
    public void exitScene()
    {
        //Using this void I will be able to assign it to the button to exit the game once the button is pressed.
        Debug.Log("It has been pressed!");
        Application.Quit();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LoadPreviousScene();
        }
    }
}
