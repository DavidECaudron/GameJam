using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(SphereCollider))]
public class Enemy : MonoBehaviour
{
    private FollowTarget _followTarget;

    private void Start()
    {
        _followTarget = GetComponent<FollowTarget>();
        Player player = GameManager.Instance.GetPlayer();
        player.OnPlayerDie += _followTarget.StopFollow;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            player.Die();
            _followTarget.StopFollow();
        }
    }
}
