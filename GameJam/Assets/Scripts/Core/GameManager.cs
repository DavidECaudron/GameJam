using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Loading screen", order = 0)]
    [SerializeField] [Range(.5f, 5f)]float _fadingTime = 2f;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] string _mainMenuSceneName;

    [SerializeField] GameObject _enemyPrefab;

    private Player _player;
    public static GameManager Instance;

    public bool GamePaused
    {
        get
        {
            return Time.timeScale == 0f;
        }
        set
        {
            Time.timeScale = value ? 0f : 1f;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Instance of GameManager already exist");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public bool OnMainMenu()
    {
        return SceneManager.GetActiveScene().name == _mainMenuSceneName;
    }

    public Player GetPlayer()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        return _player;
    }

    public void SetupScene()
    {
        LevelData levelData = FindObjectOfType<LevelData>();
        if (levelData == null) return;

        StartCoroutine(SpawnEnemy(levelData.SpawnEnemyTime, levelData.EnemySpawnPosition));
    }

    private IEnumerator SpawnEnemy(float waitingTime, Transform trsf)
    {
        yield return new WaitForSeconds(waitingTime);
        Instantiate(_enemyPrefab, trsf.position, trsf.rotation);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneWithIndex(sceneIndex));
    }

    private IEnumerator LoadSceneWithIndex(int sceneIndex)
    {
        #region Fade In
        _canvasGroup.alpha = 0;
        _canvasGroup.gameObject.SetActive(true);

        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime / _fadingTime;
            yield return null;
        }
        #endregion

        StopAllCoroutines();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncOperation.isDone)
        {
            yield return new WaitForSeconds(.1f);
        }

        #region Fade Out
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime / _fadingTime;
            yield return null;
        }

        _canvasGroup.gameObject.SetActive(false);
        #endregion

        GamePaused = false;

        if (!OnMainMenu())
        {
            SetupScene();
        }

        yield return null;
    }

}
