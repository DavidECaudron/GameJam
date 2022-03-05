using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Loading screen", order = 0)]
    [SerializeField] [Range(.5f, 5f)]float _fadingTime = 2f;
    [SerializeField] CanvasGroup _canvasGroup;

    private Player _player;
    public static GameManager Instance;

    float remainingTime = 5f;

    private void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
            remainingTime = float.MaxValue;
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

    public Player GetPlayer()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        return _player;
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

        yield return null;
    }

}
