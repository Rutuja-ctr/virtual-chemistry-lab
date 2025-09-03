using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepEngine : MonoBehaviour
{
    public ExperimentDefinition definition;
    public TextMeshProUGUI stepText;
    public Button nextButton;
    public Slider progress;
    public VoiceService voice; // pluggable TTS implementation
    public TurbidityController sampleTurbidity;
    public TurbidityController standardTurbidity;
    public StirController stirController;

    private int index = -1;
    private bool waiting;

    private void Start()
    {
        NextStep();
        if (nextButton != null) nextButton.onClick.AddListener(NextStep);
    }

    public void NextStep()
    {
        if (waiting) return;
        index++;
        if (index >= definition.steps.Count)
        {
            // done -> open observation popup
            FindObjectOfType<ObservationTableUI>(true)?.Show(definition);
            return;
        }

        var step = definition.steps[index];
        var text = string.IsNullOrEmpty(step.voiceOverride) ? step.instruction : step.voiceOverride;
        stepText.text = step.instruction;
        voice?.Speak(text);

        StopAllCoroutines();
        StartCoroutine(ExecuteStep(step));
    }

    private IEnumerator ExecuteStep(Step step)
    {
        waiting = true;
        progress.value = 0f;

        switch (step.type)
        {
            case StepType.AddChemical:
                ApplyChemical(step.targetObjectName, step.amountMl);
                yield return WaitWithProgress(step.timerSeconds);
                break;
            case StepType.Stir:
                stirController?.Stir(step.targetObjectName, step.timerSeconds);
                yield return WaitWithProgress(step.timerSeconds);
                break;
            case StepType.Wait:
                yield return WaitWithProgress(step.timerSeconds);
                break;
            case StepType.CompareOpalescence:
                // do nothing here; comparison handled in ObservationTableUI
                break;
            default:
                // Info: short pause
                yield return WaitWithProgress(Mathf.Max(0.5f, step.timerSeconds));
                break;
        }

        waiting = false;
    }

    private void ApplyChemical(string target, float amountMl)
    {
        var controller = target == "Sample" ? sampleTurbidity : standardTurbidity;
        if (controller == null) return;
        // simple model: nitric acid + AgNO3 increases turbidity
        controller.Addition(amountMl);
    }

    private IEnumerator WaitWithProgress(float seconds)
    {
        if (seconds <= 0) yield break;
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            if (progress != null) progress.value = Mathf.Clamp01(t / seconds);
            yield return null;
        }
        if (progress != null) progress.value = 1f;
    }
}
