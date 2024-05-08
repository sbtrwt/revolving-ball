using System.Collections;
using UnityEngine;
using TMPro;
namespace GAMEHIGAME.BouncingBall
{
    public class PowerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text powerText;
        private PowerController powerController;
        public void SetController(PowerController controller) { powerController = controller; }

        private void OnTriggerEnter2D(Collider2D collision) => powerController?.PowerUpTriggerEntered(collision.gameObject);
        public void SetText(string text)
        {
            powerText.text = text;
        }
    }
}
