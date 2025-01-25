using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public List<GameObject> playerBaseList = new List<GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> spawnerList = new List<GameObject>();

    public GameObject GUIMenu;

    void Start()
    {
        Time.timeScale = 0f;
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
        Time.timeScale = 0f;
        GUIMenu.GetComponent<MenuGUI>().endgameScreen.SetActive(true);
    }

    
}
