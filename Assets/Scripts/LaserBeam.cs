using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer linerenderer;
    private Ray ray;
    private RaycastHit hit;
    private bool laserOn = false;
    private Vector3 mOffset;
    private float mZcoord;
    private bool scroll;
    public float speed = 200;
    private bool insideGlass = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (laserOn)
        {
            ray = new Ray(transform.position, transform.forward);
            linerenderer.positionCount = 1;
            linerenderer.SetPosition(0, transform.position);
            int count = 0;
            while (true)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    
                    linerenderer.positionCount += 1;
                    linerenderer.SetPosition(linerenderer.positionCount - 1, hit.point);
                    if (hit.collider.tag == "Mirror")
                    {
                        Debug.Log("we hit mirror");
                        ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                    }
                    else if(hit.collider.tag == "Glass") {
                        if (insideGlass == false)
                        {
                            float refractiveRatio = 1.0f / 1.5f;
                            Vector3 newRayPos = hit.point + ray.direction * 0.001f;
                            Vector3 norm = hit.normal;
                            Vector3 incident = ray.direction;
                            Vector3 refractedVector = (refractiveRatio * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (refractiveRatio * refractiveRatio), Vector3.Cross(norm, incident)))).normalized;
                            ray = new Ray(newRayPos, refractedVector);
                            insideGlass = true;
                        }
                        else
                        {
                            float refractiveRatio = 1.5f / 1.0f;
                            Vector3 newRayPos = hit.point + ray.direction * 0.001f;
                            Vector3 norm = -hit.normal;
                            Vector3 incident = ray.direction;
                            Vector3 refractedVector = (refractiveRatio * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (refractiveRatio * refractiveRatio), Vector3.Cross(norm, incident)))).normalized;
                            ray = new Ray(newRayPos, refractedVector);
                            insideGlass = false;
                        }
                    }
                    else
                    {
                        break;
                    }
                    
                }
                else
                {
                    linerenderer.positionCount += 1;
                    linerenderer.SetPosition(linerenderer.positionCount - 1, ray.origin + ray.direction * 200);
                    break;
                }
            }
        }
        if (scroll)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {

                transform.Rotate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Rotate(Vector3.left * speed * Time.deltaTime);
            }
        }

    }
    private void OnMouseOver()
    {
        scroll = true;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            if(laserOn == false)
            {
                laserOn = true;
            }
            else
            {
                linerenderer.positionCount = 1;
                laserOn = false;
            }
        }
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
