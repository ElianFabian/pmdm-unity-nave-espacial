using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class JugadorNave : MonoBehaviour
{
    #region Aributos
    [SerializeField] Bala2D bala;
    [SerializeField] float Velocidad = 2;
    [SerializeField][Range(1, 60)] byte TasaDeDisparo = 5;

    public short Vidas = 5;
    public bool EstaMuerto = false;
    float SiguienteVezParaDisparar = 0;

    Rigidbody2D rbody;

    const float limiteInferior = -6;
    const string TAG_ENEMIGO = "Enemigo";
    #endregion

    #region Métodos de Unity
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rbody.gravityScale = 0;
        rbody.drag         = 2.5f;
    }
    void Update()
    {
        // Cuando el jugador muera se caerá y cuando sobre pase el límite inferior se destruirá
        if (transform.position.y < limiteInferior) Destroy(gameObject);

        if (EstaMuerto) return;

        Mover();

        LimitarPosicion();

        // Disparo normal
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Disparar();
        }
        // Dispar en ráfaga
        if (Input.GetKey(KeyCode.Mouse2) && Time.time >= SiguienteVezParaDisparar)
        {
            SiguienteVezParaDisparar = Time.time + 1/(float)TasaDeDisparo;
            Disparar();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(TAG_ENEMIGO) || EstaMuerto) return;

        if (!EstaMuerto) Vidas--;

        if (Vidas <= 0) EstaMuerto = true;

        if (EstaMuerto) Morir();
    }
    #endregion

    #region Métodos
    private void LimitarPosicion()
    {
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, -7.8f, 6.5f),
            Mathf.Clamp(transform.position.y, -3.8f, 3.8f),
            transform.position.z
        );
    }
    void Mover(float velocidad = 5)
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
    void Morir()
    {
        rbody.gravityScale = 1;

        EstaMuerto = true;
    }
    #endregion
}
