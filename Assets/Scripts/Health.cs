using System;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private int MaxHealth;
    int  CurrentHp;
    public event Action OnHit;
    public event Action OnDeath;
    private void Start() {
        CurrentHp = MaxHealth ;
    }

    public void TakeDamage(int Mount){
        CurrentHp -= Mount;
        if(CurrentHp > 0){
            OnHit?.Invoke();
        }else{
            OnDeath?.Invoke();
        }
    }
}