using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ElementType CurrentElementType = ElementType.Blue;

    private TowerController currentTower;
    
    private PlayerInput playerInput;
    [SerializeField] private PlayerGFXController playerGfx;
    private Vector2 moveInput;

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
        playerGfx.SetColor(CurrentElementType);
    }
    
    public void OnPreviousTower(InputValue value)
    {
        if (value.isPressed)
        {
            currentTower = GameManager.Instance.TowersManager.JumpTower(false, currentTower);
            MoveToTower(currentTower);
        }
    }

    public void OnNextTower(InputValue value)
    {
        if (value.isPressed)
        {
            currentTower = GameManager.Instance.TowersManager.JumpTower(true, currentTower);
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
        }
        else if (value.Get<float>() < 0.3f)
        {
            currentTower.LaunchBullet();
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
        transform.position = tower.transform.position;
    }
    
    public void SetCurrentTower(TowerController tower)
    {
        currentTower = tower;
    }

    public void SetElementType(ElementType elementType)
    {
        CurrentElementType = elementType;
        playerGfx.SetColor(elementType);
    }
}