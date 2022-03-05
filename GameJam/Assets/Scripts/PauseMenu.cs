using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private string _mainMenuSceneName;

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
        GameManager.Instance.LoadScene(_mainMenuSceneName);
    }

    public bool GetOptionMenuWasVisible()
    {
        return _optionMenu.activeInHierarchy;
    }
}
