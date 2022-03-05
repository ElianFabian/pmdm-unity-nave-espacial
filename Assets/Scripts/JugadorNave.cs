using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class JugadorNave : MonoBehaviour
{
    [SerializeField] Bala2D bala;
    [SerializeField] float Velocidad = 2;
    [SerializeField][Range(1, 60)] byte TasaDeDisparo = 5;

    float SiguienteVezParaDisparar = 0;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rbody.gravityScale = 0;
        rbody.drag = 2.5f;
    }

    void Update()
    {
        MoverConTecladoFlechas();

        var posicion = transform.position;
        transform.position = new Vector3
        (
            Mathf.Clamp(posicion.x, -7.8f, 6.5f),
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

        rbody.AddForce(direccion * Velocidad);
    }

    void Disparar()
    {
        var nuevaBala = Instantiate(bala, transform.position + transform.forward, bala.transform.rotation);

        nuevaBala.Disparar();
    }
}
