using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private PauseMenu _pauseMenu;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Instance of UIManager already exist");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void OnPause()
    {
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if (GameManager.Instance.OnMainMenu()) return;


        if (_pauseMenu.GetOptionMenuWasVisible())
        {
            _pauseMenu.ToggleOptionMenu();
            return;
        }

        _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeInHierarchy);        
    }
}
