using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICLayerManager {
		private const string DEFAUILT_LAYER = "Default";
		public static int DefaultLayer {
			get { return LayerMask.NameToLayer(DEFAUILT_LAYER); }
		}

		private const string SHIP_LAYER = "Ship";
		public static int ShipLayer {
			get { return LayerMask.NameToLayer(SHIP_LAYER); }
		}

		private const string ENEMY_LAYER = "Enemy";
		public static int EnemyLayer {
			get { return LayerMask.NameToLayer(ENEMY_LAYER); }
		}

		private const string MISSILE_LAYER = "Missile";
		public static int MissileLayer {
			get { return LayerMask.NameToLayer(MISSILE_LAYER); }
		}
	}
}