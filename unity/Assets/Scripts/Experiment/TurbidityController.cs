using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TurbidityController : MonoBehaviour
{
    [Range(0,1)] public float turbidity;
    public float turbidityPerMl = 0.02f; // tune per experiment
    private Renderer rend;
    private static readonly int TurbidityId = Shader.PropertyToID("_Turbidity");

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        UpdateMaterial();
    }

    public void Addition(float ml)
    {
        turbidity = Mathf.Clamp01(turbidity + ml * turbidityPerMl);
        UpdateMaterial();
    }

    public void Set(float value)
    {
        turbidity = Mathf.Clamp01(value);
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (rend != null && rend.material.HasProperty(TurbidityId))
        {
            rend.material.SetFloat(TurbidityId, turbidity);
        }
    }
}
