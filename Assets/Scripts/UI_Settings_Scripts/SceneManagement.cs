using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /////////////////////////////////////
    // SCENES MANAGEMENT AND QUIT GAME //
    /////////////////////////////////////

/*    public void ToOptionScene()
    {
        SceneManager.LoadScene(1);
    }*/

    public void ToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene(0);
    }

/*    public void ToHowToPlay()
    {
        SceneManager.LoadScene(2);
    }*/

    public void QuitGame()
    {
        Application.Quit();
    }
}
