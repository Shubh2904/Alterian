using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    private bool canDash = true;
    private float dashCooldown = 1f;
    public GameObject background ;

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
    public IEnumerator Dash(){
        if(canDash){
            canDash = false ;
            Vector2 DashDirection = direction switch {
                    "Up" => new Vector2(0, 5f),
                    "Down" => new Vector2(0, -5f),
                    "Left" => new Vector2(-5f, 0),
                    "Right" => new Vector2(5f, 0),
                    _ => Vector2.zero
                };
            transform.position += (Vector3)DashDirection ;
            yield return new WaitForSeconds(0.03f);
            background.SetActive(true);
            yield return new WaitForSeconds(0.02f);
            background.SetActive(false);
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
        
    }

}
