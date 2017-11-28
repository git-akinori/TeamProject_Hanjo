using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDarken : MonoBehaviour
{
    [SerializeField]
    private float offset_x = 60;
    [SerializeField]
    private float offset_z = -2000;
    [SerializeField]
    private float speed = 500;
    [SerializeField]
    private bool on = false;

    private float elapsedTime = 0;

    private Vector3 toPos;

    void Start()
    {
        transform.localPosition = new Vector3(offset_x, 0, offset_z);
    }

    void Update()
    {
        if (on)
        {
            // onになってから0.5秒間 画面を横切るように移動
            elapsedTime += Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, toPos, speed * Time.deltaTime);
            if (elapsedTime > 0.5)
            {
                // 経過時間をリセットし、移動フラグを折る
                elapsedTime = 0;
                on = false;
            }
        }
    }

    // 位置の初期化
    // dir カメラの移動方向
    public void InitPos(UIController.CameraMoveDir dir)
    {
        Vector3 pos;

        if (dir == UIController.CameraMoveDir.LEFT) pos = new Vector3(offset_x, 0, offset_z);
        else if (dir == UIController.CameraMoveDir.RIGHT) pos = new Vector3(-offset_x, 0, offset_z);
        else return;

        transform.localPosition = new Vector3(-pos.x * 0.6f, pos.y, pos.z);
        toPos = pos;

        on = true;
        elapsedTime = 0;
    }
}
