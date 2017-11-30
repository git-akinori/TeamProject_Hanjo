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

	private void Start()
	{
		transform.position = arrowIcon.transform.position + arrow_offset;
	}

	public void SelectedIcon(Weapon weapon)
    {
        if (weapon == Weapon.ARROW)
        {
            transform.position = arrowIcon.transform.position + arrow_offset;
        }
        else if (weapon == Weapon.STONE)
        {
            transform.position = stoneIcon.transform.position + stone_offset;
        }
        else return;
    }
}
