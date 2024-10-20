using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public static PlayerCharacter singleton;

     public string direction = "Down";
    [HideInInspector] public string action = "Idle";
    [HideInInspector] public Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed;
    [HideInInspector] public bool isCarrying = false;

    private ItemHandler itemHandler ;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        itemHandler = GetComponent<ItemHandler>();

        singleton = this;
    }

    void Update() {
        animator.Play($"{action} {direction}");
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir.normalized * speed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(float x, float y) {
        moveDir = new Vector2(x, y);
    }


    public void PickUpItem(GameObject PickableItem) {
        itemHandler.PickUpItem(PickableItem);
    }

    public void PlaceItem() {
        itemHandler.PlaceItem(direction);
    }

    public void ThrowItem() {
       itemHandler.ThrowItem(direction);
    }

}
