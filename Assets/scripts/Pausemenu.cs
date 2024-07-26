using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // The UI panel for the pause menu
    public Button resumeButton;
    public Button restartbutton;
    public Button quitButton;
    public TemporalOdyssey inputasset;
    public Button pausebutton;


    private bool isPaused = false;

    void Start()
    {
        inputasset = new TemporalOdyssey();
        inputasset.Player.Enable();
        pauseMenuUI.SetActive(false);

        // Assign button listeners
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(QuitGame);
        restartbutton.onClick.AddListener(RestartLevel);
    }

    void Update()
    {
        if (inputasset.Player.Pause.triggered || inputasset.UI.Pause.triggered)
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
        inputasset.Player.Enable();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pausebutton.gameObject.SetActive(false);
        inputasset.Player.Disable();
        inputasset.UI.Enable();
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
        inputasset.Player.Enable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
