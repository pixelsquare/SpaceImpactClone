using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICSpaceShip : SICGameElement {
		// Public Variables	
		[SerializeField] private Transform shipNozzle;

		// Private Variables
		private MoveDirection moveDirection;

		// Static Variables

		public enum MoveDirection {
			VERTICAL = 0,
			HORIZONTAL = 1
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_SPACE_SHIP; }
		}

		public void Update() {
			ShipMovement();

			if (Input.GetButtonDown("Fire")) {
				FireMissile();
			}

			//if (Input.GetButtonDown("Fire2")) {
			//    foreach (GameObject obj in SICGameUtility.GetNearestEnemies(transform.position)) {
			//        Debug.Log(obj.name);
			//    }
			//}

			ProjectileTest();
		}

		private void ShipMovement() {
			if (Input.GetButtonDown("Vertical")) {
				moveDirection = MoveDirection.VERTICAL;
			}

			if (Input.GetButtonDown("Horizontal")) {
				moveDirection = MoveDirection.HORIZONTAL;
			}

			if (moveDirection == MoveDirection.VERTICAL) {
				transform.Translate(Vector3.up * Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);
			}
			else if (moveDirection == MoveDirection.HORIZONTAL) {
				transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime);
			}

			ShipConstraint();
		}

		private void ShipConstraint() {
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, SICAreaBounds.MinPosition.x + (mainTexture.bounds.size.x / 2), SICAreaBounds.MaxPosition.x - (mainTexture.bounds.size.x / 2));
			pos.y = Mathf.Clamp(pos.y, SICAreaBounds.MinPosition.y + (mainTexture.bounds.size.y / 2), SICAreaBounds.MaxPosition.y - (mainTexture.bounds.size.y / 2));
			transform.position = pos;
		}

		private void FireMissile() {
			GameObject missileObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_MISSILE);
			if (missileObj != null) {
				SICMissile missle = missileObj.GetComponent<SICMissile>();
				missle.Initialize(shipNozzle);
				missle.EnableElement();
			}
		}

		private void FireLaser() {
			GameObject laserObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_LASER);
			if (laserObj != null) {
				SICLaser laser = laserObj.GetComponent<SICLaser>();
				laser.Initialize(shipNozzle);
				laser.EnableElement();
			}
		}

		private void FireBeam() {
			GameObject beamObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_BEAM);
			if (beamObj != null) {
				SICBeam beam = beamObj.GetComponent<SICBeam>();
				beam.Initialize(shipNozzle);
				beam.EnableElement();
			}
		}

		private void FireRocket() {
			GameObject rocketObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_ROCKET);
			if (rocketObj != null) {
				SICRocket rocket = rocketObj.GetComponent<SICRocket>();
				rocket.Initialize(shipNozzle);
				rocket.EnableElement();
			}
		}

		private void ProjectileTest() {
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				FireLaser();
			}

			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				FireBeam();
			}

			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				FireRocket();
			}
		}

		public override void DisableElement() {
			SpawnExplotion();
			base.DisableElement();
		}
	}
}