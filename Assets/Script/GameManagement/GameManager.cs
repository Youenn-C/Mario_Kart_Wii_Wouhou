using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Players"), Space(5)]
    public CartController player1;
    public CartController player2;

    [Header("°Player 1"), Space(5)]
    [SerializeField] private int lapNumberP1;
    [SerializeField] private int checkpointTriggeredP1;
    
    [Header("°Player 2"), Space(5)]
    [SerializeField] private int lapNumberP2;
    [SerializeField] private int checkpointTriggeredP2;
    
    [Header("Title Screen & Main Menu")]
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsWindow;
    [Space(5)]
    [SerializeField] private GameObject _selectorPlayer1;
    [SerializeField] private GameObject _selectorPlayer2;
    [SerializeField] private GameObject _selectorUI;
    [SerializeField] private GameObject _tempCamera;
    
    [Space(5)]
    public string positionPlayer1;
    public string positionPlayer2;
    
    public void StartGame()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Game");
    }

    public void PlayGame()
    {
        _titleScreen.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSettingsWindow()
    {
        _settingsWindow.SetActive(!_settingsWindow.activeSelf);
    }

    public void SelectAvatars()
    {
        _mainMenu.SetActive(false);
        _selectorPlayer1.SetActive(true);
        _selectorPlayer2.SetActive(true);
        _selectorUI.SetActive(true);
        _tempCamera.SetActive(false);
    }

    void Update()
    {
        if (lapNumberP1 > lapNumberP2 && checkpointTriggeredP1 > checkpointTriggeredP2)
        {
            positionPlayer1 = "1";
            positionPlayer2 = "2";
        }
        else if (lapNumberP1 > lapNumberP2 && checkpointTriggeredP1 < checkpointTriggeredP2)
        {
            positionPlayer1 = "1";
            positionPlayer2 = "2";
        }
        
        else if (lapNumberP1 < lapNumberP2 && checkpointTriggeredP1 > checkpointTriggeredP2)
        {
            positionPlayer1 = "2";
            positionPlayer2 = "1";
        }
        else if (lapNumberP1 < lapNumberP2 && checkpointTriggeredP1 < checkpointTriggeredP2)
        {
            positionPlayer1 = "2";
            positionPlayer2 = "1";
        }
    }
}
