using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBGQuad : MonoBehaviour
{

    public float scrollSpeed;
    float offset;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(0, offset);
    }
}
