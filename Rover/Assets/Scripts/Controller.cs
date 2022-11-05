using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float turnSpeed = 100.0f;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Configuracion teclado
        horizontalInput = Input.GetAxis("Mouse X");
        //Modificar el giro
        transform.Rotate(Vector3.forward, horizontalInput * Time.deltaTime * turnSpeed);
    }
}
