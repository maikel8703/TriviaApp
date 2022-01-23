using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AnyManager anyManager;

    bool gameStart;
    void Awake()
    {
        if(!gameStart)
        {
            anyManager=this;
            SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
            gameStart=true;
        }
        
    }

    public void UnloadScene(int scene){

        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(int scene)
    {
        yield return null;
        SceneManager.UnloadScene(scene);
    }

    // Update is called once per frame
}
