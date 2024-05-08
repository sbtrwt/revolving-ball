using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class PlateformMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 0.000000000000001f;
        private Vector2 mousePositionDown, mousePositionCurrent;
        private bool isMouseDown = false;


        public bool IsDragable = true;
        private void Update()
        {
            mousePositionCurrent = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                mousePositionDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                isMouseDown = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mousePositionDown = mousePositionCurrent;
                isMouseDown = false;
            }

        }
        private void FixedUpdate()
        {
            if (isMouseDown)
            {
                Move();
            }
        }


        private void Move()
        {
            if ((mousePositionDown.x - mousePositionCurrent.x) == 0) return;
            float direction = (mousePositionDown.x - mousePositionCurrent.x) > 0 ? -1f : 1f;
            //Debug.Log(mousePositionCurrent + " current");
            //Debug.Log(mousePositionDown + " down");
            Vector2 currentPos = new Vector3(transform.position.x + (direction * speed) * Time.deltaTime, transform.position.y );
            if (currentPos.x < 3 && currentPos.x > -3)
            {
                transform.position = currentPos;
            }
           
        }
    }
}
