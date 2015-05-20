using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using System.Collections.Generic;

namespace SpaceImpact {

	public enum StageType {
		NONE = 0,
		STAGE_1 = 1,
		STAGE_2 = 2,
		STAGE_3 = 3,
		STAGE_4 = 4,
		STAGE_5 = 5
	}

	public class SICGameStage : SICGameElement {
		// Public Variables	
		[SerializeField] private SICWaypoint waypoints;
		[SerializeField] private Transform pStartPosition;
		[SerializeField] private Transform[] elements;

		// Private Variables
		private List<Vector3> elementPos;

		// Static Variables

		private const string POINT_A_NAME = "Point A";
		private const string POINT_B_NAME = "Point B";

		public SICWaypoint Waypoints { get { return waypoints; } }

		public Transform PStartPosition { get { return pStartPosition; } }

		public Transform[] Elements { get { return elements; } }

		public Transform PointA { get { return waypoints.PointA; } }

		public Transform PointB { get { return waypoints.PointB; } } 

		# region Game Element
		public override void Awake() {
			base.Awake();
			elementPos = new List<Vector3>();

			if (elements.Length <= 0)
				return;

			for (int i = 0; i < elements.Length; i++) {
				elementPos.Add(elements[i].transform.position);
			}
		}

		public override void OnEnable() {
			base.OnEnable();
			ResetStage();
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_GAME_STAGE;  }
		}
		# endregion

		public void ResetStage() {
			SICGameUtility.SetAllElementsRecursively(transform, true);

			if (elements.Length <= 0)
				return;

			for (int i = 0; i < elements.Length; i++) {
				elements[i].transform.position = elementPos[i];
			}
		}

		public virtual StageType GetStageType() {
			return StageType.NONE;
		}
	}
}