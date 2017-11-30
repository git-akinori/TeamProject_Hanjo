using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAction : MonoBehaviour
{

    [SerializeField]
    private Sprite flag_N = null;
    [SerializeField]
    private Sprite flag_E = null;
    [SerializeField]
    private Sprite flag_S = null;
    [SerializeField]
    private Sprite flag_W = null;

    [SerializeField]
    private Sprite char_N = null;
    [SerializeField]
    private Sprite char_E = null;
    [SerializeField]
    private Sprite char_S = null;
    [SerializeField]
    private Sprite char_W = null;

    [SerializeField]
    private EnemyCounter enemyCounter_N = null;
    [SerializeField]
    private EnemyCounter enemyCounter_E = null;
    [SerializeField]
    private EnemyCounter enemyCounter_S = null;
    [SerializeField]
    private EnemyCounter enemyCounter_W = null;

    struct DirElements
    {
        public Sprite flag;
        public Sprite chara;
        public EnemyCounter enemyCounter;

        public DirElements(Sprite flag, Sprite chara, EnemyCounter enemyCounter)
        {
            this.flag = flag;
            this.chara = chara;
            this.enemyCounter = enemyCounter;
        }
    }

    DirElements[] dirElements = new DirElements[4];

    [SerializeField]
    private GameObject rightObj = null;
    [SerializeField]
    private GameObject behindObj = null;
    [SerializeField]
    private GameObject leftObj = null;

    struct Flag
    {
        public GameObject obj;
        public SpriteRenderer flagSR;
        public SpriteRenderer charSR;
        public int directionNum;
        public EnemyCounter enemyCounter;

        public Vector3 init_pos;
        public Vector3 moved_pos;

        public float elapsedTimeForMove;
        public float elapsedTimeForRotate;

        public float angle;

        public void Initialize()
        {
            flagSR = obj.transform.GetChild(0).GetComponent<SpriteRenderer>();
            charSR = obj.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    }

    Flag[] flags = new Flag[3];

    [SerializeField]
    private float speed = 10f;

    void Start()
    {
        // 旗のスプライト
        // 文字のスプライト
        // ステージのスクリプト をそれぞれの配列に格納
        dirElements[(int)Dir.NORTH] = new DirElements(flag_N, char_N, enemyCounter_N); // 0
        dirElements[(int)Dir.EAST] = new DirElements(flag_E, char_E, enemyCounter_E); // 1
        dirElements[(int)Dir.SOUTH] = new DirElements(flag_S, char_S, enemyCounter_S); // 2
        dirElements[(int)Dir.WEST] = new DirElements(flag_W, char_W, enemyCounter_W); // 3

        // 旗の配列にそれぞれのオブジェクトを格納
        flags[0].obj = rightObj;
        flags[1].obj = behindObj;
        flags[2].obj = leftObj;

        // 右(0),後(1),左(2) の 旗と文字 の SpriteRenderer を格納
        // これらのスプライトを切り替えていく
        for (int i = 0; i < 3; ++i)
        {
            flags[i].Initialize();
        }

        // 旗の移動に関する基準値を設定
        Vector3 base_rot = new Vector3(0, 0, -34);
        float degree = 90 + base_rot.z;
        float radian = degree * Mathf.PI / 180.0f;
        Vector3 move_vec = new Vector3(-67, 0, 0);
        move_vec.y = move_vec.x * Mathf.Tan(radian);

        // 旗の初期位置と移動先位置を設定
        flags[1].init_pos = flags[1].obj.transform.localPosition += move_vec;
        flags[1].moved_pos = flags[1].init_pos - move_vec;
        flags[2].init_pos = flags[2].obj.transform.localPosition += move_vec;
        flags[2].moved_pos = flags[2].init_pos - move_vec;

        // 右旗は逆位置
        move_vec.x = -move_vec.x;
        flags[0].init_pos = flags[0].obj.transform.localPosition += move_vec;
        flags[0].moved_pos = flags[0].init_pos - move_vec;

        ResetFlags(Dir.NORTH);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 3; ++i)
        {
            // 旗に対応する方角の敵数を取得
            int enemy_num = flags[i].enemyCounter.EnemyNum;

            // 敵が1体以上なら
            if (enemy_num >= 1)
            {
                flags[i].elapsedTimeForMove += Time.deltaTime;

                if (flags[i].elapsedTimeForMove < 120f / speed)
                {
                    flags[i].obj.transform.localPosition =
                        Vector3.MoveTowards(flags[i].obj.transform.localPosition, flags[i].moved_pos, speed * Time.deltaTime);
                }

                // 敵が一定数以上なら
                if (enemy_num >= 3)
                {
                    flags[i].elapsedTimeForRotate += Time.deltaTime * 2f;
                    flags[i].angle = Mathf.Cos(flags[i].elapsedTimeForRotate) * 0.5f;
                    flags[i].obj.transform.Rotate(new Vector3(0, 0, flags[i].angle));
                }
            }
            else
            {
                flags[i].elapsedTimeForMove = 0;

                flags[i].obj.transform.localPosition =
                    Vector3.MoveTowards(flags[i].obj.transform.localPosition, flags[i].init_pos, speed * Time.deltaTime);
            }
        }
    }

    // 旗情報の再設定
    public void ResetFlags(Dir _dir)
    {
        for (int i = 0; i < 3; ++i)
        {
            // 方角を再設定
            int num = flags[i].directionNum = ((int)_dir + i + 1) % 4;

            // 正面を 0 として、右から時計回りに 1, 2, 3 を足して 4で割ったあまりで、
            // それぞれに対応する方角を判定し、スプライトを再設定する
            flags[i].flagSR.sprite = dirElements[num].flag;
            flags[i].charSR.sprite = dirElements[num].chara;

            // 敵数が0なら初期位置に戻す
            flags[i].enemyCounter = dirElements[num].enemyCounter;
            if (flags[i].enemyCounter.EnemyNum == 0)
                flags[i].obj.transform.localPosition = flags[i].init_pos;
        }
    }
}
