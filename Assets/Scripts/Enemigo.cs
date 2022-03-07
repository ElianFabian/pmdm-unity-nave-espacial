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
    SpriteRenderer spriteRenderer;

    Vector2 Velocidad = new Vector2(2, 1);
    float AltruaInicial;

    bool EstaMuerto = false;

    const float limiteIzquierdo = -14;
    const float limiteInferior  = -6;

    public System.Action OnMorir;
    #endregion

    #region Métodos de Unity
    private void Awake()
    {
        rbody          = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        rbody.gravityScale = 0;

        AltruaInicial = transform.position.y;
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

    #region Métodos
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
        spriteRenderer.color = new Color(0.1f, 0.1f, 0.1f);
        rbody.gravityScale = 1;

        EstaMuerto = true;

        OnMorir?.Invoke();
    }
    #endregion
}
