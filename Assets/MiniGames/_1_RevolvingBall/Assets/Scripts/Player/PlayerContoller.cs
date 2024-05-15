
using GAMEHIGAME.Root;
using GAMEHIGAME.Utilities;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class PlayerController 
    {
        private PlayerView playerView;
        private PlayerSO playerSO;
        private int score;
        private int highScore;
        private int direction = -1;
        private int currentBounceCount;
        private Vector2 currentPosition;
        private Vector2 currentCenter;
        private float speed;
        private float timePeriod;
        private float currentAngle;
        private float radius;
        private List<CenterPoint> allCenter;
        public PlayerController(PlayerSO playerSO)
        {
            this.playerSO = playerSO;
            currentBounceCount = playerSO.BounceCount;
            InitView();
            highScore = GameService.Instance.MiniGameHighScore;
            
            Init();
            //GameService.Instance.MiniGameSO.MiniGameHighScore = 0;
            //SaveSystem.SaveGameData(new GameData(GameService.Instance.MiniGameSO));
        }
        public void InitView()
        {
            playerView = Object.Instantiate(playerSO.PlayerView);
            playerView.SetController(this);
        }

        private void Init()
        {
            allCenter = new List<CenterPoint>();
            speed = 2;
            timePeriod = 2;
            //currentPosition = new Vector2(3, 3);
            radius = 2;
            //currentAngle = 2;
            currentCenter = new Vector2(0, 0);
            SetBounceCountText();


        }
        public void SetScoreText()
        {
            UIManager.Instance.SetScoreText(score.ToString());
        }
        public void SetBounceCountText()
        {
            playerView.SetBounceCountText(currentBounceCount.ToString());
        }
        public bool IsBounceCountZero()
        {
            return (currentBounceCount <= 0);
        }

        public void AddCurrentBounceCount(int bounceCount)
        {
            currentBounceCount += bounceCount;
            if(currentBounceCount > playerSO.BounceCount)
            {
                currentBounceCount = playerSO.BounceCount;
            }
            else if(currentBounceCount < 0) { currentBounceCount = 0; }
        }
        public void OnPlatformCollision()
        {
            AddCurrentBounceCount(-1);
            SetBounceCountText();
            if (IsBounceCountZero()) { playerView.ShowGameOver(); }
        }
        public void OnPowerCollision()
        {
            score++;
            if(score > highScore)
            {
                GameService.Instance.MiniGameHighScore= score;
                //MainGameService.Instance.SetHighScore(MainGameService.Instance.CurrentMiniGameID, score);

                SaveSystem.SaveGameData(new GameData(GameService.Instance.MiniGameSO));
            }
            SetBounceCountText();
            SetScoreText();
        }

        public void Move()
        {
            
            CalculateSpeed();
            currentAngle += (speed * direction * Time.deltaTime);//+ (mOffset * Time.deltaTime * mDirection * mRadius)

            currentPosition.x = Mathf.Cos(currentAngle) * radius + currentCenter.x;
            currentPosition.y = Mathf.Sin(currentAngle) * radius + currentCenter.y;


            if (2 * Mathf.PI < Mathf.Abs(currentAngle))
            {
                currentAngle = 0;
            }

            playerView.transform.position = currentPosition;
            playerView.SetShockLine(currentCenter, currentPosition);

            CenterPoint nearCenter = FindClosestCenter();
            if(nearCenter != null)
            {
                nearCenter.SpriteRenderer.color = nearCenter.SelectColor;
            }
            
        }

        private void CalculateSpeed()
        {
            speed = (2 * Mathf.PI) / timePeriod;
        }
        float CalculateAngle(Vector2 center, Vector2 position, float radius)
        {

            Vector2 startAngle = new Vector2(radius + center.x, center.y);
            float angle = Vector2.Angle(center - startAngle, center - position);
            if (center.y > position.y)
            {
                angle = 360 - angle;
            }
            //Debug.Log(string.Format("calculate Angle2:{0}", angle));
            return (angle * Mathf.PI / 180);
        }

     
        public void SetCenter()
        {

            CenterPoint newCenter = FindClosestCenter();
            if (newCenter!=null && CheckCenterChange(currentCenter, newCenter.Transform.position))
            {
                radius = Vector2.Distance(currentPosition, newCenter.Transform.position);
                //direction = (newCenter.y > currentCenter.y)?1: -1;
                direction *= -1;
                currentCenter = newCenter.Transform.position;
                currentAngle = CalculateAngle(currentCenter, currentPosition, radius);
                OnPlatformCollision();
            }

        }

        private CenterPoint FindClosestCenter()
        {
            CenterPoint newCenter = null;
            float distance = Mathf.Infinity;
            foreach(var center in allCenter)
            {
                center.SpriteRenderer.color = center.DefaultColor;
                if(CheckCenterChange(currentCenter, center.Transform.position))
                {
                    float currentDistance = Vector2.Distance(currentPosition, center.Transform.position);
                    if(currentDistance < distance)
                    {
                        distance = currentDistance;
                        newCenter = center;
                    }
                }
            }
            return newCenter;
        }
        bool CheckCenterChange(Vector2 current, Vector2 last)
        {
            return !(current.x == last.x && current.y == last.y);
        }

        public void SetAllCenter(List<Transform> allCenter, List<SpriteRenderer> allCenterSpreiteRenderer)
        {
            //this.allCenter = allCenter;
            this.allCenter = new List<CenterPoint>();
            int length = allCenter.Count;
            for(int i=0; i< length; i++)
            {
                this.allCenter.Add(new CenterPoint
                {
                    Transform = allCenter[i],
                    SpriteRenderer = allCenterSpreiteRenderer[i],
                    DefaultColor = Color.white,
                    SelectColor = Color.red
                }) ;
                ;
            }
        }
    }

    [System.Serializable, Inspectable]
    public class CenterPoint 
    {
         public Transform Transform { get; set; }
       public SpriteRenderer SpriteRenderer { get; set; }
         public Color DefaultColor { get; set; }
      public Color SelectColor { get; set; }
    }
}
