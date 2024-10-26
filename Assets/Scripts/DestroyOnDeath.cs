using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    Health _Health;
    public GameObject EffectPrefab; 
    private void Start() {
        _Health = GetComponent<Health>();

        _Health.OnDeath += DestroyWhenDeath;
    }

    private void OnDestroy() {
        _Health.OnDeath -= DestroyWhenDeath;
    }

    void DestroyWhenDeath(){
        Instantiate(EffectPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

}
