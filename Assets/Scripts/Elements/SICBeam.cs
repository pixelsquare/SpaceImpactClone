using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {
	public class SICBeam : SICGameElement {
		// Public Variables	
		[SerializeField] private float beamHeight = 50f;

		// Private Variables
		private Vector3 minPosition;
		private Vector3 maxPosition;

		// Static Variables

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_BEAM; }
		}

		public void Initialize(Transform sender) {
			transform.position = sender.position;

			Vector3 scale = transform.localScale;
			scale.y = beamHeight;
			transform.localScale = scale;

			minPosition = -Vector3.up * (beamHeight / 2) / 10;
			maxPosition = Vector3.up * (beamHeight / 2) / 10;
		}

		public void Update() {
			BeamMovement();	

			Debug.DrawLine(transform.position + minPosition, transform.position + maxPosition, Color.red);
		}

		public void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.layer == SICLayerManager.EnemyLayer) {
				SICGameElement element = col.GetComponent<SICGameElement>();
				if (element == null) {
					return;
				}

				element.DisableElement();
			}
		}

		private void BeamMovement() {
			transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
			BeamConstraint();
		}

		private void BeamConstraint() {
			if (transform.position.x > SICAreaBounds.MaxPosition.x) {
				DisableElement();
			}
		}
	}
}