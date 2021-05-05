using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public SphereCollider col;
    public Vector3 pos { get { return transform.position; } }

    private Vector3 dir = Vector3.left;
    public GameObject particleSystemPrefab;
    public GameObject GOParticlePrefab;
    public bool LC = false;
    public bool killed = false;
    public int levelBeaten;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    public void Push(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void ActivateRB()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRB()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
    public void SlowDown()
    {
        rb.velocity = rb.velocity * 0.9f;
    }

    public void idleMove(float speed)
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (transform.position.x <= -1)
        {
            dir = Vector3.right;
        }
        else if (transform.position.x >= 1)
        {
            dir = Vector3.left;
        }
    }

    public void showVelocity()
    {
        Debug.Log(rb.velocity.magnitude);
    }



    public void shrinkBall()
    {
        float speed = rb.velocity.magnitude;
        float t = speed / 4000f;
        //transform.localScale = new Vector3((0.75f - (speed / 400f)), 0.75f, 0.75f);
        Debug.Log("shrinking");
        if (speed >= 20)
        {
            if (transform.localScale.x > 0.6f) transform.localScale -= new Vector3(t, 0f, 0f);
        }
        else if (speed < 20)
        {
            if(transform.localScale.x < 0.75f) transform.localScale += new Vector3(t, 0f, 0f);
        }

    }



    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            //Instantiate your particle system here.
            if (collision.gameObject.tag == "Kill")
            {
                GameObject GOImpact = (GameObject)Instantiate(GOParticlePrefab, contact.point, Quaternion.identity);
                Destroy(gameObject);
                Destroy(GOImpact, 10f);
                killed = true;

            }
            else
            {
                GameObject Impact = (GameObject)Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
                Destroy(Impact, 0.8f);
            }
        }
    }


    public void boost(float power, Vector3 dir)
    {
        print("boosted");
        rb.velocity = Vector3.zero;
        rb.AddForce(power * dir, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider boostCol)
    {
        if (boostCol.gameObject.tag == "levelReach")
        {
            //print("Next level");
            LC = true;
            levelBeaten = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("LevelBeaten", levelBeaten);

        }

    }

}
