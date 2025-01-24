using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ElementType ElementType;

    private TowerController currentTower;
    
    private PlayerInput playerInput;
    private Vector2 moveInput;

    public void InitializePlayer(PlayerInput input)
    {
        playerInput = input;
    }

    public void OnLook(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log($"Player {playerInput.playerIndex} {moveInput}!");
    }
    
    public void OnBasicAttack(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnBasicAttack!");
        if (value.isPressed)
        {
            BasicAttack();
        }
    }
    
    public void OnSpecialAttack(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnSpecialAttack!");
        if (value.isPressed)
        {
            SpecialAttack();
        }
    }

    public void OnSwitchToYellow(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnSwitchToYellow!");
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Yellow);
        }
    }
    
    public void OnSwitchToGreen(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnSwitchToGreen!");
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Green);
        }
    }
    
    public void OnSwitchToBlue(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnSwitchToBlue!");
        if (value.isPressed)
        {
            SwitchToColor(ElementType.Blue);
        }
    }
    
    public void OnSwitchToRed(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnSwitchToRed!");
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
        Debug.Log($"Player {playerInput.playerIndex} PreviousTower!");
        if (value.isPressed)
        {
            // SwitchToColor();
        }
    }
    
    public void OnNextTower(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnNextTower!");
        if (value.isPressed)
        {
            // SwitchToColor();
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
        // Debug.Log($"Player {playerInput.playerIndex} {moveInput}!");
        transform.Translate(movement);
    }

    public void SetCurrentTower(TowerController tower)
    {
        currentTower = tower;
    }
}