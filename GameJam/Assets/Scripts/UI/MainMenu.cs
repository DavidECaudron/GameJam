using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private GameObject _resumeBtn;
    [SerializeField] private GameObject _chooseLvlMenu;

    private void Start()
    {
        //TODO vérifier si il y a une sauvegarde, si non cacher bouton resume
        SaveData save = SaveManager.Instance.GetSaveData();
        if(save.LevelReached <= 0)
        {
            _resumeBtn.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        //TODO load level save.LevelReached
        SaveData save = SaveManager.Instance.GetSaveData();
    }

    public void NewGame()
    {
        //TODO load level 1
    }

    public void ChooseLevel()
    {
        _chooseLvlMenu.SetActive(!_chooseLvlMenu.activeInHierarchy);
    }

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
