using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICCameraMover : MonoBehaviour {
		// Public Variables	
		[SerializeField] private float cameraSpeed = 5f;
		[SerializeField] private Transform from;
		[SerializeField] private Transform to;

		// Private Variables	
		private float moveTime;
		private Vector3 fromLocation;
		private Vector3 toLocation;

		// Static Variables

		public Transform From { get { return from; } }
		public Transform To { get { return to; } }

		public void Initialize(Transform from, Transform to) {
			this.from = from;
			this.to = to;

			this.fromLocation = new Vector3(this.from.position.x, this.from.position.y, transform.position.z);
			this.toLocation = new Vector3(this.to.position.x, this.to.position.y, transform.position.z);

			moveTime = 0f;
		}

		public void Update() {
			if (from == null || to == null)
				return;

			moveTime += Time.deltaTime;
			transform.position = Vector3.Lerp(fromLocation, toLocation, moveTime * cameraSpeed);
		}

		public void SetCameraSpeed(float spd) {
			cameraSpeed = spd;
		}
	}
}