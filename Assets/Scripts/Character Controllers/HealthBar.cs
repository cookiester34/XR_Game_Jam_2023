using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private BaseController crab;

    [SerializeField]
    private SpriteRenderer[] heartSprites;

    [SerializeField]
    private Sprite FullHeart;

    [SerializeField]
    private Sprite EmptyHeart;

    public void LostHealth()
    {
        if (crab.health >= 0 && crab.health < heartSprites.Length)
        {
            var heartSprite = heartSprites[crab.health];
            heartSprite.sprite = EmptyHeart;
        }
    }

    public void Reset()
    {
        crab.health = 5;

        foreach (var heartSprite in heartSprites)
        {
            heartSprite.sprite = FullHeart;
        }
    }
}