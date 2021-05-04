using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRbox : MonoBehaviour
{
    public bool Spin = false;
    public float speed;
    public float spinSpeed = 50f;
    public float delay = 1f;
    public float Lmax = 3f;
    public float Rmax = 3f;
    private Vector3 dir = Vector3.left;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Spin) spin();
        StartCoroutine(LRDmove());
        //LRMove(speed);


    }

    public void LRMove(float speed)
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (transform.position.x <= -Lmax)
        {
            dir = Vector3.right;
        }
        else if (transform.position.x >= Rmax)
        {
            dir = Vector3.left;
        }
    }

    IEnumerator LRDmove()
    {
        yield return new WaitForSeconds(delay);
        LRMove(speed);

    }

    void spin()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }


}
