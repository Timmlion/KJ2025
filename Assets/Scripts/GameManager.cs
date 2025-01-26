using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public WavesManager WavesManager;
    public LevelsManager LevelsManager;
    public TowersManager TowersManager;
    public PlayersManager PlayersManager;
    public HapticsManager HapticsManager;

    private void Awake()
    {
        Instance = this;
        
        InputSystem.settings.supportedDevices = new[] { "Gamepad" };
    }
    
    
}
