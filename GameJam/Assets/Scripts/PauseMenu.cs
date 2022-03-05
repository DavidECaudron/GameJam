using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private int _mainMenuSceneIndex;

    public void ResumeGame()
    {
        UIManager.Instance.TogglePauseMenu();
    }

    public void ToggleOptionMenu()
    {
        _optionMenu.SetActive(!_optionMenu.activeInHierarchy);
    }

    public void BackToMainMenu()
    {        
        GameManager.Instance.LoadScene(_mainMenuSceneIndex);
        UIManager.Instance.TogglePauseMenu();
    }

    public bool GetOptionMenuWasVisible()
    {
        return _optionMenu.activeInHierarchy;
    }

    private void OnEnable()
    {
        GameManager.Instance.GamePaused = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.GamePaused = false;
    }
}
