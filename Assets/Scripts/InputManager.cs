using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;

    private float panDetect = 20f;

    private float minHeight = 20f;
    private float maxHeight = 200f;

    public GameObject selectedObject;

    private ObjectInfo selectedInfo;




    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();

        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;

        }

    }

    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                selectedObject = null;
                Debug.Log("DeSelected right now.");
            }
            else if (hit.collider.tag == "Selectable")
            {
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;

                Debug.Log("You have now selected:" + selectedInfo.objectName);
            }


        }



    }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        float xPosi = Input.mousePosition.x;
        float yPosi = Input.mousePosition.y;


        if (Input.GetKey(KeyCode.A) || xPosi > 0 && xPosi < panDetect)
        {
            moveX -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || xPosi < Screen.width && xPosi  > Screen.width - panDetect)
        {

            moveX += panSpeed;

        }

        if (Input.GetKey(KeyCode.W) || yPosi < Screen.height && yPosi > Screen.height - panDetect)
        {

            moveZ += panSpeed;
        }
        else if (Input.GetKey(KeyCode.S) || yPosi > 0 && yPosi < panDetect)
        {
            moveZ -= panSpeed;

        }

        moveY -= Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 50);

        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);

        Vector3 newPos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = newPos;

    }

    void RotateCamera() {

        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if (Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;

        }

        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin,destination, Time.deltaTime * rotateSpeed);

        }


    }
}
