using System.Collections;
using UnityEngine;

public class StirController : MonoBehaviour
{
    public Transform sampleLiquid;
    public Transform standardLiquid;

    public void Stir(string target, float seconds)
    {
        var tf = target == "Standard" ? standardLiquid : sampleLiquid;
        if (tf != null) StartCoroutine(StirAnim(tf, seconds));
    }

    private IEnumerator StirAnim(Transform t, float seconds)
    {
        float elapsed = 0f;
        while (elapsed < seconds)
        {
            elapsed += Time.deltaTime;
            t.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 10f) * 3f);
            yield return null;
        }
        t.localRotation = Quaternion.identity;
    }
}
