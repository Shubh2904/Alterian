using System.Collections;
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
    [HideInInspector] public GameObject nearbyPickableItem = null;
    [HideInInspector] public bool isCarrying = false;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.Play($"{action} {direction}");
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(float x, float y) {
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

    public void PlaceItem() {
        if (isCarrying) {
            Transform itemToDrop = transform.GetChild(0);
            itemToDrop.SetParent(null); 
            itemToDrop.GetComponent<Rigidbody2D>().simulated = true;
            itemToDrop.GetComponent<SpriteRenderer>().sortingOrder = 5;

            Vector2 dropOffset = direction switch {
                "Up" => new Vector2(0, 1f),
                "Down" => new Vector2(0, -1f),
                "Left" => new Vector2(-1f, 0),
                "Right" => new Vector2(1f, 0),
                _ => Vector2.zero
            };

            itemToDrop.position = rb.position + dropOffset;
            isCarrying = false;
        }
    }

    public void ThrowItem() {
        if (isCarrying) {
            Transform itemToThrow = transform.GetChild(0);
            itemToThrow.SetParent(null); 
            Rigidbody2D itemRb = itemToThrow.GetComponent<Rigidbody2D>();
            itemRb.simulated = true;

            Vector2 throwDirection = direction switch {
                "Up" => new Vector2(0, 3f),
                "Down" => new Vector2(0, -3f),
                "Left" => new Vector2(-3f, 0),
                "Right" => new Vector2(3f, 0),
                _ => Vector2.zero
            };

            Vector2 targetPosition = rb.position + throwDirection;
            StartCoroutine(SmoothThrowAndRotate(itemToThrow, targetPosition, 0.5f));
            itemToThrow.GetComponent<SpriteRenderer>().sortingOrder = 5;
            isCarrying = false;
        }
    }

    private IEnumerator SmoothThrowAndRotate(Transform item, Vector2 targetPosition, float duration) {
        Vector2 startPosition = item.position;
        float timeElapsed = 0f;
        float startRotation = item.rotation.eulerAngles.z;
        float targetRotation = startRotation - 360f;

        while (timeElapsed < duration) {
            item.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed / duration);
            item.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startRotation, targetRotation, timeElapsed / duration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        item.position = targetPosition;
        item.rotation = Quaternion.Euler(0, 0, 0);
    }
}
