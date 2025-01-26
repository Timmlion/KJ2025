using System;
using TMPro;
using UnityEngine;

public class TopWave : MonoBehaviour
{
    private void OnEnable()
    {
        var topWave = PlayerPrefs.GetInt("topWave");
        GetComponent<TMP_Text>().text = $"TOP WAVE: {topWave}";
    }
}
