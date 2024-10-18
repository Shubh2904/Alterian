using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputData inputData;
    [SerializeField] private float PickUpRange = 1.5f;

    [SerializeField] private LayerMask pickableLayer;
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
             HandleInteraction();        
        }
        if(Input.GetKeyDown(inputData.fire1) && player.isCarrying){
            player.ThrowItem();
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

    private void HandleInteraction() {
        if (!player.isCarrying) {
            Collider2D closestPickable = null;
            float closestDistance = Mathf.Infinity;

        foreach (Collider2D pickable in Physics2D.OverlapCircleAll(transform.position, PickUpRange, pickableLayer)) {
            float distance = Vector2.Distance(transform.position, pickable.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestPickable = pickable;
            }
        }
        if (closestPickable != null) {
            player.PickUpItem(closestPickable.gameObject);
        }
        } else {
            player.PlaceItem();
        }
    }


}
