using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundEffectSlider;

    [SerializeField] private float _sliderMinValue;
    [SerializeField] private float _sliderMaxValue;

    [SerializeField] private Image _ps5Image;
    [SerializeField] private Image _xboxImage;
    [SerializeField] private Image _switchImage;

    private void Start()
    {
        _musicSlider.minValue = _sliderMinValue;
        _musicSlider.maxValue = _sliderMaxValue;

        _soundEffectSlider.minValue = _sliderMinValue;
        _soundEffectSlider.maxValue = _sliderMaxValue;

        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _soundEffectSlider.value = AudioManager.Instance.GetSoundEffectVolume();

        SetupGamepadImage();
    }

    private void SetupGamepadImage()
    {
        switch (Gamepad.current.displayName)
        {
            case "Playstation Controller":
                _ps5Image.gameObject.SetActive(true);
                break;

            case "Xbox Controller":
                _xboxImage.gameObject.SetActive(true);
                break;

            case "":
                _switchImage.gameObject.SetActive(true);
                break;
        }
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
