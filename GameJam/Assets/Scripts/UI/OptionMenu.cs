using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundEffectSlider;

    [SerializeField] private float _sliderMinValue;
    [SerializeField] private float _sliderMaxValue;

    private void Start()
    {
        _musicSlider.minValue = _sliderMinValue;
        _musicSlider.maxValue = _sliderMaxValue;

        _soundEffectSlider.minValue = _sliderMinValue;
        _soundEffectSlider.maxValue = _sliderMaxValue;

        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _soundEffectSlider.value = AudioManager.Instance.GetSoundEffectVolume();
    }

    public void SoundEffectVolumeChanged(float value)
    {
        AudioManager.Instance.SoundEffectVolumeChanged(value);
    }

    public void MusicVolumeChanged(float value)
    {
        AudioManager.Instance.MusicVolumeChanged(value);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
