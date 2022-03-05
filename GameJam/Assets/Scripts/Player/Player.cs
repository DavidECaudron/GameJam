using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void OnValueChanged();
    public event OnValueChanged OnPlayerDie;

    public bool Alive = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        Alive = false;
        OnPlayerDie?.Invoke();
    }
}
