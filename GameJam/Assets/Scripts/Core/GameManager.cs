using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Loading screen", order = 0)]
    [SerializeField] [Range(.5f, 5f)] float _fadingTime = 2f;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] string _mainMenuSceneName;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    private GameObject _playerObject;
    private int _controlableObjectIndex;
    public static GameManager Instance;

    private List<IControlableObject> _controlableObject = new List<IControlableObject>();

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

    public GameObject GetPlayer()
    {
        return _playerObject;
    }

    public void SetupScene()
    {
        LevelData levelData = FindObjectOfType<LevelData>();
        if (levelData == null) return;

        _playerObject = Instantiate(_playerPrefab, levelData.PlayerSpawnPosition.position, levelData.PlayerSpawnPosition.rotation);
        FollowCam.Instance.ChangeTarget(_playerObject.transform);
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

        _controlableObject.Clear();
        GamePaused = false;

        if (!OnMainMenu())
        {
            SetupScene();
        }

        yield return null;
    }

    public void AddControlableObject(IControlableObject obj)
    {
        _controlableObject.Add(obj);
    }

    public void RemoveControlableObject(IControlableObject obj)
    {
        _controlableObject.Remove(obj);
    }

    public void ChangeControlableObjectSpecificFocus(IControlableObject target)
    {        
        _controlableObject[_controlableObjectIndex].DisableController();
        _controlableObjectIndex = _controlableObject.IndexOf(target);
        target.EnableController();

        FollowCam.Instance.ChangeTarget(target.GetTransform());
    }

    public void ChangeControlableObjectFocus()
    {
        _controlableObject[_controlableObjectIndex].DisableController();
        _controlableObjectIndex++;
        _controlableObjectIndex %= _controlableObject.Count;
        IControlableObject controlableObject = _controlableObject[_controlableObjectIndex];
        controlableObject.EnableController();

        FollowCam.Instance.ChangeTarget(controlableObject.GetTransform());
    }

}
