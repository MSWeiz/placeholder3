// SettingsButton.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    public void OnSettingsButtonClicked()
    {
        // Сохраняем текущую сцену для возврата назад
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
        Debug.Log($"Текущая сцена: {currentScene}, Загружаем: Settings");

        // Пробуем загрузить сцену
        SceneManager.LoadScene("Settings");
    }
}