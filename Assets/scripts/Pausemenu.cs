using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // The UI panel for the pause menu
    public Button resumeButton;
    public Button restartbutton;
    public Button quitButton;
    
    public Button pausebutton;
    public movements movements;


    private bool isPaused = false;

    void Start()
    {
        
        pauseMenuUI.SetActive(false);
        movements= GameObject.FindWithTag("Player").GetComponent<movements>();
        // Assign button listeners
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(QuitGame);
        restartbutton.onClick.AddListener(RestartLevel);
    }

    void Update()
    {
        if (movements.inputasset.Player.Pause.triggered || movements.inputasset.UI.pause.triggered)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pausebutton.gameObject.SetActive(true);
        movements.inputasset.Player.Enable();
        movements.inputasset.UI.Disable();
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pausebutton.gameObject.SetActive(false);
        //inputasset.Player.Disable();
        movements.inputasset.Player.Disable();
        movements.inputasset.UI.Enable();
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    void QuitGame()
    {
        Time.timeScale = 1f; // Reset time scale before quitting

        SceneManager.LoadScene("mainmenu"); // Assumes you have a scene named "MainMenu"
    }

    void RestartLevel()
    {
        Time.timeScale = 1f; // Reset time scale before restarting
        //inputasset.Player.Enable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
