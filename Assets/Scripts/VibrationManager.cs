using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private static AndroidJavaClass unityPlayer;
    private static AndroidJavaObject vibrator;
    private static AndroidJavaObject currentActivity;
    private static AndroidJavaClass vibrationEffectClass;
    private static int defaultAmplitude = 255;

    private void OnEnable() {
        using(unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
            if (currentActivity != null) {
                vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
            }
        }
    }

    public static void Vibrate(long milliseconds) {
        using (AndroidJavaObject vibrationEffect =
            vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, defaultAmplitude)) {
            vibrator.Call("vibrate", vibrationEffect);
        }
    }
}
