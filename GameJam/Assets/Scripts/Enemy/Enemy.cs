using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _canKillPlayer = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_canKillPlayer)
            {
                ModelController model = other.GetComponentInParent<ModelController>();
                if (model == null)
                {
                    //TODO Player part
                }
                else
                {
                    //Player
                    model.Die();
                }
            }
        }
    }

    public void ChangeCanKillPlayer(bool value)
    {
        _canKillPlayer = value;
    }
}
