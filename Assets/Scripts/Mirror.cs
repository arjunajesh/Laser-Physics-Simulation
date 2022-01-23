using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public float speed = 200;
    private bool scroll = false;
    private Vector3 mOffset;
    private float mZcoord;
    public bool initialized =  true;
    //public bool isStartingObject = false;
    // Start is called before the first frame update
    void Start()
    {
       
            mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            Debug.Log("bruh");
            
            transform.position = GetMouseWorldPos() + mOffset;
        }
        if (scroll)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {

                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Debug.Log("bruh");
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
            }
        }
    }
    private void OnMouseOver()
    {
        scroll = true;
    }
    private void OnMouseExit()
    {
        scroll = false;
    }
    private void OnMouseDown()
    {
        if (initialized)
        {
            
            initialized = false;
        }
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZcoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Trash")
        {
            Destroy(gameObject);
        }
    }
}
