using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectAfterTimeFrame : MonoBehaviour
{
    private void Start() {
        Destroy(gameObject,1f);
    }
}
