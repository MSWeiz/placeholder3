// SettingsManager.cs
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("UI Elements")]
    public Slider musicSlider;
    public Slider sensitivitySlider;
    public Button backButton;

    [Header("Settings Keys")]
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string MOUSE_SENSITIVITY_KEY = "MouseSensitivity";

    private void Start()
    {
        LoadSettings();
        SetupEventListeners();
    }

    private void SetupEventListeners()
    {
        // Подписываемся на события слайдеров
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        // Подписываемся на кнопку назад
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnMusicVolumeChanged(float value)
    {
        // Конвертируем линейное значение в децибелы
        float volumeDB = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MusicVolume", volumeDB);

        // Сохраняем настройку
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
    }

    private void OnSensitivityChanged(float value)
    {
        // Сохраняем чувствительность мыши
        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY_KEY, value);
    }

    private void LoadSettings()
    {
        // Загружаем настройки или устанавливаем значения по умолчанию
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.75f);
        float sensitivity = PlayerPrefs.GetFloat(MOUSE_SENSITIVITY_KEY, 1.0f);

        // Применяем настройки к UI
        musicSlider.value = musicVolume;
        sensitivitySlider.value = sensitivity;

        // Применяем аудио настройки
        float volumeDB = Mathf.Log10(musicVolume) * 20;
        audioMixer.SetFloat("MusicVolume", volumeDB);
    }

    private void OnBackButtonClicked()
    {
        // Сохраняем все настройки
        PlayerPrefs.Save();

        // Возвращаемся на предыдущую сцену
        string previousScene = PlayerPrefs.GetString("PreviousScene", "MainMenu");
        SceneManager.LoadScene(previousScene);
    }

    // Статический метод для получения чувствительности из других скриптов
    public static float GetMouseSensitivity()
    {
        return PlayerPrefs.GetFloat(MOUSE_SENSITIVITY_KEY, 1.0f);
    }
}