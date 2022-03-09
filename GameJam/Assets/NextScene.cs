using UnityEngine;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private void LoadNextScene()
    {
        AudioManager.Instance.StopCinematicMusic();
        GameManager.Instance.LoadScene(_sceneIndex);
    }
}
