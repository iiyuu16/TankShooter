using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemySprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        ChangeSprites();
    }

    void ChangeSprites()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
