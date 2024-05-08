using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GAMEHIGAME.BouncingBall
{
    public class PlayerView : MonoBehaviour
    {
        
        [SerializeField]        private TMPro.TMP_Text bounceCountText;
        [SerializeField] private LineRenderer lineRenderer;

        private PlayerController playerController;
       
        
        private void Start()
        {
            UIManager.Instance.SetRestartPanel(false);
            Time.timeScale = 1;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
           
            if (collision.gameObject.CompareTag(GlobalConstant.Tags.PLATFORM))
            {

                playerController.OnPlatformCollision();
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GlobalConstant.Tags.GROUND))
            {
                ShowGameOver();
            }
        }

       public void SetController(PlayerController controller ) { playerController = controller; }

      
        public void SetBounceCountText(string bounceCount)
        {
            bounceCountText.text = bounceCount;
        }
        public void ShowGameOver()
        {
            UIManager.Instance.SetRestartPanel(true);
            Time.timeScale = 0;
        }
        public void SetShockLine(Vector2 startPosition, Vector2 endPosition)
        {
            lineRenderer.SetPositions(new Vector3[] { startPosition, endPosition });
        }
    }
}