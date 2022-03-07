using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _soundEffectMixer;
    [SerializeField] private string _musicVolumeParameterName;
    [SerializeField] private string _soundEffectVolumeParameterName;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of AudioManager already exist");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void SoundEffectVolumeChanged(float value)
    {
        _soundEffectMixer.audioMixer.SetFloat(_soundEffectVolumeParameterName, value);
    }

    public void MusicVolumeChanged(float value)
    {
        _musicMixer.audioMixer.SetFloat(_musicVolumeParameterName, value);
    }

    public float GetMusicVolume()
    {
        float value;
        _musicMixer.audioMixer.GetFloat(_musicVolumeParameterName, out value);
        return value;
    }

    public float GetSoundEffectVolume()
    {
        float value;
        _soundEffectMixer.audioMixer.GetFloat(_soundEffectVolumeParameterName, out value);
        return value;
    }

    public void PlaySoundAt(AudioClip clip, Transform position)
    {
        GameObject obj = new GameObject() { name = "TempAudio" };
        obj.transform.position = position.position;

        obj.AddComponent<AudioSource>();
        AudioSource source = obj.GetComponent<AudioSource>();

        source.outputAudioMixerGroup = _soundEffectMixer;
        source.clip = clip;
        source.Play();

        Destroy(obj, clip.length);
    }
}
