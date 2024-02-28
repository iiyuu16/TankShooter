using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class RGB_BG : MonoBehaviour
{
    public float speed = 1f;
    private Image image;
    private Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        targetColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        image.color = Color.Lerp(image.color, targetColor, Time.deltaTime * speed);
    }

    public void SetTargetColor(Color color)
    {
        targetColor = color;
    }
}
