using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{

    public static GuestManager s_instance;
    public static int GuestCount = 0;
    public GameObject[] GuerstPrefab;
    void instance()
    {
        if(s_instance==null)
        {
            s_instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
     

        
    }

    public void GuestAdd()
    {
        //instanciate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
