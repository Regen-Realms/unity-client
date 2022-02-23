using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public float followSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x, Time.deltaTime * followSpeed), 
                                         Mathf.Lerp(transform.position.y, target.transform.position.y, Time.deltaTime * followSpeed), -10);
    }
}
