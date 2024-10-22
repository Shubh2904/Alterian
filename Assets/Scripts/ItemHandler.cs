using System.Collections;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private void Awake() {
        playerCharacter = GetComponent<PlayerCharacter>();
    }
    public void PickUpItem(GameObject pickableItem) {
        pickableItem.transform.SetParent(transform);
        pickableItem.transform.localPosition = new Vector3(0, 1, 0);
        playerCharacter.isCarrying = true;
        pickableItem.GetComponent<Rigidbody2D>().simulated = false;
        pickableItem.GetComponent<SpriteRenderer>().sortingOrder = 6;
    }
    public void PlaceItem(string direction) {
        if (playerCharacter.isCarrying) {
            Transform itemToDrop = FindPickableItem();
            itemToDrop.SetParent(null);
            itemToDrop.GetComponent<Rigidbody2D>().simulated = true;
            itemToDrop.GetComponent<SpriteRenderer>().sortingOrder = 5;
            Vector2 dropOffset = GetDropOffset(direction);
            itemToDrop.position = transform.position +  (Vector3)dropOffset;
            playerCharacter.isCarrying = false;
        }
    }
    public void ThrowItem(string direction) {
        if (playerCharacter.isCarrying) {
            Transform itemToThrow = FindPickableItem();
            itemToThrow.SetParent(null);
            Rigidbody2D itemRb = itemToThrow.GetComponent<Rigidbody2D>();
            itemRb.simulated = true;

            Vector2 throwDirection = GetThrowDirection(direction);
            Vector2 targetPosition = transform.position + (Vector3)throwDirection;
            StartCoroutine(SmoothThrowAndRotate(itemToThrow, targetPosition, 0.5f));
            playerCharacter.isCarrying = false;
        }
    }
     private IEnumerator SmoothThrowAndRotate(Transform item, Vector2 targetPosition, float duration) {
        Vector2 startPosition = item.position;
        float timeElapsed = 0f;
        float startRotation = item.rotation.eulerAngles.z;
        float targetRotation = startRotation - 360f;

        while (timeElapsed < duration) {
            item.SetPositionAndRotation(Vector2.Lerp(startPosition, targetPosition, timeElapsed / duration), Quaternion.Euler(0, 0, Mathf.Lerp(startRotation, targetRotation, timeElapsed / duration)));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        item.GetComponent<SpriteRenderer>().sortingOrder = 5;
        item.SetPositionAndRotation(targetPosition, Quaternion.Euler(0, 0, 0));
    }   
    private Vector2 GetDropOffset(string direction){
        Vector2 dropOffset = direction switch {
                "Up" => new Vector2(0, 0.2f),
                "Down" => new Vector2(0, -1f),
                "Left" => new Vector2(-1f, 0),
                "Right" => new Vector2(1f, 0),
                _ => Vector2.zero
            };
        return dropOffset;
    }
    private Vector2 GetThrowDirection(string direction){
         Vector2 throwDirection = direction switch {
                "Up" => new Vector2(0, 3f),
                "Down" => new Vector2(0, -3f),
                "Left" => new Vector2(-4f, 0),
                "Right" => new Vector2(4f, 0),
                _ => Vector2.zero
            };
        return throwDirection;
    }

    private Transform FindPickableItem() {
    foreach (Transform child in transform) {
        if (child.gameObject.layer == LayerMask.NameToLayer("Pickables")) {
            return child;
        }
    }
    return null;
    }

}
