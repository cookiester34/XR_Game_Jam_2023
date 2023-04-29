using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private BaseController crab;

    private int health;

    [SerializeField]
    private GameObject[] heartSprites;

    [SerializeField]
    private Sprite FullHeart;

    [SerializeField]
    private Sprite EmptyHeart;

    private void Start()
    {
        health = crab.health;
    }

    private void Update()
    {
        if (crab.health == health)
        {
            return;
        }
        if (crab.health < health)
        {
            if (health - 1 >= 0 && health - 1 < heartSprites.Length)
            {
                var sprite = heartSprites[health - 1].GetComponent<SpriteRenderer>();
                sprite.sprite = EmptyHeart;
            }

            health -= 1;
        }
        else if (health < 5)
        {
            if (health >= 0 && health < heartSprites.Length)
            {
                var sprite = heartSprites[health].GetComponent<SpriteRenderer>();
                sprite.sprite = FullHeart;
            }

            health += 1;
        }
        else
        {
            crab.health = 5;
        }
    }
}