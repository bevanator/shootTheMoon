using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{

    public Ball ball;
    public float power = 50f;
    Vector3 dir;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = transform.rotation.eulerAngles.z;
        
        float x = Mathf.Cos(angle * Mathf.PI / 180);
        float y = Mathf.Sin(angle * Mathf.PI / 180);
        dir = new Vector3(x, y, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.rotation.eulerAngles.z);
    }
    private void OnTriggerEnter(Collider boostCol)
    {
        if (boostCol.gameObject.tag == "Player")
        {
            print("boosted");
            ball.boost(power, dir);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Player") print("boosted");
    }
}
