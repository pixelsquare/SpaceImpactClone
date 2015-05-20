using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICCameraMover : MonoBehaviour {
		// Public Variables	
		[SerializeField] private float cameraSpeed = 5f;
		[SerializeField] private Transform from;
		[SerializeField] private Transform to;

		// Private Variables	
		private Vector3 fromLocation;
		private Vector3 toLocation;

		private float startTime;
		private float totalDistance;

		// Static Variables

		public Transform From { get { return from; } }
		public Transform To { get { return to; } }

		public void Initialize(Transform from, Transform to) {
			this.from = from;
			this.to = to;

			this.fromLocation = new Vector3(this.from.position.x, this.from.position.y, transform.position.z);
			this.toLocation = new Vector3(this.to.position.x, this.to.position.y, transform.position.z);

			startTime = Time.time;
			totalDistance = Vector3.Distance(fromLocation, toLocation);
		}

		public void Update() {
			if (from == null || to == null)
				return;

			float moveSpeed = (Time.time - startTime) * cameraSpeed;
			float moveTime = moveSpeed / totalDistance;
			transform.position = Vector3.Lerp(fromLocation, toLocation, moveTime);
		}

		public void ResetCameraMover() {
			transform.position = Vector3.zero;
			from = null;
			to = null;

			startTime = 0f;
			totalDistance = 0f;
		}

		public void SetCameraSpeed(float spd) {
			cameraSpeed = spd;
		}
	}
}