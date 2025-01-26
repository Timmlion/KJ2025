using UnityEngine;
using UnityEngine.InputSystem;

public class MenuGUI : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject endgameScreen;

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

                if (gamepad.rightTrigger.wasPressedThisFrame)
                {
                    StartGame();
                }

            }

        if (endgameScreen.activeSelf)
            { 
                var gamepad = Gamepad.current;
                if (gamepad == null)
                    return; // No gamepad connected.

                if (gamepad.rightTrigger.wasPressedThisFrame)
                {
                    RestartGame();
                }

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
