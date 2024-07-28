using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    

    public void mainmenu()
    {
        SceneManager.LoadScene("mainmenu");

    }

    public void restartlastevel()
    {
        if (!string.IsNullOrEmpty(GameData.LastScene))
        {
            SceneManager.LoadScene(GameData.LastScene);
        }
        else
        {
            Debug.LogWarning("Last scene not set.");
        }
    }

    public void quit()
    {

       
        Application.Quit();
    }
}
