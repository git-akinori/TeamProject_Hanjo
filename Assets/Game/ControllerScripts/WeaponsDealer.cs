using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsDealer : MonoBehaviour
{

	[SerializeField]
	private GameObject _arrow = null;
	[SerializeField]
	private GameObject _stone = null;
	//[SerializeField]
	//private GameObject _oil = null;
	private GameObject obj;

	[SerializeField]
	private Image _arrowFilled = null;
	[SerializeField]
	private Image _stoneFilled = null;
	[SerializeField]
	private Image _oilFilled = null;

	[SerializeField]
	private float _setArrowCT = 0.15f;
	[SerializeField]
	private float _setStoneCT = 2f;
	[SerializeField]
	private float _setOilCT = 10f;


	private Vector2 touchBeganPos = Vector2.zero;
	//private Vector2 touchEndedPos = Vector2.zero;
	private Vector2 touchPosDiff = Vector2.zero;

	CoolTimeManager[] weapons = new CoolTimeManager[3];

	Vector3 arrivalPos = Vector3.zero;
	RaycastHit raycastHit;

	[SerializeField]
	SelectedDisp selectedDisp = null;

	[SerializeField]
	float timeScale = 0.05f;

    [SerializeField]
    private Animator _animator;
    private readonly int _isAttack = Animator.StringToHash("isAttack");
    private float _isAttackTime;

	void Start()
	{
		obj = _arrow;

		weapons[0] = new CoolTimeManager(_arrowFilled, _setArrowCT);
		weapons[1] = new CoolTimeManager(_stoneFilled, _setStoneCT);
		weapons[2] = new CoolTimeManager(_oilFilled, _setOilCT);
	}

	private void Update()
	{
		// 仮置き
		if (Input.GetButtonDown("Fire2"))
		{
			if (Time.timeScale == 1.0F)
			{
				Debug.Log("pause! Right Click");
				Time.timeScale = timeScale;
			}
			else
				Time.timeScale = 1.0F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}

		if (Time.timeScale == 1.0f)
		{
			foreach (var weapon in weapons)
			{
				weapon.Update();
			}

			var touchPos = AppUtil.GetTouchPosition();

			if (AppUtil.GetTouchInfo() == TouchInfo.Began)
			{
				touchBeganPos = touchPos;

				//Debug.Log(AppUtil.GetTouchPosition());
			}

			if (AppUtil.GetTouchInfo() == TouchInfo.Moved)
			{
				touchPosDiff = (Vector2)touchPos - touchBeganPos;
			}


			if (AppUtil.GetTouchInfo() == TouchInfo.Ended)
			{
				Ray ray = Camera.main.ScreenPointToRay(touchPos);
				RaycastHit[] hits = Physics.RaycastAll(ray);
				foreach (var hit in hits)
				{
					if (hit.collider.tag == "Wall")
					{
						Debug.Log("Wall");
						break;
					}
					if (touchPosDiff.magnitude < 30 && hit.collider.tag == "Stage")
					{
						//Debug.Log(hit.point);
						// 弾の生成
						var _obj = GenerateBullet();

                        _animator.SetBool(_isAttack, true);
                        _isAttackTime = 0;

						if (_obj != null)
						{
							raycastHit = hit;
							arrivalPos = hit.point;
						}

						break;
					}
				}
			}

            _isAttackTime += Time.deltaTime;

            if (_isAttackTime > 3)
                _animator.SetBool(_isAttack, false);
		}
	}

	public Vector3 ArrivalPos
	{
		get { return arrivalPos; }
	}

	public RaycastHit ArrivalPosRayHit
	{
		get { return raycastHit;  }
	}

	public GameObject GenerateBullet()
	{
		if (obj)
		{
			var i = 0;
			switch (obj.gameObject.tag)
			{
				case "Arrow": i = 0; break;
				case "Stone": i = 1; break;
			}

			if (weapons[i].ElapsedTime >= weapons[i].CoolTime)
			{
				weapons[i].ElapsedTime = 0;
			}

			if (weapons[0].ElapsedTime == 0 || weapons[1].ElapsedTime == 0)
			{
				return Instantiate(obj, obj.transform.position, obj.transform.localRotation, transform);
			}
		}
		return null;
	}

	class CoolTimeManager
	{
		private Image img;
		private float coolTime;
		private float elapsedTime;

		public CoolTimeManager(Image _img, float _coolTime)
		{
			if (_img == null) return;
			img = _img;
			elapsedTime = coolTime = _coolTime;
		}

		public void Update()
		{
			if (elapsedTime < coolTime)
			{
				elapsedTime += Time.deltaTime;
				img.fillAmount = elapsedTime / coolTime;
			}
			else if (elapsedTime < 0) elapsedTime = 0;
		}

		public float ElapsedTime
		{
			get
			{
				return elapsedTime;
			}
			set
			{
				if (value >= 0 && value <= coolTime)
					elapsedTime = value;
			}
		}

		public float CoolTime
		{
			get { return coolTime; }
		}
	}

	public void TapArrow()
	{
		selectedDisp.SelectedIcon(Weapon.ARROW);
		obj = _arrow;
	}

	public void TapStone()
	{
		selectedDisp.SelectedIcon(Weapon.STONE);
		obj = _stone;
	}

	public void TapOil()
	{
		if (weapons[2].ElapsedTime >= weapons[2].CoolTime)
		{
			weapons[2].ElapsedTime = 0;
			//Instantiate(_oil, new Vector2(_oil.transform.position.x, wallLine), Quaternion.identity);
		}
	}
}
