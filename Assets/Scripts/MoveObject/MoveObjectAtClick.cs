using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectAtClick : MonoBehaviour
{
    public RectTransform moveObject;

    // Start is called before the first frame update
    void Start()
    {
        moveObject.localPosition = new Vector3(-1495, -744, 52);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveObject()
    {
        moveObject.localPosition = new Vector3(-1581, -787, 38);
    }


}
