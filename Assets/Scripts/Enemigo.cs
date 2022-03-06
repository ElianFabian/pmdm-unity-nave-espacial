using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemigo : MonoBehaviour
{
    #region Atributos
    [SerializeField] float Amplitud = 3;

    const string TAG_BALA_JUGADOR = "Bala Jugador";

    Rigidbody2D rbody;
    Vector2 Velocidad = new Vector2(2, 1);
    float Desfase;
    float AltruaInicial;

    bool EstaMuerto = false;

    const float limiteIzquierdo = -14;
    const float limiteInferior = -6;
    #endregion

    #region M�todos de Unity
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

        AltruaInicial = transform.position.y;

        //Velocidad = new Vector2(x, y);
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
    #endregion

    #region M�todos
    void Mover()
    {
        transform.position = new Vector2
        (
            // Movimiento lineal
            transform.position.x - Velocidad.x * Time.deltaTime,
            // Movimiento senoidal
            Amplitud * 0.5f * Mathf.Sin(Velocidad.y * transform.position.x) + AltruaInicial
        );
    }
    void Morir()
    {
        rbody.gravityScale = 1;

        EstaMuerto = true;

        ControladorJuego.Puntuacion++;
    }
    #endregion
}
