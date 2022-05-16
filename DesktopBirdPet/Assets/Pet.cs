using System.Collections;
using UnityEngine;

public class Pet : MonoBehaviour
{

    enum Decision{
        WALK,
        SIT
    }

    Vector3 distination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isCurrentStateDone()
    {
        return false;
    }

    void FixedUpdate()
    {
        
    }

    IEnumerator DecisionMaking()
    {
        while (true)
        {
            isCurrentStateDone();
            yield return new WaitForSeconds(3);
        }
    }
}
