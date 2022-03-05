using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jugador : MonoBehaviour
{
    [SerializeField] Bala2D bala;
    [SerializeField] float Velocidad = 2;

    float SiguienteVezParaDisparar = 0;
    byte TasaDeDisparo = 10;

    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoverConTecladoFlechas();

        var posicion = transform.position;
        transform.position = new Vector3
        (
            Mathf.Clamp(posicion.x, -7.8f, 4.8f),
            Mathf.Clamp(posicion.y, -3.8f, 3.8f),
            transform.position.z
        );

        // Disparo normal
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Disparar();
        }

        // Dispar en ráfaga
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= SiguienteVezParaDisparar)
        {
            SiguienteVezParaDisparar = Time.time + 1/(float)TasaDeDisparo;
            Disparar();
        }
    }


    void MoverConTecladoFlechas(float velocidad = 5)
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var direccion = new Vector3(h, v, 0);
        direccion = direccion.normalized;

        rigidbody.AddForce(direccion * Velocidad);
        //transform.Translate(Time.deltaTime * velocidad * direccion, Space.World);
    }

    void Disparar()
    {
        var nuevaBala = Instantiate(bala, transform.position + transform.forward, bala.transform.rotation);

        nuevaBala.Disparar();
    }
}
