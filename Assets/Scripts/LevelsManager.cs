using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour
{
    public List<GameObject> playerBaseList = new List<GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> spawnerList = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBaseList.Count <= 0) {
            GameOver();
        }
    }

    void GameOver() {
        Time.timeScale = 0f;
    }

    
}
