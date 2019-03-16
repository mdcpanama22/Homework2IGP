using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject Apple;
    public GameObject Apples;
    public float speed = 0.2f;
    public float bTolerance = 0.3f; //Bound tolerance
    public float tTolerance; //Time Tolerance
    private float aTolerance; //Apple Tolerance

    private float aInitialT;

    private float initialT;

    private bool LostLife;

    // Start is called before the first frame update
    void Start()
    {
        initialT = Time.time;
        aInitialT = Time.time;
        tTolerance = Random.Range(0.0f, 5.0f);
        aTolerance = Random.Range(0.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        LostLife = GM.instance.lostLife;

        if (!LostLife)
        {
            float finalT = Time.time;
            Vector3 worldBounds = GM.instance.worldBounds;
            if ((transform.position.x < ((-1 * worldBounds.x) + bTolerance)) ||
                (transform.position.x > (worldBounds.x - bTolerance)))
            {
                speed *= -1;


            }
            else if ((finalT - initialT) > tTolerance)
            {
                speed *= -1;
                initialT = finalT;
                tTolerance = Random.Range(0.0f, 5.0f);
            }

            if ((finalT - aInitialT) > aTolerance)
            {
                aInitialT = finalT;
                aTolerance = Random.Range(0.0f, 1.0f);
                GameObject tApple = Instantiate<GameObject>(Apple);
                tApple.transform.position = transform.position;
                tApple.transform.parent = Apples.transform;
            }
            Debug.Log(worldBounds.y);

            transform.Translate(speed, 0, 0);
        }
    }

}
