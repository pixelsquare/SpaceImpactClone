using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICMissile : SICGameElement {
		// Public Variables	

		// Private Variables	

		// Static Variables

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_MISSILE; }
		}

		public void Initialize(Transform sender) {
			transform.position = sender.position;
		}

		public void Update() {
			MissileMovement();
		}

		public void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.layer == SICLayerManager.EnemyLayer) {
				SICGameElement element = col.GetComponent<SICGameElement>();
				if (element == null) {
					return;
				}

				element.DisableElement();
				DisableElement();
			}
		}

		private void MissileMovement() {
			transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
			MissileConstraint();
		}

		private void MissileConstraint() {
			if (transform.position.x > SICAreaBounds.MaxPosition.x) {
				DisableElement();
			}
		}
	}
}