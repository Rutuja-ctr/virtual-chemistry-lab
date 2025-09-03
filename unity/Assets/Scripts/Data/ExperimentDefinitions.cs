using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExperimentDefinition", menuName = "ARLab/Experiment Definition")]
public class ExperimentDefinition : ScriptableObject
{
    public string experimentKey;
    [TextArea] public string title;
    [TextArea] public string aim;
    [TextArea] public string principle;
    [TextArea] public string requirements;
    [TextArea] public string chemicals;
    public List<Step> steps = new List<Step>();
    public string observationRuleSummary; // e.g., "Sample opalescence ≤ Standard → Pass"
}

[Serializable]
public class Step
{
    public string id;
    [TextArea] public string instruction;
    public StepType type;
    public string targetObjectName; // "Sample", "Standard", "Beaker_KCl", etc.
    public float amountMl; // optional for chemical additions
    public float timerSeconds; // optional delay
    public string voiceOverride; // if you want custom narration text
}

public enum StepType
{
    Info,
    AddChemical,
    Stir,
    Wait,
    CompareOpalescence
}
