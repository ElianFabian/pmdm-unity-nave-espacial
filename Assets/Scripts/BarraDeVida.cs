using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    #region Atributos
    [SerializeField] JugadorNave jugador;
    Image imgBarraDeVida;
    #endregion

    #region M�todos de Unity
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

    #region M�todos
    void Rellenar()
    {
        var velocidad = 10 * Time.deltaTime;

        imgBarraDeVida.fillAmount = Mathf.Lerp
        (
            imgBarraDeVida.fillAmount,
            jugador.Vida / jugador.maxVida,
            velocidad
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
