using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LabManualUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI aimText;
    public TextMeshProUGUI principleText;
    public TextMeshProUGUI requirementsText;
    public TextMeshProUGUI chemicalsText;
    public Button startButton;

    public ExperimentDefinition[] experiments;

    private ExperimentDefinition current;

    private void Start()
    {
        var key = PlayerPrefs.GetString("current_experiment_key", "Experiment1");
        foreach (var e in experiments)
        {
            if (e.experimentKey == key) { current = e; break; }
        }
        if (current == null && experiments.Length > 0) current = experiments[0];

        if (current != null)
        {
            titleText.text = current.title;
            aimText.text = current.aim;
            principleText.text = current.principle;
            requirementsText.text = current.requirements;
            chemicalsText.text = current.chemicals;
        }

        if (startButton != null) startButton.onClick.AddListener(() => SceneManager.LoadScene("ARExperiment"));
    }
}
