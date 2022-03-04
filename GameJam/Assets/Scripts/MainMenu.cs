using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionMenu;

    public void ToggleOptionMenu()
    {
        _optionMenu.SetActive(!_optionMenu.activeInHierarchy);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
