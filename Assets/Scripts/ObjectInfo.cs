using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour
{
    public bool isSelected = false;

    public string objectName;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            RightClick();
        }
    }

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
                Debug.Log("The unit is moving right now!");
            }
            else if (hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("We are harvesting resources now......");
            }
        }

    }
}
