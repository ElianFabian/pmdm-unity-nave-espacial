using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    #region Atributos
    [SerializeField] JugadorNave jugador;
    Image imgBarraDeVida;

    float Velocidad = 10;
    #endregion

    #region Métodos de Unity
    private void Awake()
    {
        imgBarraDeVida = GetComponent<Image>();
    }

    void Update()
    {
        Rellenar();
        CambiarColor();
    }
    #endregion

    #region Métodos
    void Rellenar()
    {
        imgBarraDeVida.fillAmount = Mathf.Lerp
        (
            imgBarraDeVida.fillAmount,
            jugador.Vida / jugador.maxVida,
            Velocidad * Time.deltaTime
        );
    }

    void CambiarColor()
    {
        imgBarraDeVida.color = Color.Lerp
        (
            Color.red,
            Color.green,
            imgBarraDeVida.fillAmount
        );
    }
    #endregion
}
