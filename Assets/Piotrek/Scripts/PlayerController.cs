using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ElementType ElementType;

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
    }
    
    public void OnBasicAttack(InputValue value)
    {
        if (value.isPressed)
        {
            BasicAttack();
        }
    }
    
    public void OnSpecialAttack(InputValue value)
    {
        if (value.isPressed)
        {
            SpecialAttack();
        }
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
        ElementType = elementType;
    }
    
    public void OnPreviousTower(InputValue value)
    {
        if (value.isPressed)
        {
            currentTower = GameManager.Instance.TowersManager.JumpTower(false, currentTower);
            transform.position = currentTower.transform.position;
        }
    }
    
    public void OnNextTower(InputValue value)
    {
        if (value.isPressed)
        {
            currentTower = GameManager.Instance.TowersManager.JumpTower(true, currentTower);
            transform.position = currentTower.transform.position;
        }
    }
    
    private void BasicAttack()
    {
        Debug.Log($"Player {playerInput.playerIndex} shoot!");
    }
    
    private void SpecialAttack()
    {
        Debug.Log($"Player {playerInput.playerIndex} KAMEHAMEHA!");
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * 5f;
        transform.Translate(movement);
    }

    public void SetCurrentTower(TowerController tower)
    {
        currentTower = tower;
    }

    public void SetElementType(ElementType elementType)
    {
        ElementType = elementType;
        playerGfx.SetColor(elementType);
    }
}