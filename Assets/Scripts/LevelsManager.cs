using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public List<GameObject> playerBaseList = new List<GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> spawnerList = new List<GameObject>();

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

    void StartGame() {
        Time.timeScale = 1f;
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver() {
        Time.timeScale = 0f;
    }

    
}
