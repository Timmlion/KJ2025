using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public List<GameObject> playerBaseList = new List<GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> spawnerList = new List<GameObject>();

    public GameObject GUIMenu;
    private MenuGUI MenuGUI;

    void Start()
    {
        Time.timeScale = 0f;
        MenuGUI = GUIMenu.GetComponent<MenuGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerBaseList.Count <= 0) {
            GameOver();
        }
    }
    public void StartLevel() {
        Time.timeScale = 1f;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver() {
        Invoke(nameof(ShowEndScreen), 4);
    }

    public void ShowEndScreen(){
        Time.timeScale = 0f;
        MenuGUI.endgameScreen.SetActive(true);
    }

    public void ShowWaveLabel(int waveNumber)
    {
        MenuGUI.ShowWaveLabel(waveNumber);
    }
}
