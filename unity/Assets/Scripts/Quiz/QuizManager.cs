using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class QuizQuestion
{
    public string q;
    public string[] options; // 2-5 options
    public int answerIndex;
    public string feedback;
}

[Serializable]
public class QuizBank { public QuizQuestion[] questions; }

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI progressText;
    public GameObject resultsPanel;
    public TextMeshProUGUI resultsText;

    public int numQuestions = 6;

    private List<QuizQuestion> deck = new();
    private int idx = -1;
    private int score = 0;

    private void Start()
    {
        resultsPanel.SetActive(false);
        var json = Resources.Load<TextAsset>("quiz-questions");
        var bank = JsonUtility.FromJson<QuizBank>(json.text);
        var list = new List<QuizQuestion>(bank.questions);
        // shuffle
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        deck = list.GetRange(0, Mathf.Min(numQuestions, list.Count));
        Next();
    }

    private void Next()
    {
        idx++;
        if (idx >= deck.Count)
        {
            resultsPanel.SetActive(true);
            resultsText.text = $"Score: {score}/{deck.Count}";
            foreach (var b in optionButtons) b.gameObject.SetActive(false);
            return;
        }

        var q = deck[idx];
        questionText.text = q.q;
        progressText.text = $"{idx + 1}/{deck.Count}";
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < q.options.Length)
            {
                int captured = i;
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnChoose(captured));
            }
            else optionButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnChoose(int choice)
    {
        var q = deck[idx];
        bool correct = choice == q.answerIndex;
        if (correct) score++;
        // simple feedback via log; could show a toast/popup
        Debug.Log((correct ? "Correct: " : "Incorrect: ") + q.feedback);
        Next();
    }
}
