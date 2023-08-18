using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject endGameMenu;

    public bool _isMainMenuVisible = false;
    public float _currentTimeScale = 1.0f;

    private void Start()
    {
        PauseGame(false);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(true);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ToggleMainMenu()
    {
        this._isMainMenuVisible = !this._isMainMenuVisible;

        mainMenu.SetActive(_isMainMenuVisible);

        PauseGame(_isMainMenuVisible);
    }

    public void ShowMainMenu(bool show)
    {
        if (mainMenu != null) 
        { 
            mainMenu.SetActive(show); 
            _isMainMenuVisible = show;
        }

    }

    public void PauseGame(bool isPaused)
    {
        if (pauseMenu != null)
        {
            if (isPaused)
            {
                pauseMenu.SetActive(isPaused);
                ShowMainMenu(true);

                //GameManager.instance.SwitchState(States.Paused);
            }
            else
            {
                pauseMenu.SetActive(isPaused);
                ShowMainMenu(false);

                //GameManager.instance.SwitchState(States.Running);
            }
        }
    }

    public void EndGameMenu()
    {
        endGameMenu.SetActive(true);

        //GameManager.instance.SwitchState(States.EndGame);

        
    }
}
