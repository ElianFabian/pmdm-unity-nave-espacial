using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    [SerializeField] Text txtPuntuacion;
    [SerializeField] JugadorNave Jugador;
    [SerializeField] AudioSource Musica;
    [SerializeField] AudioSource GameOverSound;

    string escenaActual;

    public static uint Puntuacion = 0;
    bool JugadorSeHaMuerto = false;

    private void Awake()
    {
        escenaActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Musica = GameObject.Find("MUSICA").GetComponent<AudioSource>();
    }

    void Update()
    {
        txtPuntuacion.text = $"Puntuación: {Puntuacion}";

        if (Input.GetKeyDown(KeyCode.R)) Reiniciar();

        if (Jugador.EstaMuerto && !JugadorSeHaMuerto)
        {
            Musica.Stop();
            GameOverSound.Play();

            JugadorSeHaMuerto = true;
        }
    }

    public void Reiniciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(escenaActual);

        // Al reiniciar sólo se reproducirá la música de nuevo si el jugador está muerto
        // ya que de forma normal la música continúa independientemente de reiniciarse
        if (Jugador.EstaMuerto) Musica.Play();
    }
}
