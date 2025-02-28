using UnityEngine;

public class RayCastDetecting : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayCastDistance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.forward, rayCastDistance));
            {
                Debug.Log("Ray Cast");
            }
        }
    }
}
