using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Ball ball;
    public Vector3 offset;
    float pos;


    // Start is called before the first frame update
    private void Start()
    {

    }
    void Update()
    {
        pos = ball.transform.position.y;

    }

    // Update is called once per frame

    void LateUpdate()
    {
        if((pos + 5.37)>= transform.position.y) camUpdate();
    }
    void FixedUpdate()
    {

    }
    void camUpdate()
    {
        transform.position = new Vector3(0f, ball.transform.position.y, -5f) + offset;
    }
}
