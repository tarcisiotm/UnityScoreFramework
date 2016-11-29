using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Score{
	
	[Serializable]
	public class Leaderboard {
		public string m_leaderboardName;
		public List<Score> m_scoreList;
	}

}