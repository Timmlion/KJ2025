using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerState
{
    Idle = 0,
    MovingToTower = 1,
    ShootingSpecial = 2,
    ShootingBasic = 3,
}
public class PlayerController : MonoBehaviour
{
    public ElementType CurrentElementType = ElementType.Blue;
    public  PlayerInput PlayerInput;

    private TowerController currentTower;
    private TowerController targetTower;
    private Vector3 startPosition;
    private float elapsedTime;
    
    [SerializeField] private PlayerGFXController playerGfx;
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private float basicAttackCooldown = 0.15f;
    [SerializeField] private float specialAttackCooldown = 3f;
    private Vector2 moveInput;
    private PlayerState CurrentPlayerState = PlayerState.Idle;
    [SerializeField] private Light orbLight;
    
    [SerializeField] private SpecialAttackIndicator specialAttackIndicator;
    private float remainingBasicAttackCooldown = 0;
    private float remainingSpecialAttackCooldown = 0;
    
    [SerializeField] private Color colorYellow = Color.yellow;
    [SerializeField] private Color colorRed = Color.red;
    [SerializeField] private Color colorBlue = Color.blue;
    [SerializeField] private Color colorGreen = Color.green;
    [SerializeField] private Color colorNone = Color.white;
    
    private PlayersManager playersManager;

    public void InitializePlayer(PlayerInput input)
    {
        PlayerInput = input;
        orbLight.color = ElementColor(CurrentElementType);
        playersManager = GameManager.Instance.PlayersManager;

    }

    public void OnLook(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        currentTower?.SetDirection(moveInput);
    }

    public void OnSwitchToYellow(InputValue value)
    {
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Yellow);
        }
    }
    
    public void OnSwitchToGreen(InputValue value)
    {
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Green);
        }
    }
    
    public void OnSwitchToBlue(InputValue value)
    {
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Blue);
        }
    }
    
    public void OnSwitchToRed(InputValue value)
    {
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Red);
        }
    }

    private void SwitchToColor(ElementType elementType)
    {
        if (playersManager.IsElementTypeFree(elementType))
        {
            print("Setting color");
            orbLight.color = ElementColor(elementType);
            print(orbLight.color);
            CurrentElementType = elementType;
            currentTower.SetTowerColor(CurrentElementType);
            playerGfx.SetColor(CurrentElementType);
            specialAttackIndicator.SetColor(CurrentElementType);
        }
        else
        {
            //TODO: add vibrations
        }
    }

    private Color ElementColor(ElementType et)
    {
        switch (et)
        {
            case ElementType.Yellow:
                return colorYellow;

            case ElementType.Red:
                return colorRed;

            case ElementType.Blue:
                return colorBlue;

            case ElementType.Green:
                return colorGreen;

            default:
                Debug.LogWarning("Unknown ElementType, returning default white color.");
                return colorNone;
        }
    }
    public void OnPreviousTower(InputValue value)
    {
        if (CurrentPlayerState != PlayerState.Idle)
        {
            return;
        }
        
        if (value.isPressed)
        {
            //Set default light to old tower
            // currentTower.SetTowerColor(ElementType.None);
                
            currentTower = GameManager.Instance.TowersManager.JumpTower(false, currentTower);
            //Set new tower color
            currentTower.SetTowerColor(CurrentElementType);
            MoveToTower(currentTower);
        }
    }

    public void OnNextTower(InputValue value)
    {
        if (CurrentPlayerState != PlayerState.Idle)
        {
            return;
        }
        
        if (value.isPressed)
        {
            currentTower = GameManager.Instance.TowersManager.JumpTower(true, currentTower);
            //Set new tower color
            currentTower.SetTowerColor(CurrentElementType);
            MoveToTower(currentTower);
        }
    }
    
    private void OnBasicAttack(InputValue value)
    {
        if (value.Get<float>() > 0.6f 
            && CurrentPlayerState == PlayerState.Idle 
            && remainingBasicAttackCooldown <= 0)
        {
            currentTower.CreateBullet(CurrentElementType, false);
            currentTower.LaunchBullet();
            remainingBasicAttackCooldown = basicAttackCooldown;
            CurrentPlayerState = PlayerState.Idle; // No need to change state
        }
    }
    
    private void OnSpecialAttack(InputValue value)
    {
        if (value.Get<float>() > 0.6f 
              && CurrentPlayerState == PlayerState.Idle 
              && remainingSpecialAttackCooldown <= 0)
        {
            currentTower.CreateBullet(CurrentElementType, true);
            currentTower.LaunchBullet();
            remainingSpecialAttackCooldown = specialAttackCooldown;
            CurrentPlayerState = PlayerState.Idle; // No need to change state
        }
    }

    private void MoveToTower(TowerController tower)
    {
        CurrentPlayerState = PlayerState.MovingToTower;
        targetTower = tower;
        startPosition = transform.position;
        elapsedTime = 0f;
        
        transform.position = tower.transform.position;
    }
    
    private void Update()
    {
        if (CurrentPlayerState == PlayerState.MovingToTower)
        {
            // Gradually move towards the target
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetTower.transform.position, t);

            // Stop moving when we reach the target
            if (t >= 1.0f)
            {
                CurrentPlayerState = PlayerState.Idle;
            }
        }

        if (remainingBasicAttackCooldown > 0)
        {
            remainingBasicAttackCooldown -= Time.deltaTime;
        }        
        
        if (remainingSpecialAttackCooldown > 0)
        {
            remainingSpecialAttackCooldown -= Time.deltaTime;

            float progress = 1 - (remainingSpecialAttackCooldown / specialAttackCooldown);
            if (progress > 0.98f)
            {
                progress = 1;
                GameManager.Instance.HapticsManager.RublePlayer(0.3f, 0.3f, 0.2f, PlayerInput);
            }
            specialAttackIndicator.SetProgress(progress);
        }
    }

    public void SetCurrentTower(TowerController tower)
    {
        currentTower = tower;
    }

    public void SetElementType(ElementType elementType)
    {
        CurrentElementType = elementType;
        playerGfx.SetColor(elementType);
        specialAttackIndicator.SetColor(elementType);
    }

    public void Remove()
    {
        currentTower.Posessed = false;
        Destroy(gameObject);
    }
}