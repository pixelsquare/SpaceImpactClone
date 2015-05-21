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

		// Private Variables
		private List<Vector3> elementPos;
		private List<SICGameElement> stageElements;

		private SICGameBoss stageBoss;

		// Static Variables

		private const string POINT_A_NAME = "Point A";
		private const string POINT_B_NAME = "Point B";

		public SICWaypoint Waypoints { get { return waypoints; } }

		public Transform PStartPosition { get { return pStartPosition; } }

		public List<SICGameElement> StageElements { get { return stageElements; } }

		public SICGameBoss StageBoss { get { return stageBoss; } }

		public Transform PointA { get { return waypoints.PointA; } }

		public Transform PointB { get { return waypoints.PointB; } } 

		# region Game Element
		public override void Awake() {
			base.Awake();
			elementPos = new List<Vector3>();

			stageElements = new List<SICGameElement>();
			SICGameUtility.GetElementsRecursively(transform, ref stageElements);

			if (stageElements.Count <= 0)
				return;

			List<SICGameEnemy> enemyList = new List<SICGameEnemy>();
			SICGameUtility.GetEnemyRecursively(transform, ref enemyList);

			stageBoss = enemyList.Find(a => a.GetEnemyType() == EnemyType.BOSS) as SICGameBoss;

			for (int i = 0; i < stageElements.Count; i++) {
				elementPos.Add(stageElements[i].transform.position);
			}
		}

		public override void OnEnable() {
			base.OnEnable();
			ResetStage();
			AddElementsToPool();
		}

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_GAME_STAGE;  }
		}
		# endregion

		public void AddElementsToPool() {
			if (stageElements == null || stageElements.Count <= 0)
				return;

			for (int i = 0; i < stageElements.Count; i++) {
				for (int j = 0; j < SICObjectPoolManager.SharedInstance.ObjParents.Count; j++) {
					if (!stageElements[i].IsElementActive)
						continue;

					if (stageElements[i].OBJECT_ID == SICObjectPoolManager.SharedInstance.ObjParents[j].refObj.OBJECT_ID) {
						SICObjectPoolManager.SharedInstance.ObjParents[j].AddObject(stageElements[i].gameObject, false);
					}
				}
			}
		}

		public void ResetStage() {
			SICGameUtility.SetAllElementsRecursively(transform, true);

			if (stageElements.Count <= 0)
				return;

			for (int i = 0; i < stageElements.Count; i++) {
				stageElements[i].transform.position = elementPos[i];
			}
		}

		public virtual StageType GetStageType() {
			return StageType.NONE;
		}
	}
}