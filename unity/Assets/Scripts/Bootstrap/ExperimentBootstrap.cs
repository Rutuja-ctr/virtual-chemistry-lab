using System.Collections.Generic;
using UnityEngine;

public class ExperimentBootstrap : MonoBehaviour
{
    public ExperimentDefinition definitionAsset; // optional ScriptableObject set via Inspector

    public ExperimentDefinition GetOrCreate()
    {
        if (definitionAsset != null) return definitionAsset;

        var def = ScriptableObject.CreateInstance<ExperimentDefinition>();
        def.experimentKey = "Experiment1";
        def.title = "Limit Test for Chloride";
        def.aim = "To perform the Limit Test for Chloride.";
        def.principle = "Chlorides react with silver nitrate in presence of dilute nitric acid to form silver chloride precipitate observed as opalescence/turbidity.";
        def.requirements = "- Nessler cylinders\n- Glass rod\n- Test tube stand";
        def.chemicals = "- Dilute Nitric Acid (10 ml)\n- 0.1M Silver Nitrate (1 ml)\n- Sodium Chloride Standard Solution (10 ml)\n- Water (QS)";
        def.observationRuleSummary = "Sample opalescence ≤ Standard → Pass; else Fail";

        def.steps = new List<Step>
        {
            new Step{ id="s1", instruction="Dissolve the compound in water and transfer to Sample Nessler cylinder.", type=StepType.Info, targetObjectName="Sample", timerSeconds=1 },
            new Step{ id="s2", instruction="Take 1 ml Sodium chloride standard solution in Standard Nessler cylinder.", type=StepType.Info, targetObjectName="Standard", timerSeconds=1 },
            new Step{ id="s3", instruction="Add 10 ml dilute nitric acid to both Sample and Standard.", type=StepType.AddChemical, targetObjectName="Sample", amountMl=10, timerSeconds=1 },
            new Step{ id="s3b", instruction="Add 10 ml dilute nitric acid to Standard.", type=StepType.AddChemical, targetObjectName="Standard", amountMl=10, timerSeconds=1 },
            new Step{ id="s4", instruction="Dilute both to 50 ml with water.", type=StepType.Info, timerSeconds=1 },
            new Step{ id="s5", instruction="Add 1 ml 0.1M silver nitrate to Sample.", type=StepType.AddChemical, targetObjectName="Sample", amountMl=1, timerSeconds=0.5f },
            new Step{ id="s5b", instruction="Add 1 ml 0.1M silver nitrate to Standard.", type=StepType.AddChemical, targetObjectName="Standard", amountMl=1, timerSeconds=0.5f },
            new Step{ id="s6", instruction="Stir both and keep for 5 minutes.", type=StepType.Stir, targetObjectName="Sample", timerSeconds=5 },
            new Step{ id="s7", instruction="Observe turbidity and compare Sample vs Standard.", type=StepType.CompareOpalescence, timerSeconds=0.5f }
        };

        return def;
    }
}
