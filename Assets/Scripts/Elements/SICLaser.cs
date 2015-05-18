using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICLaser : SICGameElement {
		// Public Variables	
		[SerializeField] private float rayDuration = 0.2f;

		// Private Variables
		private float time;
		private Transform origin;

		// Static Variables

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_LASER; }
		}

		public void Initialize(Transform sender) {
			origin = sender;
			time = 0f;

			Vector3 scale = transform.localScale;
			scale.x = 0f;
			transform.localScale = scale;
		}

		public void Update() {
			// Distance from origin to the maximum end of the screen
			float laserLength = Mathf.Abs(SICAreaBounds.MaxPosition.x - origin.position.x) * 10f;
			time += Time.deltaTime;

			// Disable laser on completion
			//if ((transform.localScale.x / laserLength) >= 1) {
			//    time += Time.deltaTime;
			//    if (time > rayDuration) {
			//        DisableElement();
			//    }
			//}

			LaserDestroyElements();
			LaserMovement(laserLength);

# if UNITY_EDITOR
			Debug.DrawLine(origin.position, new Vector3(SICAreaBounds.MaxPosition.x, origin.position.y));
			Debug.DrawLine(origin.position, new Vector3(origin.position.x + (transform.localScale.x / 10f), origin.position.y), Color.red);
# endif
		}

		public void LaserMovement(float laserLength) {
			Vector3 scale = transform.localScale;
			scale.x = Mathf.Lerp(0f, laserLength, time * MoveSpeed);
			transform.localScale = scale;

			transform.position = origin.position;
			LaserConstraint();
		}

		public void LaserConstraint() {
			if (time > rayDuration) {
				DisableElement();
			}
		}

		public void LaserDestroyElements() {
			RaycastHit2D hit = Physics2D.Raycast(origin.position, Vector2.right, (transform.localScale.x / 10f), 1 << SICLayerManager.EnemyLayer);
			if (hit.transform != null) {
				SICGameElement element = hit.transform.GetComponent<SICGameElement>();
				if (element != null) {
					element.DisableElement();
				}
			}
		}
	}
}