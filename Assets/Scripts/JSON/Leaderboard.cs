using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Score{
	
	[Serializable]
	public class Leaderboard {
		
		public string m_leaderboardName; //Example: Stage 1

		[SerializeField]
		Score m_highestScore = null;
		public Score HighestScore {get {return m_highestScore;}}

		//TODO change to stack and see if it is supported
		public List<Score> m_scoreList;

		public Leaderboard()
		{
		}

		public Leaderboard(string name)
		{
			m_leaderboardName = name;
			m_scoreList = new List<Score> ();
			m_highestScore = null;
		}

		public void AddScore(Score score)
		{
			m_scoreList.Add (score);
		}

		public void SetHighScore(Score score)
		{
			//print ("Inside set score");
			m_highestScore = score;
		}

	}

}