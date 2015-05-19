using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICStage1 : SICGameStage {
		// Public Variables	

		// Private Variables	

		// Static Variables

		# region Game Element
		public override string OBJECT_ID {
			get { return SICObjectPoolName.STAGE_1; }
		}
		# endregion

		# region Stage

		public override STAGETYPE GetStageType() {
			return STAGETYPE.STAGE_1;
		}

		# endregion
	}
}