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
    
    private PlayerInput playerInput;
    [SerializeField] private PlayerGFXController playerGfx;
    private Vector2 moveInput;
    private PlayerState CurrentPlayerState = PlayerState.Idle;

    public void InitializePlayer(PlayerInput input)
    {
        playerInput = input;
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
        CurrentElementType = elementType;
        currentTower.SetTowerColor(CurrentElementType);
        playerGfx.SetColor(CurrentElementType);
    }
    
    public void OnPreviousTower(InputValue value)
    {
        if (CurrentPlayerState == PlayerState.Shooting)
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
        if (CurrentPlayerState == PlayerState.Shooting)
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

        if (value.Get<float>() > 0.6f)
        {
            currentTower.CreateBullet(CurrentElementType, AttackType.Basic);
            CurrentPlayerState = PlayerState.Shooting;
        }
        else if (value.Get<float>() < 0.3f)
        {
            currentTower.LaunchBullet();
            CurrentPlayerState = PlayerState.Idle;
        }
    }
    
    private void OnSpecialAttack(InputValue value)
    {
        OnBasicAttack(value); // TODO: this
    }

    private void Update()
    {
        // Debug only
        //Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * (Time.deltaTime * 5f);
        //transform.Translate(movement);
    }

    private void MoveToTower(TowerController tower)
    {
        //TODO: Implement flowing to tower
        transform.position = tower.transform.position;
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