using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputData inputData;

    PlayerCharacter player;

    void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(
            Input.GetKey(inputData.right) ? 1 : Input.GetKey(inputData.left) ? -1 : 0,
            Input.GetKey(inputData.up) ? 1 : Input.GetKey(inputData.down) ? -1 : 0
        );
        
        player.SetMoveDirection(moveInput.x, moveInput.y);
        
        if (Input.GetKeyDown(inputData.interact)) {
            if (player.nearbyPickableItem != null && !player.isCarrying) {
                player.PickUpItem();
            }else if (player.isCarrying){
                player.DropItem();
            }
        }
        if (moveInput == Vector2.zero)
        {
            player.action = player.isCarrying ? "CarryIdle" : "Idle";
        }
        else
        {
            player.direction = moveInput.y > 0 ? "Up" : moveInput.y < 0 ? "Down" : moveInput.x < 0 ? "Left" : "Right";
            player.action = player.isCarrying ? "CarryWalk" : "Walk";
        }
    }
}
