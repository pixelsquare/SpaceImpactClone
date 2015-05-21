using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	[System.Serializable]
	public class SICElementMover {
		// Public Variables	
		[SerializeField] private bool isLooping;
		[SerializeField] private float speed = 0.1f;
		[SerializeField] private float distanceThreshold = 0.01f;
		[SerializeField] private float startDelay;
		[SerializeField] private Vector3[] waypoints;

		// Private Variables	
		private int curNode;

		private float startTime;
		private float totalDistance;
		private bool isFinished;

		private float time;

		private Transform owner;

		// Static Variables

		public bool IsFinished { get { return isFinished; } }

		public Vector3[] Path { get { return waypoints; } }

		public void Initialize(Transform owner) {
			this.owner = owner;
			curNode = 0;
			startTime = Time.time;
			isFinished = false;
			time = 0f;
			totalDistance = Vector3.Distance(owner.position, waypoints[curNode]);
		}

		public void UpdateMove() {
			if (time < startDelay) {
				time += Time.deltaTime;
				return;
			}

			if (isFinished)
				return;

			if (Vector3.Distance(owner.position, waypoints[curNode]) <= distanceThreshold) {
				curNode++;

				if (isLooping && curNode > waypoints.Length - 1) {
					curNode = 0;
				}

				totalDistance = Vector3.Distance(owner.position, waypoints[curNode]);
				startTime = Time.time;
			}

			float moveSpeed = (Time.time - startTime) * speed;
			float moveTime = moveSpeed / totalDistance;
			owner.position = Vector3.Lerp(owner.position, waypoints[curNode], moveTime);

			if (!isLooping && Vector3.Distance(owner.position, waypoints[curNode]) <= distanceThreshold)
				isFinished = true;
		}

		public void SetPath(Vector3[] path) {
			this.waypoints = path;
		}

		public void SetSpeed(float spd) {
			this.speed = spd;
		}

		public void DrawGizmos() {
			if (waypoints == null || waypoints.Length <= 0)
				return;

			for (int i = 0; i < waypoints.Length; i++) {
				if (i < waypoints.Length - 1) {
					Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
				}

				Gizmos.DrawLine(waypoints[waypoints.Length - 1], waypoints[0]);
			}
		}
	}
}