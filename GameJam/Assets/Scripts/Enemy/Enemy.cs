using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip[] _walkSounds;
    [SerializeField] private AudioClip _screamSound;
    [SerializeField] private float _minScreamSoundDuration;
    [SerializeField] private float _maxScreamSoundDuration;

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
                    if (!model.Alive) return;
                    model.Die();
                }
            }
        }
    }

    private void Start()
    {
        StartCoroutine(PlayScreamSound());
    }

    public void ChangeCanKillPlayer(bool value)
    {
        _canKillPlayer = value;
    }

    public void PlayWalkSound()
    {
        int index = Random.Range(0, _walkSounds.Length);
        AudioManager.Instance.PlaySoundAt(_walkSounds[index], transform);
    }

    private IEnumerator PlayScreamSound()
    {
        while (true && _screamSound != null)
        {
            float duration = Random.Range(_minScreamSoundDuration, _maxScreamSoundDuration);
            yield return new WaitForSeconds(duration);
            AudioManager.Instance.PlaySoundAt(_screamSound, transform);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
