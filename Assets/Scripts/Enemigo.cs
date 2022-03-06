using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemigo : MonoBehaviour
{
    [SerializeField] float Amplitud = 3;

    const string TAG_BALA_JUGADOR = "Bala Jugador";

    Rigidbody2D rbody;
    Vector2 Velocidad;
    float Desfase;

    bool EstaMuerto = false;

    const float limiteIzquierdo = -14;
    const float limiteInferior = -6;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rbody.gravityScale = 0;

        Desfase = Random.Range(0.0f, 2 * Mathf.PI);

        var x = Random.Range(1.0f, 3.0f);
        var y = Random.Range(1.0f, 3.0f);

        Velocidad = new Vector2(x, y);
    }
    void Update()
    {
        if (!EstaMuerto) Mover();

        if (transform.position.x < limiteIzquierdo || transform.position.y < limiteInferior)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(TAG_BALA_JUGADOR)) return;

        if (!EstaMuerto) Morir();
    }

    void Mover()
    {
        transform.position = new Vector2
        (
            // Movimiento lineal
            transform.position.x - Velocidad.x * Time.deltaTime,
            // Movimiento senoidal
            Amplitud * Mathf.Sin(Velocidad.y * Time.time + Desfase)
        );
    }

    void Morir()
    {
        rbody.gravityScale = 1;

        EstaMuerto = true;

        ControladorJuego.Puntuacion++;
    }
}
