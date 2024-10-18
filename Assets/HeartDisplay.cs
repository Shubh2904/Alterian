using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{

    [Tooltip("Object holding all the heart sprites.")]
    [SerializeField] Transform HeartHolder;
    List<Image> hearts = new List<Image>();
    [SerializeField] List<Sprite> heartLevels = new List<Sprite>(); 

    const int hpPerHeart = 4;

    //delete
    int testHP = 8;
    int maxHP = 10;

    int TestHP 
    {
        get {return testHP;}
        set { testHP = value; Debug.Log(testHP); }
    }

    void Awake()
    {
        getHeartSprites();

        if(heartLevels.Count < hpPerHeart + 1)
            Debug.LogError("Number of heart levels is not sufficient to display HP.");

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts(TestHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Equals))
        UpdateHearts(++TestHP, maxHP);
      else if(Input.GetKeyDown(KeyCode.Minus))
        UpdateHearts(--TestHP, maxHP);
    }

    void getHeartSprites()
    {
        var heartCount = HeartHolder.childCount;

        for(int i=0; i<heartCount; i++)
        {
            var heart = HeartHolder.GetChild(i).GetComponent<Image>();

            if(heart == null)
                Debug.LogError("A child of heart holder doesnt have a sprite renderer.");
        

            hearts.Add(heart);

            heart.sprite = heartLevels[0];

            // Debug.Log($"{i} -> {hearts[i].name}");
        }

    }

    public void UpdateHearts(int hp, int maxHP)
    {   
        if(hp < 1)
        {
            foreach (var heart in hearts)
                heart.sprite = heartLevels[0];

            return;
        }

        int totalHearts =  maxHP / hpPerHeart + Mathf.Clamp(maxHP % hpPerHeart, 0, 1);   
        
        //reset all hearts
        for (int i = 0; i < hearts.Count; i++)
        {
            if(i<totalHearts)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = heartLevels[0];
            }
            else 
                hearts[i].enabled = false;
        }
            

        int requiredHearts = hp / hpPerHeart + Mathf.Clamp(hp % hpPerHeart, 0, 1);

        for (int i = 0; i < requiredHearts; i++)
        {
            hearts[i].enabled = true;
            hearts[i].sprite = heartLevels[heartLevels.Count - 1];
        }
            
        var lastHeart = hearts[requiredHearts-1];
        
        if(hp % hpPerHeart != 0)
            lastHeart.sprite = heartLevels[hp % hpPerHeart];

        LeanTween.cancel(lastHeart.gameObject);
        lastHeart.transform.localScale = Vector3.one;
        LeanTween.scale(lastHeart.gameObject, Vector3.one * 1.25f, 0.15f).setRepeat(2).setLoopPingPong();
    }
}
