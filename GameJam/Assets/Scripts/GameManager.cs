using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    public static GameManager Instance;

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
}
