using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MenuGUI : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject endgameScreen;
    public TMP_Text waveText;

    void Start()
    {
        startScreen.SetActive(true);
        GameManager.Instance.LevelsManager.GUIMenu = this.gameObject;
    }

    void Update()
    {
        if (startScreen.activeSelf)
            { 
                var gamepad = Gamepad.current;
                if (gamepad == null)
                    return; // No gamepad connected.

                if (gamepad.rightTrigger.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }

            }

        if (endgameScreen.activeSelf | Input.GetKeyDown(KeyCode.Space))
            { 
                var gamepad = Gamepad.current;
                if (gamepad == null)
                    return; // No gamepad connected.

                if (gamepad.rightTrigger.wasPressedThisFrame)
                {
                    RestartGame();
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
    }



    public void StartGame() {
        // change to async <-
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        GameManager.Instance.LevelsManager.StartLevel();
    }

    public void RestartGame() {
        GameManager.Instance.LevelsManager.RestartLevel();
    }

}
