using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Game : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private KillZone _killZone;
    [SerializeField] private Sprite _pauseButtonSprite, _playButtonSprite;
    [SerializeField] private GameMenu _menu;
    [SerializeField] private TMP_Text _pauseText;
    [SerializeField] private AudioClip _gameTrack;
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private EndGameScreen _winScreen, _loseScreen;

    private AudioSource _audioSource;
    private bool _paused;
    private bool _wavesAreOver;
    private bool _enemyesAreOver;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonPressed);
        _loseScreen.MenuButtonClicked += OnMenuButtonClick;

        _loseScreen.RestartButtonClicked += OnRestartButtonClick;

        _winScreen.MenuButtonClicked += OnMenuButtonClick;
        _winScreen.RestartButtonClicked += OnRestartButtonClick;

        _menu.AnyPanelOpened += OnMenuOpened;
        _menu.MenuClosed += OnMenuClosed;

        _player.AllEnemyesPassed += OnAllEnemyesPassed;
        _player.AllEnemyesKilled += OnAllEnemyKilled;

        _spawner.AllWavesFinnished += OnAllWawesFinnished;
        _spawner.EnemySpawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonPressed);

        _loseScreen.MenuButtonClicked -= OnMenuButtonClick;
        _loseScreen.RestartButtonClicked -= OnRestartButtonClick;

        _winScreen.MenuButtonClicked -= OnMenuButtonClick;
        _winScreen.RestartButtonClicked -= OnRestartButtonClick;

        _menu.AnyPanelOpened -= OnMenuOpened;
        _menu.MenuClosed -= OnMenuClosed;

        _player.AllEnemyesPassed -= OnAllEnemyesPassed;
        _player.AllEnemyesKilled -= OnAllEnemyKilled;

        _spawner.AllWavesFinnished -= OnAllWawesFinnished;
        _spawner.EnemySpawned -= OnEnemySpawned;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StopGame();

        _audioSource.clip = _gameTrack;
        _audioSource.Play();
        _pauseButton.image.sprite = _pauseButtonSprite;
        _pauseText.gameObject.SetActive(true);
    }

    private void OnPauseButtonPressed()
    {
        if (_paused)
        {
            StartGame();
            _pauseText.gameObject.SetActive(false);
        }
        else
        {
            StopGame();
            _pauseText.gameObject.SetActive(true);
        }
    }

    private void OnAllEnemyesPassed()
    {
        StopGame();
        _loseScreen.gameObject.SetActive(true);
    }

    private void OnMenuOpened()
    {
        StopGame();
    }

    private void OnMenuClosed()
    {
        StartGame();
    }

    private void OnMenuButtonClick()
    {
        MainMenu.Load();
    }

    private void OnRestartButtonClick()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        _winScreen.gameObject.SetActive(false);
        _loseScreen.gameObject.SetActive(false);
        StartGame();
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        _player.NewEnemyFound(enemy);
    }

    private void OnAllWawesFinnished()
    {
        _wavesAreOver = true;
    }

    private void OnAllEnemyKilled()
    {
        if (_wavesAreOver)
        {
            StopGame();
            _winScreen.gameObject.SetActive(true);
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        _pauseButton.image.sprite = _playButtonSprite;
        _paused = true;
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _pauseButton.image.sprite = _pauseButtonSprite;
        _paused = false;
    }
}