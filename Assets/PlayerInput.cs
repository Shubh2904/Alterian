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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(inputData.up))
        {
           player.setMoveY(1);
           player.direction = "Up";
        }
        else if(Input.GetKey(inputData.down))
        {
            player.setMoveY(-1);
            player.direction = "Down";
        }
        else 
            player.setMoveY(0);
            

        if(Input.GetKey(inputData.left))
        {
            player.setMoveX(-1);
            player.direction = "Left";
        }
        else if(Input.GetKey(inputData.right))
        {
            player.setMoveX(1);
            player.direction = "Right";
        }
        else 
            player.setMoveX(0);

        if(player.moveDir == Vector2.zero)
            player.action = "Idle";
        else 
            player.action = "Walk";

        
    }
}
