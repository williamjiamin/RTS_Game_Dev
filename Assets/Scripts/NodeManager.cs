using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public enum ResourceTypes { Stone}
    public ResourceTypes resourceType;

    public float harvestTime;
    public float availableResource;

    public bool beingGathered;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResourceTick());
    }

    // Update is called once per frame
    void Update()
    {
        if (availableResource <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ResourceGather()
    {
        if (beingGathered) {

            availableResource--;
        }

    }

    IEnumerator ResourceTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ResourceGather();

        }
    }
}
