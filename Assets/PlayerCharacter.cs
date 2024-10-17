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
    [HideInInspector] public string direction = "Down";
    [HideInInspector] public string action = "Idle";

    [HideInInspector] public Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed;

    public GameObject nearbyPickableItem = null;
    public bool isCarrying = false ;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
        animator.Play($"{action} {direction}");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(float x, float y)
    {
        moveDir = new Vector2(x, y);
    }


    private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Pickable")) {
        nearbyPickableItem = other.gameObject;
    }
}

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Pickable")) {
            nearbyPickableItem = null;
        }
    }
    public void PickUpItem() {
        nearbyPickableItem.transform.SetParent(transform);
        nearbyPickableItem.transform.localPosition = new Vector3(0, 1, 0);
        isCarrying = true;
        nearbyPickableItem.GetComponent<Rigidbody2D>().simulated = false;
        nearbyPickableItem.GetComponent<SpriteRenderer>().sortingOrder = 6;
    }
    public void DropItem() {
        Transform itemToDrop = transform.GetChild(0);
        itemToDrop.transform.SetParent(null); 
        itemToDrop.GetComponent<Rigidbody2D>().simulated = true; 
        itemToDrop.GetComponent<SpriteRenderer>().sortingOrder = 5; 
        isCarrying = false;   
    }

    
}
