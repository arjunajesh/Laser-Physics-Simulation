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

            while (true)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    linerenderer.positionCount += 1;
                    linerenderer.SetPosition(linerenderer.positionCount - 1, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
                else
                {
                    linerenderer.positionCount += 1;
                    linerenderer.SetPosition(linerenderer.positionCount - 1, ray.origin + ray.direction * 200);
                    break;
                    Debug.Log("no more collisions");
                }
            }
        }
        
    }
    private void OnMouseOver()
    {
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

}
