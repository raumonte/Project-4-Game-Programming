using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void exitScene()
    {
        //Using this void I will be able to assign it to the button to exit the game once the button is pressed.
        Debug.Log("It has been pressed!");
        Application.Quit();

    }
    //This is a bit of a test to add into buttons for when click. But this void is for moving to another 
    public void LoadNextScene()
    {
        GameManager.instance.LoadLevel (GameManager.instance.currentSceneIndex + 1);
    }
}
