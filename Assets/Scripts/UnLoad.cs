using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnLoad : MonoBehaviour
{
    // Start is called before the first frame update
  
    public int scene;

    bool unloaded;

    void Awake()
    {
        

        AnyManager.anyManager.UnloadScene(scene);
        unloaded=true;
        
    }
}
