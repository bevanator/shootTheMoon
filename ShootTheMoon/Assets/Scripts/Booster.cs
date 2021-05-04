using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider boostCol)
    {
        //if(boostCol.gameObject.tag == "Player") print("boosted");
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Player") print("boosted");
    }
}
