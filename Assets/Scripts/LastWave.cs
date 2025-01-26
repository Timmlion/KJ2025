using System;
using TMPro;
using UnityEngine;

public class LastWave : MonoBehaviour
{
    private void OnEnable()
    {
        var lastWave = PlayerPrefs.GetInt("lastWave");
        GetComponent<TMP_Text>().text = $"LAST WAVE: {lastWave}";
    }
}
