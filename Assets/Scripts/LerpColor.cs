using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LerpColor : MonoBehaviour
{
    [SerializeField] Color colorA;
    [SerializeField] Color colorB;

    [SerializeField] float lerpDuration;
    SpriteRenderer sRend;

    float S,V,hueA, hueB;

    void Start()
    {
        sRend = GetComponent<SpriteRenderer>();

        Color.RGBToHSV(colorA, out hueA, out S, out V);
        Color.RGBToHSV(colorA, out hueA, out S, out V);

        LeanTween.value(this.gameObject, updateColor ,hueA, hueB, lerpDuration).setRepeat(-1).setLoopPingPong();
    }
    // Update is called once per frame
    void updateColor(float hue)
    {
        sRend.color = Color.HSVToRGB(hue,S,V);
    }
}
