﻿using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public abstract class SICGameElement : MonoBehaviour {
		// Public Variables	
		[SerializeField] private float moveSpeed = 5f;

		// Private Variables
		protected Rigidbody2D rBody2D;
		protected Collider2D col2D;

		private float originalMoveSpeed;

		public abstract string OBJECT_ID { get; }

		public float MoveSpeed { get { return moveSpeed; } }

		public virtual void Awake() {
			rBody2D = GetComponent<Rigidbody2D>();
			col2D = GetComponent<Collider2D>();

			originalMoveSpeed = moveSpeed;
		}

		public virtual void OnEnable() {
			InitializeRBody();
		}

		public virtual void Start() { }

		public virtual void OnElementUpdate() { }

		public virtual bool OnElementConstraint() { return false; }

		public virtual void ResetElement() {
			moveSpeed = originalMoveSpeed;
		}

		public void Update() {
			if (!IsElementVisible())
				return;

			OnElementUpdate();

			if (OnElementConstraint()) {
				DisableElement();
			}
		}

		public bool IsElementVisible() {
			return !(transform.position.x < SICAreaBounds.MinExPosition.x || transform.position.x > SICAreaBounds.MaxExPosition.x) &&
				!(transform.position.y < SICAreaBounds.MinExPosition.y || transform.position.y > SICAreaBounds.MaxExPosition.y);
		}

		public void InitializeRBody() {
			if (rBody2D == null)
				return;

			rBody2D.fixedAngle = true;
			rBody2D.isKinematic = true;
		}

		public void AddSpeed(float spd) {
			moveSpeed += spd;
		}

		public void SubtractSpeed(float spd) {
			moveSpeed -= spd;
		}

		public void SetSpeed(float spd) {
			moveSpeed = spd;
		}

		public void EnableElement() {
			if (gameObject.activeInHierarchy)
				return;

			gameObject.SetActive(true);
		}

		public virtual void DisableElement() {
			if (!gameObject.activeInHierarchy)
				return;

			gameObject.SetActive(false);
		}

		public virtual void ShowExplosionFX() {
			GameObject explodeObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_PARTICLE, ParticleType.UNIT_EXPLOSION);
			if (explodeObj != null) {
				explodeObj.transform.position = transform.position;
				SICGameElement explode = explodeObj.GetComponent<SICGameElement>();
				explode.EnableElement();
			}
		}
	}
}