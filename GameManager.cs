using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Imagecover;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnClickStartButton()
    {
        Imagecover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
