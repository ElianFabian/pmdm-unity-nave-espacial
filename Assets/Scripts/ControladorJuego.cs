using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    #region Atributos
    [SerializeField] Text        txtPuntuacion;
    [SerializeField] JugadorNave Jugador;
    [SerializeField] AudioSource Musica;
    [SerializeField] AudioSource GameOverSound;

    string escenaActual;

    public static uint Puntuacion = 0;
    bool JugadorSeHaMuerto = false;
    #endregion

    #region M?todos de Unity
    void Awake()
    {
        escenaActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Musica = GameObject.Find("MUSICA").GetComponent<AudioSource>();
    }

    void Update()
    {
        txtPuntuacion.text = $"Puntuaci?n: {Puntuacion}";

        if (Input.GetKeyDown(KeyCode.R)) Reiniciar();

        if (Jugador.EstaMuerto && !JugadorSeHaMuerto)
        {
            Musica.Stop();
            GameOverSound.Play();

            JugadorSeHaMuerto = true;
        }
    }
    #endregion

    #region M?todos
    public void Reiniciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(escenaActual);

        // Al reiniciar s?lo se reproducir? la m?sica de nuevo si el jugador est? muerto
        // ya que de forma normal la m?sica contin?a independientemente de reiniciarse
        if (Jugador.EstaMuerto) Musica.Play();

        Puntuacion = 0;
    }
    #endregion
}
