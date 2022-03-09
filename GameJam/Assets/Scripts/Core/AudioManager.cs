using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _soundEffectMixer;
    [SerializeField] private AudioMixerGroup _ambianceMixer;
    [SerializeField] private string _musicVolumeParameterName;
    [SerializeField] private string _soundEffectVolumeParameterName;
    [SerializeField] private string _ambiancetVolumeParameterName;

    [SerializeField] private AudioClip[] _musics;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _ambiances;
    [SerializeField] private AudioSource _ambianceSource;
    [SerializeField] private AudioClip _cinematicClip;

    private bool _ambianceStarted;
    private int _musicIndex;
    private int _ambianceIndex;

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

    private void Start()
    {
        _musicIndex = 0;
        StartMusic();
    }

    public void PlayCinematicMusic()
    {
        StopCoroutine(nameof(PlayNextMusic));
        _musicSource.clip = _cinematicClip;
        _musicSource.Play();
    }

    public void StopCinematicMusic()
    {
        StartMusic();
    }

    public void StartAmbianceSound()
    {
        if (_ambiances.Length == 0) return;
        if (_ambianceStarted) return;
        _ambianceStarted = true;

        AudioClip clip = _ambiances[_musicIndex];
        _ambianceSource.clip = clip;
        _ambianceSource.Play();

        StartCoroutine(PlayNextAmbiance(clip.length + 1));
    }

    private IEnumerator PlayNextAmbiance(float duration)
    {
        yield return new WaitForSeconds(duration);
        _ambianceIndex++;
        _ambianceIndex %= _ambiances.Length;

        AudioClip clip = _ambiances[_musicIndex];
        _ambianceSource.clip = clip;
        _ambianceSource.Play();

        StartCoroutine(PlayNextAmbiance(clip.length + 1));
    }

    private void StartMusic()
    {
        if (_musics.Length == 0) return;
        AudioClip clip = _musics[_musicIndex];
        _musicSource.clip = clip;
        _musicSource.Play();

        StartCoroutine(PlayNextMusic(clip.length + 1));
    }

    private IEnumerator PlayNextMusic(float duration)
    {
        yield return new WaitForSeconds(duration);
        _musicIndex++;
        _musicIndex %= _musics.Length;

        AudioClip clip = _musics[_musicIndex];
        _musicSource.clip = clip;
        _musicSource.Play();

        StartCoroutine(PlayNextMusic(clip.length + 1));
    }

    public void SoundEffectVolumeChanged(float value)
    {
        _soundEffectMixer.audioMixer.SetFloat(_soundEffectVolumeParameterName, value);
    }

    public void MusicVolumeChanged(float value)
    {
        _musicMixer.audioMixer.SetFloat(_musicVolumeParameterName, value);
    }

    public void AmbianceVolumeChanged(float value)
    {
        _ambianceMixer.audioMixer.SetFloat(_ambiancetVolumeParameterName, value);
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

    public float GetAmbianceVolume()
    {
        float value;
        _ambianceMixer.audioMixer.GetFloat(_ambiancetVolumeParameterName, out value);
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
