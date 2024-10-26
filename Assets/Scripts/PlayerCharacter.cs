using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [HideInInspector] public Vector2 moveDir = Vector2.zero;
    [HideInInspector] public string direction = "Down";
    [HideInInspector] public string action = "Idle";

    [Header("Dash")]
    private bool canDash = true;
    private float dashCooldown = 1f;
    public GameObject background;

    [Header("Player Status")]
    public static PlayerCharacter singleton;
    [HideInInspector] public bool isCarrying = false;
    [HideInInspector] public bool isAttacking = false;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator body;
    private ItemHandler itemHandler;
    private PlayerAttack playerAttack;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        itemHandler = GetComponent<ItemHandler>();
        singleton = this;
        playerAttack = GetComponent<PlayerAttack>();
        body = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update() {
        if (!isAttacking) body.Play($"{action} {direction}");
    }

    void FixedUpdate() {
        if (!isAttacking)
            rb.MovePosition(rb.position + moveDir.normalized * speed * Time.fixedDeltaTime);
    }

    public void Attack() {
        playerAttack.Attack(direction);
    }

    public void SetMoveDirection(float x, float y) {
        moveDir = new Vector2(x, y);
    }

    public void PickUpItem(GameObject pickableItem) {
        itemHandler.PickUpItem(pickableItem);
    }

    public void PlaceItem() {
        itemHandler.PlaceItem(direction);
    }

    public void ThrowItem() {
       itemHandler.ThrowItem(direction);
    }

    public IEnumerator Dash() {
        if (!canDash) yield break;
        
        canDash = false;
        Vector2 dashDirection = direction switch {
            "Up" => new Vector2(0, 5f),
            "Down" => new Vector2(0, -5f),
            "Left" => new Vector2(-5f, 0),
            "Right" => new Vector2(5f, 0),
            _ => Vector2.zero
        };
        
        transform.position += (Vector3)dashDirection;
        yield return new WaitForSeconds(0.03f);
        
        background.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        
        background.SetActive(false);
        yield return new WaitForSeconds(dashCooldown);
        
        canDash = true;
    }
}
