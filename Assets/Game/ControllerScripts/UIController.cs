using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private GameObject _camera;
    private Vector3 cameraVecUp, cameraVecFor;
    private float radian;

    [SerializeField]
    private GameObject _northBall = null;
    [SerializeField]
    private GameObject _eastBall = null;
    [SerializeField]
    private GameObject _southBall = null;
    [SerializeField]
    private GameObject _westBall = null;

    private GameObject activeBall = null;
    private GameObject[] ballArray = new GameObject[4];
    private int activeNum = 0;

    [SerializeField]
    private float _minFlickDist = 70;
    private Vector2 touchBeganPos = Vector2.zero;
    private Vector2 touchingPos = Vector2.zero;
    private Vector2 touchPosDiff = Vector2.zero;
    //private Vector2 touchEndPos = Vector2.zero;

    [SerializeField]
    private ToDarken toDarken;
    [SerializeField]
    private FlagAction flagAction;

    public enum CameraMoveDir
    {
        LEFT = 0,
        RIGHT = 1,
        NONE = 99,
    }

    // 起動時に 60FPSに設定（いらないかも）
    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start() {
        radian = 0f;
        // Mainカメラの upベクトル と forwardベクトル を格納
        cameraVecUp = _camera.transform.up;
        cameraVecFor = _camera.transform.forward;

        // 方角の玉をそれぞれ配列に格納
        ballArray[0] = _northBall.gameObject;
        ballArray[1] = _eastBall.gameObject;
        ballArray[2] = _southBall.gameObject;
        ballArray[3] = _westBall.gameObject;
        
        // 方角の玉をすべて非表示に設定
        foreach(var ball in ballArray) { ball.SetActive(false); }

        // 0番（北）の玉を表示するよう設定
        activeNum = 0;
        activeBall = ballArray[activeNum];
        activeBall.SetActive(true);



        // フリックによるカメラ移動に必要な指の最低移動距離
        _minFlickDist = 80;
    }

    void Update() {
        // 一時停止時は反応させない
        if (Time.timeScale == 1.0f)
        {
            // タッチし始めたときの位置を格納
            if (AppUtil.GetTouchInfo() == TouchInfo.Began)
            {
                touchBeganPos = AppUtil.GetTouchPosition();
            }

            // タッチ中
            if (AppUtil.GetTouchInfo() == TouchInfo.Moved)
            {
                // タッチ中の位置とタッチし始めたときの位置の差を格納
                touchingPos = (Vector2)AppUtil.GetTouchPosition();
            }

            // タッチしていた指が離れたとき
            if (AppUtil.GetTouchInfo() == TouchInfo.Ended)
            {
                // タッチ中の位置とタッチし始めたときの位置の差を格納
                touchPosDiff = touchingPos - touchBeganPos;
                // 指を動かした方向を取得
                var direction = GetDirection(touchPosDiff);

                // 指の移動距離をリセット
                touchBeganPos = Vector2.zero;
                touchPosDiff = Vector2.zero;

                if (direction != CameraMoveDir.NONE)
                {
                    // 左なら90度左回転し、番号を下げる
                    // 右なら90度右回転し、番号を上げる
                    if (direction == CameraMoveDir.LEFT)
                    {
                        radian -= Mathf.PI / 2;
                        --activeNum;
                    }
                    else if (direction == CameraMoveDir.RIGHT)
                    {
                        radian += Mathf.PI / 2;
                        ++activeNum;
                    }

                    // Mainカメラの upベクトル と forwardベクトル を再設定
                    _camera.transform.up = new Vector3(Mathf.Sin(radian) * cameraVecUp.z, cameraVecUp.y, Mathf.Cos(radian) * cameraVecUp.z);
                    _camera.transform.forward = new Vector3(Mathf.Sin(radian) * cameraVecFor.z, cameraVecFor.y, Mathf.Cos(radian) * cameraVecFor.z);

                    // 値が 0~3 で循環するように
                    if (activeNum < 0) activeNum = 3;
                    if (activeNum > 3) activeNum = 0;

                    // 現在の玉を非表示にし、次の玉を表示する
                    activeBall.SetActive(false);
                    activeBall = ballArray[activeNum];
                    activeBall.SetActive(true);

                    flagAction.ResetFlags((Dir)activeNum);

                    // 画面切り替え時の暗転処理
                    toDarken.InitPos(direction);
                }
            }
        }
    }

    // カメラ動かす方向を取得
    // _touchDiff
    private CameraMoveDir GetDirection(Vector2 _touchDiff)
    {
        // 指を動かした方向が縦より横の方が大きい時
        if (Mathf.Abs(_touchDiff.y) < Mathf.Abs(_touchDiff.x)) {
            // 横方向の値から 移動方向を決定
            if (_touchDiff.x > _minFlickDist) return CameraMoveDir.LEFT;
            else if (_touchDiff.x < -_minFlickDist) return CameraMoveDir.RIGHT;
        }
        return CameraMoveDir.NONE;
    }

    // 現在向いている方向を判断する値を取得する
    public int ActiveNum { get { return activeNum; } }
    public Dir ActiveDir { get { return (Dir)activeNum; } }

    // Mainカメラの forwardベクトル を取得する
    public Vector3 CameraForVec { get { return _camera.transform.forward; } }
}
