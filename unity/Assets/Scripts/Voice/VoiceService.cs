using UnityEngine;

public abstract class VoiceService : MonoBehaviour
{
    public abstract void Speak(string text);
}

#if UNITY_ANDROID && !UNITY_EDITOR
public class AndroidTTSService : VoiceService
{
    private AndroidJavaObject ttsObject;
    private AndroidJavaObject activity;

    void Start()
    {
        using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        ttsObject = new AndroidJavaObject("android.speech.tts.TextToSpeech", activity, null);
    }

    public override void Speak(string text)
    {
        if (ttsObject == null) return;
        ttsObject.Call<int>("speak", text, 0, null, null);
    }
}
#else
public class EditorLogVoiceService : VoiceService
{
    public override void Speak(string text)
    {
        Debug.Log("[Voice] " + text);
    }
}
#endif
