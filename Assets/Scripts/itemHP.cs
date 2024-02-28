using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemHP : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x - speed * Time.deltaTime, position.y);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.x < min.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DelayDestroy(3f);
            Destroy(gameObject);
        }
    }

    private IEnumerator DelayDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
