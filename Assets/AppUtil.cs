using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// スマホ用のタッチ設定でもエディター上でマウスを使えるようにする
//
// 参照
// https://qiita.com/tempura/items/4a5482ff6247ec8873df

public static class AppUtil {

    public static TouchInfo GetTouchInfo()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))    return TouchInfo.Began;
            if (Input.GetMouseButton(0))        return TouchInfo.Moved;
            if (Input.GetMouseButtonUp(0))      return TouchInfo.Ended;
        }
        else
        {
            if(Input.touchCount > 0)
            {
                return (TouchInfo)((int)Input.GetTouch(0).phase);
            }
        }
        return TouchInfo.None;
    }

    public static Vector3 GetTouchPosition()
    {
        if (Application.isEditor)
        {
            if (GetTouchInfo() != TouchInfo.None) return Input.mousePosition;
        }
        else
        {
            if (Input.touchCount > 0) return Input.GetTouch(0).position;
        }

        return Vector3.zero;
    }
}

public enum TouchInfo
{
    None = 99,
    Began = 0,
    Moved = 1,
    Stationary = 2,
    Ended = 3,
    Canceled = 4
}
