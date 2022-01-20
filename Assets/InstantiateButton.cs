using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateButton : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPress()
    {
        Vector3 mousePos = Input.mousePosition;
        
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        objectPos.z = 1.85f;
        Instantiate(prefab, objectPos, Quaternion.identity);
    }
}
