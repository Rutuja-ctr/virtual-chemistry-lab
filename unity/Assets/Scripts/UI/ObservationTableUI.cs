using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObservationTableUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI ruleSummaryText;
    public TextMeshProUGUI resultText;
    public Button quizButton;

    public TurbidityController sample;
    public TurbidityController standard;

    private void Awake()
    {
        if (panel != null) panel.SetActive(false);
        if (quizButton != null) quizButton.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene("Quiz"));
    }

    public void Show(ExperimentDefinition def)
    {
        if (panel != null) panel.SetActive(true);
        if (ruleSummaryText != null) ruleSummaryText.text = def.observationRuleSummary;

        bool pass = sample != null && standard != null && sample.turbidity <= standard.turbidity + 1e-3f;
        if (resultText != null)
        {
            resultText.text = pass ? "Result: PASS (Sample opalescence ≤ Standard)" :
                                     "Result: FAIL (Sample opalescence > Standard)";
            resultText.color = pass ? new Color(0.063f, 0.725f, 0.475f) /* #10B981 */ : new Color(0.855f, 0.106f, 0.106f);
        }
    }
}
