using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Life;
    public float basketOffset;
    public float speed = 1;
    public float speedF = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < 3; i++) {
         GameObject Lives = Instantiate<GameObject> (Life);

            Vector3 newT = new Vector3(0, -3.271f, 0.05213f);
            newT.y += basketOffset * i;

            Lives.transform.position = newT;
            Lives.transform.parent = transform;
       }
    }

    // Update is called once per frame
    void Update()
    {
        float RealSpeed = speed * speedF;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-1*RealSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(RealSpeed, 0, 0);
        }
    }

}
