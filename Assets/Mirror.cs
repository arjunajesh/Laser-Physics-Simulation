using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public float speed = 200;
    private bool scroll = false;
    private Vector3 mOffset;
    private float mZcoord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
