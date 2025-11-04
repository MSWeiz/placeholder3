// BackButton.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
        Debug.Log($"Текущая сцена: {currentScene}, Загружаем: MainMenu");

        // Просто загружаем сцену MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}