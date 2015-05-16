using UnityEngine;
using System.Collections;

namespace SpaceInvader.Utility {

	public class SICCamera : MonoBehaviour {
		[SerializeField]
		private float cameraWidth = 800f;

		[SerializeField]
		private float cameraHeight = 600f;

		[SerializeField]
		private int cameraPixelsPerUnit = 100;

		private Camera cam;

		private void Awake() {
			cam = GetComponent<Camera>();
		}

		private void Start() {
			if (cam != null) {
				if (!cam.orthographic) {
					cam.orthographic = true;
				}

				cam.orthographicSize = (cameraHeight / 2) / cameraPixelsPerUnit;
			}
		}

# if UNITY_EDITOR
		private void OnDrawGizmos() {
			Vector3 ptUpperRight = new Vector3((cameraWidth / 2) / cameraPixelsPerUnit, (cameraHeight / 2) / cameraPixelsPerUnit, 0.0f);
			Vector3 ptLowerRight = new Vector3((cameraWidth / 2) / cameraPixelsPerUnit, -(cameraHeight / 2) / cameraPixelsPerUnit, 0.0f);
			Vector3 ptUpperLeft = new Vector3(-(cameraWidth / 2) / cameraPixelsPerUnit, (cameraHeight / 2) / cameraPixelsPerUnit, 0.0f);
			Vector3 ptLowerLeft = new Vector3(-(cameraWidth / 2) / cameraPixelsPerUnit, -(cameraHeight / 2) / cameraPixelsPerUnit, 0.0f);

			Gizmos.DrawLine(ptUpperRight, ptLowerRight);
			Gizmos.DrawLine(ptLowerRight, ptLowerLeft);
			Gizmos.DrawLine(ptLowerLeft, ptUpperLeft);
			Gizmos.DrawLine(ptUpperLeft, ptUpperRight);
		}
# endif
	}
}