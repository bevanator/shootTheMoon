using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectChecker : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator[] Transitions;                              //An array of Animation Controllers for all the objects in the scene.
    //public Animator Transition;
    public float animationDuration = 1f;
    public GameObject Sphere;                                   //Empty Game Object to store the selected sphere.
    public int objCount = 3;                                    //   
    Vector3 center = new Vector3(0, 0, 0);                      //Defining Center Vector3 for later use.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("held");

            //freezer();

            if (Physics.Raycast(ray, out hit, 100.0f))
            {

                //Debug.Log(hit.transform.gameObject);
                Sphere = hit.transform.gameObject;                  //Store the selected object
                Sphere.GetComponent<PlainOrbit>().enabled = false;  //Disable rotation and
                Sphere.GetComponent<Animator>().enabled = false;    //Fade out animation for the selected object
                sphereFadeOut();                                    //Used for fading out remaining objects. Go to definition for details

            }

        }
        else Debug.Log("not held");
        if(Sphere.transform.position != center) moveToCenter();     //Keep moving the object towards center until it reaches center = vec3(0,0,0);
        //loadNextLevel();                                            //Used for Seamless transtions. Go to definition for details


    }

    //Method for fading out other gameobjects
    void sphereFadeOut()                                             
    {
        for (int i = 0; i < 3; i++)                                 //There are multiple ways to do this and depending on the scenario
        {                                                           //Many soultions may exist
            Debug.Log("trans" + i);                                 //In this loop all triggers for the animations are set
            Transitions[i].SetTrigger("SFadeOut" + (i + 1));
        }
    }

    //Method for moving selected gameObject towards the center.
    void moveToCenter()
    {
        Vector3 newPos = Vector3.MoveTowards(Sphere.transform.position, center, 10 * Time.deltaTime);
        Sphere.transform.position = newPos;
        //Debug.Log("moving");

    }

    /*
    void freezer()
    {
        for (int i = 1; i < 4; i++)
        {
            Debug.Log("Sphere" + i);
            GameObject.Find("Sphere" + i).GetComponent<PlainOrbit>().enabled = false;     
        }  
    }
    */


    //Used for loading next level
    void loadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    //Coroutine to wait for the animation to end before changing scene
    IEnumerator LoadLevel(int levelind)                             //using levelind as a parameter to pass buildindex
    {
        DontDestroyOnLoad(Sphere.transform.gameObject);             //to carry the selected object to the next scene.
        yield return new WaitForSeconds(animationDuration);         //wait for the animation Duration
        SceneManager.LoadScene(levelind);                           //Loading scene.
    }


}
