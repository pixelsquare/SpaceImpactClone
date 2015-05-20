using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;
using System.Collections.Generic;

namespace SpaceImpact.GameCore {

	public class SICGameManager : MonoBehaviour {
		// Public Variables	
		[SerializeField] private SICCameraMover cameraMover;
		[SerializeField] private SICGameMetrics gameMetrics;
		[SerializeField] private StageType stage;
		[SerializeField] private List<SICGameStage> refStages;

		// Private Variables	
		private GameObject spaceShipObj;
		private SICSpaceShip spaceShip;

		private GameObject stageObj;
		private SICGameStage curStage;
		private Vector3 shipInitPos;

		// Static Variables
		private static SICGameManager instance;

		public static SICGameManager SharedInstance { get { return instance; } }

		public SICSpaceShip SpaceShip { get { return spaceShip; } }

		public SICCameraMover CameraMover { get { return cameraMover; } } 

		public SICGameMetrics GameMetrics { get { return gameMetrics; } }

		public List<SICGameStage> RefStages { get { return refStages; } }

		public SICGameStage CurrentStage { get { return curStage; } } 

		private void Awake() {
			instance = this;
		}

		private void Start() {
			gameMetrics.SetLife(SICGameSettings.DEFAULT_LIVES);
			gameMetrics.SetScore(SICGameSettings.DEFAULT_SCORE);
			gameMetrics.SetSpecial(SICGameSettings.DEFAULT_SPECIAL);

			SetStage(StageType.STAGE_1);
		}

//# if UNITY_EDITOR
		public void Update() {
			//if (Input.GetKeyDown(KeyCode.L)) {
			//    foreach (SICObjectPoolManager.ObjectPooled obj in SICObjectPoolManager.SharedInstance.GetParents(SICObjectPoolName.OBJECT_GAME_STAGE)) {
			//        Debug.Log(obj.refObjParent.name);
			//    }
			//}

			//if (Input.GetKeyDown(KeyCode.N)) {
			//    ReloadCurrentStage();
			//}

			if (gameMetrics.GetLives() <= 0) {
				SetStage(StageType.NONE);
			}

			LevelsTest();
		}
//# endif

		public void LoadStage(StageType stageType) {
			if (curStage != null) {
				curStage.DisableElement();
			}

			stageObj = SICObjectPoolManager.SharedInstance.GetObject(GetStage(stageType).OBJECT_ID, stageType);
			if (stageObj != null) {
				curStage = stageObj.GetComponent<SICGameStage>();
				curStage.EnableElement();

				cameraMover.Initialize(curStage.PointA, curStage.PointB);
			}

			shipInitPos = curStage.PStartPosition.position;
			InitializeSpaceShip();
			ResetSpaceShip();
		}

		public void ResetStage() {
			curStage.ResetStage();

			ResetSpaceShip();
			cameraMover.ResetCameraMover();
		}

		public void ClearStage() {
			spaceShip.DisableElement();
			curStage.DisableElement();
			cameraMover.ResetCameraMover();
			SICObjectPoolManager.SharedInstance.ResetAllParents();
		}

		public void ReloadCurrentStage() {
			ResetStage();
			LoadStage(stage);
		}

		public void InitializeSpaceShip() {
			if (spaceShipObj != null && spaceShipObj.activeInHierarchy)
				return;

			spaceShipObj = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_SPACE_SHIP);
		}

		public void ResetSpaceShip() {
			if (spaceShipObj != null && spaceShip == null) {
				spaceShip = spaceShipObj.GetComponent<SICSpaceShip>();
			}


			spaceShip.SetSpecial(ProjectileType.NONE);
			spaceShip.SetSpecial(SICGameSettings.DEFAULT_SPECIAL);
			spaceShip.SetHP(gameMetrics.GetLives());
			spaceShip.SetPosition(shipInitPos);
			spaceShip.EnableElement();
		}

		public SICGameStage GetStage(StageType stageType) {
			return refStages.Find(a => a.GetStageType() == stageType);
		}

		public void SetStage(StageType stageType) {
			if (stage == stageType)
				return;

			stage = stageType;

			if (stage == StageType.NONE) {
				ClearStage();
				return;
			}

			LoadStage(stage);
		}

		public void LevelsTest() {
			if (Input.GetKeyDown(KeyCode.F1)) {
				SetStage(StageType.STAGE_1);
			}

			if (Input.GetKeyDown(KeyCode.F2)) {
				SetStage(StageType.STAGE_2);
			}

			if (Input.GetKeyDown(KeyCode.F3)) {
				SetStage(StageType.STAGE_3);
			}

			if (Input.GetKeyDown(KeyCode.F4)) {
				SetStage(StageType.STAGE_4);
			}

			if (Input.GetKeyDown(KeyCode.F5)) {
				SetStage(StageType.STAGE_5);
			}

			if (Input.GetKeyDown(KeyCode.Home)) {
				SetStage(StageType.NONE);
			}
		}
	}
}