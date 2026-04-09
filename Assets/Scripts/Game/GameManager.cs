using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private NotificationScreen _gameVictoryScreen;
    [SerializeField] private NotificationScreen _gameOverScreen;

    private void Awake()
    {
        DisableCursor();
    }

    private void OnEnable()
    {
        _player.Died += PlayerDie;
        _gameOverScreen.ButtonClicked += Restart;
        _gameVictoryScreen.ButtonClicked += Restart;
        _waveManager.AllWavesCompleted += Victory;
    }

    private void OnDisable()
    {
        _player.Died -= PlayerDie;
        _gameOverScreen.ButtonClicked -= Restart;
        _gameVictoryScreen.ButtonClicked -= Restart;
        _waveManager.AllWavesCompleted -= Victory;
    }

    private void PlayerDie()
    {
        Time.timeScale = 0;

        _gameOverScreen.Open();
        EnableCursor();
    }


    private void Restart()
    {
        Time.timeScale = 1;

        _player.Reset();
        _waveManager.Reset();

        DisableCursor();
    }

    private static void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private static void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Victory()
    {
        Time.timeScale = 0; 
        _gameVictoryScreen.Open(); 
    }
}