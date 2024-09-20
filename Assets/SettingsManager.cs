using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure to include this for TMP_Dropdown
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider ballSpeedSlider;
    [SerializeField] private Slider paddleSizeSlider;
    [SerializeField] private Slider matchDurationSlider;
    [SerializeField] private TMP_Dropdown aiDifficultyDropdown; // Use TMP_Dropdown
    [SerializeField] private Button saveButton;

    private void Start()
    {
        // Check if all UI components are assigned
        if (ballSpeedSlider == null || paddleSizeSlider == null || matchDurationSlider == null || aiDifficultyDropdown == null || saveButton == null)
        {
            Debug.LogError("One or more UI components are not assigned.");
            return;
        }

        // Initialize sliders and dropdown with default values
        InitializeUI();

        // Load settings when the scene starts
        LoadSettings();

        // Add listener for the Save button
        saveButton.onClick.AddListener(SaveSettings);

        // Add listener for dropdown value change
        aiDifficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
    }

    private void InitializeUI()
    {
        // Set sliders to their default midpoint values
        ballSpeedSlider.minValue = 0f;
        ballSpeedSlider.maxValue = 20f; // Example max value
        ballSpeedSlider.value = (ballSpeedSlider.minValue + ballSpeedSlider.maxValue) / 2;

        paddleSizeSlider.minValue = 0.5f; // Example min value
        paddleSizeSlider.maxValue = 2f;   // Example max value
        paddleSizeSlider.value = (paddleSizeSlider.minValue + paddleSizeSlider.maxValue) / 2;

        matchDurationSlider.minValue = 30f; // Example min value
        matchDurationSlider.maxValue = 120f; // Example max value
        matchDurationSlider.value = (matchDurationSlider.minValue + matchDurationSlider.maxValue) / 2;

        // Set dropdown options
        aiDifficultyDropdown.ClearOptions();
        aiDifficultyDropdown.AddOptions(new List<string> { "Easy", "Medium", "Hard" });

        // Default dropdown value
        aiDifficultyDropdown.value = 1; // Medium
    }

    private void LoadSettings()
    {
        // Retrieve settings from PlayerPrefs or use default values
        float ballSpeed = PlayerPrefs.GetFloat("BallSpeed", ballSpeedSlider.value);
        float paddleSize = PlayerPrefs.GetFloat("PaddleSize", paddleSizeSlider.value);
        float matchDuration = PlayerPrefs.GetFloat("MatchDuration", matchDurationSlider.value);
        int aiDifficultyIndex = PlayerPrefs.GetInt("AIDifficulty", aiDifficultyDropdown.value); // Load AI difficulty index

        // Set sliders and dropdown to current settings
        ballSpeedSlider.value = ballSpeed;
        paddleSizeSlider.value = paddleSize;
        matchDurationSlider.value = matchDuration;
        aiDifficultyDropdown.value = aiDifficultyIndex;
    }

    private void SaveSettings()
    {
        // Get values from sliders and dropdown
        float ballSpeed = ballSpeedSlider.value;
        float paddleSize = paddleSizeSlider.value;
        float matchDuration = matchDurationSlider.value;
        int aiDifficultyIndex = aiDifficultyDropdown.value; // Save AI difficulty index

        // Save settings to PlayerPrefs
        PlayerPrefs.SetFloat("BallSpeed", ballSpeed);
        PlayerPrefs.SetFloat("PaddleSize", paddleSize);
        PlayerPrefs.SetFloat("MatchDuration", matchDuration);
        PlayerPrefs.SetInt("AIDifficulty", aiDifficultyIndex); // Save AI difficulty index

        // Optionally, provide feedback to the user
        Debug.Log("Settings saved!");
    }

    private void OnDifficultyChanged(int index)
    {
        // Optional: Handle difficulty change if needed
        Debug.Log("Difficulty changed to: " + aiDifficultyDropdown.options[index].text);
    }
}
