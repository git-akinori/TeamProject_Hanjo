using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	[SerializeField]
	private float _speed = 400;
	[SerializeField]
	private float _offsetY = -50;
	//[SerializeField]
	//private float emitPos;

	private Vector3 arrivalPos;


	Vector3 rotate;

	[SerializeField]
	GameObject target;
	GameObject _target;

	[SerializeField]
	GameObject effect;

	[SerializeField]
	GameObject _bulletSE;
	GameObject bulletSE;

	[SerializeField]
	AudioClip fireSE;
	[SerializeField]
	AudioClip hitSE;

	void Start()
	{
		// 到達点を設定
		arrivalPos = PreLoad.Scripts.WeaponsDealer.ArrivalPos;

		// 初期位置と向きを設定
		var pos = PreLoad.Scripts.UIController.CameraForVec * 100;
		_offsetY += Camera.main.transform.position.y;

		transform.position = (Mathf.Abs(pos.x) > Mathf.Abs(pos.z))
			 ? new Vector3(pos.x, _offsetY, arrivalPos.z * 0.15f)
			 : new Vector3(arrivalPos.x * 0.15f, _offsetY, pos.z);

		transform.LookAt(new Vector3(0, transform.position.y, 0));
		transform.Rotate(new Vector3(90, 0, 180));

		// ターゲット生成
		var hit = PreLoad.Scripts.WeaponsDealer.ArrivalPosRayHit;
		_target = Instantiate(target, hit.point, target.transform.rotation, hit.transform);
		_target.transform.parent = hit.transform;
		_target.transform.LookAt(new Vector3(0, _target.transform.position.y, 0));

		pos = _target.transform.position;
		var angle = (Mathf.Abs(pos.x) < Mathf.Abs(pos.z)) ? 0 : 90;

		_target.transform.Rotate(new Vector3(90, 0, angle - Mathf.Atan(pos.x / pos.z) * 180 / Mathf.PI), Space.Self);


		// 音声再生オブジェクト生成
		_bulletSE.GetComponent<AudioSource>().clip = fireSE;
		bulletSE = Instantiate(_bulletSE, PreLoad.Scripts.SoundController.transform);
	}

	void FixedUpdate()
	{
		// 到達点と自分との距離を格納
		var vecDiff_magnitude = (arrivalPos - transform.position).magnitude;

		if (vecDiff_magnitude > 0.1f)
		{
			// 移動
			transform.position = Vector3.MoveTowards(transform.position, arrivalPos, _speed * Time.deltaTime);

		}
		else
		{
			// 破棄
			Destroy(_target.gameObject);
			GetComponent<CapsuleCollider>().enabled = false;
			Destroy(bulletSE, 3);
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, 3);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// ヒット音再生
		bulletSE.GetComponent<AudioSource>().PlayOneShot(hitSE);

		if (other.tag == "Enemy")
		{
			// エフェクト生成
			Destroy(Instantiate(effect, transform.position * 0.2f + other.transform.position * 0.8f, other.transform.rotation, PreLoad.Scripts.WeaponsDealer.transform), 1);

			// ソート
			var this_sr = GetComponent<SpriteRenderer>();
			var other_sr = other.GetComponent<SpriteRenderer>();
			this_sr.sortingOrder = other_sr.sortingOrder + 1;
		}
	}
}
