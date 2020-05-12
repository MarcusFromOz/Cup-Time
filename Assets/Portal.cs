using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    //** variables
    [SerializeField] int sceneToLoad = -1;

    //** Start and Update
    void Start()
    {
       
    }
        
    //** Public methods
       
    //** Private methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            {
            SceneManager.LoadScene(1);
        }
    }
}
