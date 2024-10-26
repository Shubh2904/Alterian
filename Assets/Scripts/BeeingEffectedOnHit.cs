using System.Collections;
using UnityEngine;

public class BeeingEffectedOnHit : MonoBehaviour
{
    Health _Health;
    SpriteRenderer sprite;

    private void Start() {
        _Health = GetComponent<Health>();
        sprite = GetComponent<SpriteRenderer>();
        _Health.OnHit += ApplyEffect;
    }

    private void OnDestroy() {
        _Health.OnHit -= ApplyEffect;
    }

    void ApplyEffect() {
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect() {
         yield return new WaitForSeconds(0.3f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sprite.enabled = true;
    }
}
