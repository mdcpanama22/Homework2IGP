using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private float speed;
    private Vector3 worldBounds;

    // Start is called before the first frame update
    void Start()
    {
        worldBounds = GM.instance.worldBounds;
        speed = Random.Range(0.02f, 0.08f);
        speed *= -1;

        GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.0f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed, 0);
        if(transform.position.y < (-1 * worldBounds.y))
        {
            GM.instance.LoseLife();
            Debug.Log("Destroy Apple");
        }
    }
}
