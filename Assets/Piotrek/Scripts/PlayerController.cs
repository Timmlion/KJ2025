using System;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerState
{
    Idle = 0,
    MovingToTower = 1,
    Shooting = 2,
}
public class PlayerController : MonoBehaviour
{
    public ElementType CurrentElementType = ElementType.Blue;

    private TowerController currentTower;
    private TowerController targetTower;
    private Vector3 startPosition;
    private float elapsedTime;
    
    private PlayerInput playerInput;
    [SerializeField] private PlayerGFXController playerGfx;
    [SerializeField] private float moveDuration = 0.1f;
    private Vector2 moveInput;
    private PlayerState CurrentPlayerState = PlayerState.Idle;
    [SerializeField] private Light orbLight;

    public void InitializePlayer(PlayerInput input)
    {
        playerInput = input;
        orbLight.color = ElementColor(CurrentElementType);
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
        print("Setting color");
        orbLight.color = ElementColor(elementType);
        print(orbLight.color);
        CurrentElementType = elementType;
        currentTower.SetTowerColor(CurrentElementType);
        playerGfx.SetColor(CurrentElementType);
    }

    private Color ElementColor(ElementType et)
    {
        switch (et)
        {
            case ElementType.Yellow:
                return Color.yellow;

            case ElementType.Red:
                return Color.red;

            case ElementType.Blue:
                return Color.blue;

            case ElementType.Green:
                return Color.green;

            default:
                Debug.LogWarning("Unknown ElementType, returning default white color.");
                return Color.white;
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
            currentTower.SetTowerColor(ElementType.None);
                
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
            //Set default light to old tower
            currentTower.SetTowerColor(ElementType.None);
            
            currentTower = GameManager.Instance.TowersManager.JumpTower(true, currentTower);
            //Set new tower color
            currentTower.SetTowerColor(CurrentElementType);
            MoveToTower(currentTower);
        }
    }
    
    private void OnBasicAttack(InputValue value)
    {
        if (currentTower == null)
        {
            // TODO: Consider how to handle this. Is player flying to next tower?
        }


        if (value.Get<float>() > 0.6f && CurrentPlayerState == PlayerState.Idle)
        {
            currentTower.CreateBullet(CurrentElementType, AttackType.Basic);
            CurrentPlayerState = PlayerState.Shooting;
        }
        else if (value.Get<float>() < 0.3f && CurrentPlayerState == PlayerState.Shooting)
        {
            currentTower.LaunchBullet();
            CurrentPlayerState = PlayerState.Idle;
        }
        
    }
    
    private void OnSpecialAttack(InputValue value)
    {
        OnBasicAttack(value); // TODO: this
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
    }

    public void SetCurrentTower(TowerController tower)
    {
        currentTower = tower;
        print("Setting tower color");
        currentTower.SetTowerColor(CurrentElementType);
    }

    public void SetElementType(ElementType elementType)
    {
        CurrentElementType = elementType;
        currentTower.SetTowerColor(CurrentElementType);
        playerGfx.SetColor(elementType);
    }
}