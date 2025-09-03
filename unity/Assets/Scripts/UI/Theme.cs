using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "ARLab/Theme")]
public class Theme : ScriptableObject
{
    // Colors (3-5 total)
    public Color Primary = new Color(0.058f, 0.647f, 0.910f);   // #0EA5E9 blue
    public Color NeutralBg = Color.white;                       // #FFFFFF
    public Color NeutralText = new Color(0.067f, 0.094f, 0.153f); // #111827 near-black
    public Color Muted = new Color(0.420f, 0.447f, 0.502f);     // #6B7280 gray
    public Color Accent = new Color(0.063f, 0.725f, 0.475f);    // #10B981 green
}
