using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    [SerializeField] Text txtVidas;
    [SerializeField] Text txtPuntuacion;
    [SerializeField] JugadorNave Jugador;

    public static uint Puntuacion = 0;

    void Update()
    {
        txtPuntuacion.text = $"Puntuación: {Puntuacion}";
        txtVidas.text = $"Vidas: {Jugador.Vidas}";
    }
}
