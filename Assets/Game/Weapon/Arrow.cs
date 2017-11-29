using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField]
	private float _speed = 1000;
	[SerializeField]
	private float _offsetY = -30;
	//[SerializeField]
	//private float emitDist;

	private Vector3 arrivalPos;

	[SerializeField]
	Vector3 rotate = new Vector3(90, 0, 180);

	[SerializeField]
	GameObject _target;
	GameObject target;

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

		transform.LookAt(new Vector3(arrivalPos.x, arrivalPos.y, arrivalPos.z));
		rotate = new Vector3(90, 0, 180);
		transform.Rotate(rotate);

		// ターゲット生成
		var hit = PreLoad.Scripts.WeaponsDealer.ArrivalPosRayHit;
		target = Instantiate(_target, hit.point, _target.transform.rotation, hit.transform);
		target.transform.parent = hit.transform;
		target.transform.LookAt(new Vector3(0, target.transform.position.y, 0));

		pos = target.transform.position;
		var angle = (Mathf.Abs(pos.x) < Mathf.Abs(pos.z)) ? 0 : 90;

		target.transform.Rotate(new Vector3(90, 0, angle -Mathf.Atan(pos.x / pos.z) * 180 / Mathf.PI), Space.Self);

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

			// 距離によって矢の向きを変えるかを判定
			if (vecDiff_magnitude > 100f)
			{
				transform.LookAt(new Vector3(arrivalPos.x, arrivalPos.y, arrivalPos.z));
				transform.Rotate(rotate);
			}
		}
		else
		{
			// 破棄
			Destroy(target);
			GetComponent<CapsuleCollider>().enabled = false;
			Destroy(bulletSE, 3);
			Destroy(gameObject, 3);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			// ヒット音再生
			bulletSE.GetComponent<AudioSource>().PlayOneShot(hitSE);

			// エフェクト生成
			Instantiate(effect, transform.position, effect.transform.rotation, PreLoad.Scripts.WeaponsDealer.transform);

			// ソート
			var this_sr = GetComponent<SpriteRenderer>();
			var other_sr = other.GetComponent<SpriteRenderer>();
			this_sr.sortingOrder = other_sr.sortingOrder;
		}
	}
}
