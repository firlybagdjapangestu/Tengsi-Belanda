using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGaze : MonoBehaviour
{
    [SerializeField] private int raycastDistance;
    [SerializeField] private RaycastHit raycastHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if(Physics.Raycast(ray,out raycastHit, raycastDistance))
        {
            if (raycastHit.transform.CompareTag("Fire"))
            {
                Destroy(raycastHit.transform.gameObject);
            }
        }
    }
}
