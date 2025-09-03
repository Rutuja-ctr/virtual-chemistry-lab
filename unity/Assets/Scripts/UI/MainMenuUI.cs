using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button experiment1Button;
    public Button experiment2Button;

    private void Awake()
    {
        if (experiment1Button != null) experiment1Button.onClick.AddListener(() => LoadLabManual("Experiment1"));
        if (experiment2Button != null) experiment2Button.onClick.AddListener(() => LoadLabManual("Experiment2"));
    }

    private void LoadLabManual(string experimentKey)
    {
        PlayerPrefs.SetString("current_experiment_key", experimentKey);
        SceneManager.LoadScene("LabManual");
    }
}
