using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // This sets the Game Manager as an instance.
    public static GameManager instance;
    //This gives value to the scene index by using integers.
    public int currentSceneIndex = 0 ;
    //This sets the player lives as an integer.
    public int playerLives;
    //This sets the start of lives as an integer.
    public int startLives;
    // This has the checkpoint set a s a vector 3 to get the location of where it is.
    public Vector3 checkpoint;
    public GameObject playerPf;                     //the prefab to instantiate again updon player death
    public int playerPoint;
    public Text pointCounter;
    public Text lifeCounter;

    [HideInInspector]
    public GameObject currentPlayerPawn;            //the current player character in the scene
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;   
    }
    private void Awake()
    {
        //This sets the game object as the game manager. Then adds a destroy on load that will take it through other scenes without destroying it.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //When there is more than one instance of a game manager it will delete the second one and 
        else
        {
            Debug.Log("[GameManager] Attempted to create a second  instance of GameManager");
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //when the game starts the spawn point is set to the origin.
        checkpoint = new Vector3(0, 0, 0);
        //when the game starts it sets the amount of playeer lives that the designer put in to the editor.
        instance.playerLives = instance.startLives;

    }
    public void PlayerDeath()
    {
        //Whenever the player's lives are more than 0 and the function activates. It begins to subtract the amount of lives while also destroying and respawning the player.
        if (playerLives > 0)
        {
            playerLives -= 1;
            Destroy(currentPlayerPawn);
            Instantiate(playerPf, checkpoint, playerPf.transform.rotation);
            lifeCounter.text = "Lives Left: 0" + playerLives.ToString();
        }
        //If the amount of lives reach under zero that is when the Game Over scene loads in.
        else
        {
            SceneManager.LoadScene("Game_over");
        }
    }
    //This sets the level index to the current build index of the project.
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    //This would get the level name through the build index.
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    //Once scene is loaded within the console it will tell the developer that the scene has been loaded.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene finished loading");
        currentSceneIndex = scene.buildIndex;
        lifeCounter.text = "Lives Left: 0" + playerLives.ToString();
    }
    //This function helps to load the scene by getting the scene index and adding 1 to go into the next of the build index.
    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }
    //This was an add in where I tested if it went back to the other scenes before it.
    public void LoadPreviousScene()
    {
        LoadLevel(currentSceneIndex - 1);
    }
    public void Victory()
    {
        if (playerPoint == 400)
        {
            LoadLevel("Victory");
        }
        pointCounter.text ="Points: " + playerPoint.ToString();
    }
// Update is called once per frame
void Update()
    {
    }
}
