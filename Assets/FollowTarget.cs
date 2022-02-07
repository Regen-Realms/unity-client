using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public float followSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x, Time.deltaTime * followSpeed), 
                                           Mathf.Lerp(transform.position.y, target.transform.position.y, Time.deltaTime * followSpeed), -10);

        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        
    }
}
