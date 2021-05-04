using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlainOrbit : MonoBehaviour
{
    // Start is called before the first frame update
 
    public GameObject target;


    public float orbitDistance = 10.0f;
    public float speed = 180.0f;
    void Start()
    {

    }

    void Orbit()
    {
        transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;   //updating object's position by normalizing distance and multiplying it with user defined radius
        transform.RotateAround(target.transform.position, Vector3.forward, speed * Time.deltaTime);                                          //used for orbiting a target game object
    }
    // Update is called once per frame

    void Update()
    {
        //Debug.Log((transform.position - target.transform.position).normalized);
        Orbit();

    }
}