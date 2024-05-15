using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircleMove : MonoBehaviour
{
    public static CircleMove instance;
    #region var  

    public int mDirection = -1;
    public float ZIndx;
    public Material lineMaterial;
    public float mRadius, mAngle, mSpeed, mOffset = 0.5f, mTimeperiod;
    public Vector3 mCenter, mPosition;
    public GameObject mCurrentCenter, mLastCenter, mLightStart, mLightEnd;

    public float drawRadiusTimer = 0.05f;
    public float drawLineTimer = 100f;
    public Button btnRope;
    public bool IsStarted = false, IsAnimation = false;

    #endregion

    #region Event
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        btnRope = GameObject.FindGameObjectWithTag("btnrope").GetComponent<Button>();
        if (btnRope != null)
        {
            btnRope.onClick.AddListener(() => Rope());
        }
    }

    void Start()
    {
        InitGameStart();
        IsStarted = true;
    }



    void FixedUpdate()
    {
        transform.position = new Vector3(mPosition.x, mPosition.y, ZIndx);
    }

    void OnGUI()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(mCenter, mPosition);

        //foreach (var line in SolveAns.Ans)
        //{
        //    Gizmos.DrawLine(line._objStart.position, line._objEnd.position);
        //}
    }
    public void Move()
    {
        UpdateCenter();
        CalculateSpeed();
        mAngle += (mSpeed * mDirection * Time.deltaTime);//+ (mOffset * Time.deltaTime * mDirection * mRadius)

        mPosition.x = Mathf.Cos(mAngle) * mRadius + mCenter.x;
        mPosition.y = Mathf.Sin(mAngle) * mRadius + mCenter.y;


        if (2 * Mathf.PI < Mathf.Abs(mAngle))
        {
            mAngle = 0;
        }
        SetLightning(mPosition, mCenter);
    }
    #endregion

    #region Custom
    public void SetCenter()
    {

        mCurrentCenter = FindClosestCenter();
        if (CheckCenterChange(mCurrentCenter, mLastCenter))
        {

            mLastCenter = mCurrentCenter;
            mCenter = mCurrentCenter.transform.position;
            mRadius = Vector2.Distance(mCenter, mPosition);
            mAngle = CalculateAngle(mCenter, mPosition, mRadius);

        }
        if (mCurrentCenter != null)
        {
            mCenter = mCurrentCenter.transform.position;

        }


    }

    void UpdateCenter()
    {
        if (mCurrentCenter != null)
        {
            mCenter = mCurrentCenter.transform.position;
        }
    }

    public GameObject FindClosestCenter()
    {
        GameObject[] Centers;
        Centers = GameObject.FindGameObjectsWithTag("topcenter");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Centers)
        {
            if (go.Equals(mLastCenter))
            {
                //Debug.Log("Inside Find closest check");
                continue;
            }
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        Centers = GameObject.FindGameObjectsWithTag("bottomcenter");
        foreach (GameObject go in Centers)
        {
            if (go.Equals(mLastCenter))
            {
                //Debug.Log("Inside Find closest check");
                continue;
            }
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
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

    bool CheckCenterChange(GameObject current, GameObject last)
    {
        if (current == null && last == null)
            return true;
        return !current.Equals(last);
    }
    void InitGameStart()
    {
        mTimeperiod = 5;
        mCurrentCenter = mLastCenter = null;
        //mSpeed = (2 * Mathf.PI) / mTimeperiod; // 2 * PI in degrees is 360, so you get 5 Sec to complete a circle
        mAngle = 0;
        mRadius = 1;
        //mOffset = 0.05f;
        mPosition = gameObject.transform.position;
        CalculateSpeed();
        SetCenter();
        if (mCurrentCenter != null)
        {
            mDirection = (mCurrentCenter.tag.Equals("topcenter")) ? 1 : -1;
        }



    }
    void CalculateSpeed()
    {
        mSpeed = (2 * Mathf.PI) / mTimeperiod;
    }
    void Rope()
    {
        SetCenter();
        if (mCurrentCenter != null)
        {
            mDirection = (mCurrentCenter.tag.Equals("topcenter")) ? 1 : -1;
        }
    }

    void DrawLine(Vector2 start, Vector2 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = (0.5f);
        lr.endWidth = (0.5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingOrder = 2;
        GameObject.Destroy(myLine, duration);
    }
    public void SetLightning(Vector3 start, Vector3 End)
    {
        mLightStart.transform.position = start;
        mLightEnd.transform.position = End;
    }
    #endregion
}
