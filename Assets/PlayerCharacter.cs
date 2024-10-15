using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [HideInInspector] public string direction;
    [HideInInspector] public string action;

    [HideInInspector] public Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.Play($"{action} {direction}");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    public void setMoveY(float y)
    {
        moveDir = new Vector2(moveDir.x, y);
    }

    public void setMoveX(float x)
    {
        moveDir = new Vector2(x, moveDir.y);
    }

    public void setMoveDir()
    {

    }


}
