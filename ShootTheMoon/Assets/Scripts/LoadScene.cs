using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    private bool lastScene = false;
    public Animator Transition;
    public float transitionTime = 1f;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2) lastScene = true;                    //checking whether it's the last scene or not

    }

    // Update is called once per frame
    void Update()
    {


    }

    //alternate method to load next scene
    public void LoadNext()
    {
        if (lastScene) SceneManager.LoadScene(0);                                               
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);              
        Debug.Log("clicked");
    }

    //reusing old methods
    public void loadNextLevel()
    {
        if (lastScene) StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 2)); //Checking if last scene, go to the first scene
        else StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));           //otherwise take the current build index and add 1 to load next one.

    }

    IEnumerator LoadLevel(int levelind)
    {
        Transition.SetTrigger("LoadNext");                                                      //wait for animation to finish
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelind);
    }
}
