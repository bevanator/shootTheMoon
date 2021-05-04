using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator[] Transitions;                              //An array of Animation Controllers for all the objects in the scene.
    //public Animator Transition;
    float animationDuration = 1f;
    GameObject Sphere;                                   //Empty Game Object to store the selected sphere.                                            
    Vector3 center = new Vector3(0, 0, 0);                      //Defining Center Vector3 for later use.

    Camera cam;
    public Ball ball;
    public float pushForce = 4f;
    bool isDragging = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 direction;
    Vector3 force;
    float distance;
    Vector3 dist;

    bool isIdle;
    public float speed = 0.4f;
    public bool levelClear;
    public bool gamePaused;

    public TimeManager timeManager;
    public GameObject ui;
    public ParticleSystem title;
    LineRenderer lr;
    void Start()
    {
        cam = Camera.main;
        ball.DeactivateRB();
        isIdle = true;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.positionCount = 2;
        levelClear = false;
        gamePaused = false;

    }
    private void Awake()
    {
        gamePaused = false;
        title.Play();
        timeManager.ResetTime();
    }



    //Do this when the mouse click on this selectable UI object is released.


    // Update is called once per frame
    void Update()
    {

        //ball.showVelocity();
        //ball.shrinkBall();
        //timeManager.turbo();
        
        if(gamePaused) timeManager.FreezeTime();
        else
        {
            if (isIdle) ball.idleMove(speed);
            if (ball.LC)
            {
                ball.SlowDown();
                loadCenter();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!this.IsPointerOverUIObject())
                {
                    isIdle = false;
                    isDragging = true;
                    OnDragStart();
                    timeManager.SlowMo();
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!this.IsPointerOverUIObject())
                {
                    isDragging = false;
                    OnDragEnd();
                    timeManager.ResetTime();
                    //timeManager.turbo();
                }

            }

            if (isDragging)
            {
                OnDrag();
            }


        }




    }

    private bool IsPointerOverUIObject()
    {
        // get current pointer position and raycast it
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // check if the target is in the UI
        foreach (RaycastResult r in results)
        {
            bool isUIClick = r.gameObject.transform.IsChildOf(this.ui.transform);
            if (isUIClick)
            {
                return true;
            }
        }
        return false;
    }
    void loadCenter()
    {
        StartCoroutine(movingToCenter());

    }
    void OnDragStart()
    {
        lr.enabled = true;
        //ball.DeactivateRB();
        //startPoint = ball.transform.position;

    }

    void OnDrag()
    {
        startPoint = ball.transform.position;
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        
        distance = Vector3.Distance(startPoint, endPoint);
        dist = startPoint - endPoint;
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;
        //Debug.DrawLine(startPoint, endPoint);

        //lr.SetPosition(0, new Vector3(ball.transform.position.x, ball.transform.position.y, 0f));
        //lr.useWorldSpace = true;
        //lr.SetPosition(1, new Vector3((ball.transform.position.x - endPoint.x), (ball.transform.position.x - endPoint.y), 0f));
        lr.SetPosition(0, startPoint);
        //lr.useWorldSpace = true;
        lr.SetPosition(1, (startPoint + dist));

    }

    void OnDragEnd()
    {
        ball.DeactivateRB();
        ball.ActivateRB();
        ball.Push(force);
        lr.enabled = false;
    }




    public void PauseGame()
    {
        gamePaused = true;
        print("here");
        
    }

    public void ResumeGame()
    {
        gamePaused = false;
        timeManager.ResetTime();
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
    public void moveToCenterx()
    {
        if (ball.transform.position.x != 0f)
        {
            Vector3 newPos = Vector3.MoveTowards(ball.transform.position, new Vector3(0, ball.transform.position.y, ball.transform.position.z), 10 * Time.deltaTime);
            ball.transform.position = newPos;
        }
        //Debug.Log("moving");

    }




    //Used for loading next level
    void loadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void loadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void loadCurrent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void loadHome()
    {
        SceneManager.LoadScene(0);

    }

    public void ResumeAdventure()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelBeaten")+1);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("LevelBeaten", 0);
    }


    //Coroutine to wait for the animation to end before changing scene
    IEnumerator LoadLevel(int levelind)                             //using levelind as a parameter to pass buildindex
    {
                                                                     //to carry the selected object to the next scene.
        yield return new WaitForSeconds(animationDuration);         //wait for the animation Duration
        SceneManager.LoadScene(levelind);                           //Loading scene.
    }

    IEnumerator movingToCenter()
    {
        moveToCenterx();
        yield return new WaitForSeconds(1f);
        print("x=0");
        ball.DeactivateRB();
        isIdle = true;
        ball.LC = false;
    }


}