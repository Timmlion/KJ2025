using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class HapticsManager : MonoBehaviour
{
    public bool rumbleAllInProgress = false;
    
    public void RumbleAll(float lowFrequency, float highfrequency, float duration)
    {
        rumbleAllInProgress = true;
        ReadOnlyArray<Gamepad> gamepads = Gamepad.all;
        foreach (Gamepad gamepad in gamepads)
        {
            gamepad.SetMotorSpeeds(lowFrequency, highfrequency);
            StartCoroutine(StopRumble(duration, gamepad, true));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad gamepad, bool rumbleAll = false)
    {
        if (rumbleAllInProgress != rumbleAll) yield return null;
        if (rumbleAllInProgress) rumbleAllInProgress = false;
        
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gamepad.SetMotorSpeeds(0,0);
    }

    public void RublePlayer(float lowFrequency, float highfrequency, float duration, PlayerInput playerInput )
    {
        if (rumbleAllInProgress) return;
        
        Gamepad gamepad = playerInput.devices[0] as Gamepad;
        gamepad.SetMotorSpeeds(1,1);
        StartCoroutine(StopRumble(duration, gamepad));
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8)) 
        {
            RumbleAll(1f,1f, 1f );
        }
    }
}
