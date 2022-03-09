using UnityEngine;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private void LoadNextScene()
    {
        GameManager.Instance.LoadScene(_sceneIndex);
    }
}
