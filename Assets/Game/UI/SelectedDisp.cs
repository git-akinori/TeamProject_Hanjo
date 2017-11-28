using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    ARROW = 0,
    STONE = 1,
}

public class SelectedDisp : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowIcon;
    [SerializeField]
    private GameObject stoneIcon;

    [SerializeField]
    private Vector3 arrow_offset = Vector3.zero;
    [SerializeField]
    private Vector3 stone_offset = Vector3.zero;

    private Vector3 arrow_pos = Vector3.zero;
    private Vector3 stone_pos = Vector3.zero;

    void Start()
    {
        arrow_pos = arrowIcon.transform.position + arrow_offset;
        stone_pos = stoneIcon.transform.position + stone_offset;

        transform.position = arrow_pos;
    }

    void Update()
    {

    }

    public void SelectedIcon(Weapon weapon)
    {
        if (weapon == Weapon.ARROW)
        {
            transform.position = arrow_pos;
        }
        else if (weapon == Weapon.STONE)
        {
            transform.position = stone_pos;
        }
        else return;
    }
}
