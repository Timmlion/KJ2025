using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 moveInput;

    public void InitializePlayer(PlayerInput input)
    {
        playerInput = input;
        // playerInput.defaultActionMap.
    }

    public void OnLook(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    public void OnAttack(InputValue value)
    {
        Debug.Log($"Player {playerInput.playerIndex} OnAttack!");
        if (value.isPressed)
        {
            Shoot();
        }
    }
    

    public void Shoot()
    {
        Debug.Log($"Player {playerInput.playerIndex} shoot!");
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * 5f;
        // Debug.Log($"Player {playerInput.playerIndex} {moveInput}!");
        transform.Translate(movement);
    }
}