using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Score{
	
	[Serializable]
	public class Leaderboard{
		
		public string m_leaderboardName; //Example: Stage 1

		//Set it to false for smaller times, etc
		[SerializeField]
		bool m_biggerIsHigher = true;

		[SerializeField]
		Score m_highestScore = null;

		[SerializeField]
		int m_maxNumberScores = 10;

		public Score HighestScore {get {return m_highestScore;}}

		//TODO change to stack and see if it is supported
		public List<Score> m_scoreList;

		public Leaderboard(string name = "", int maxNumberOfScores = 10, bool higherIsBigger = true)
		{
			m_leaderboardName = name;
			m_scoreList = new List<Score> ();
			m_highestScore = null;
		}

		public void AddScore(Score score, out string output)
		{
			output = String.Empty;

			CheckHighScore (score);

			if (m_scoreList.Count == m_maxNumberScores && m_maxNumberScores > 0)
			{
				output += "  Max size. Removing oldest entry.";
				m_scoreList.RemoveAt (0);
			}

			m_scoreList.Add (score);
		}

		public void CheckHighScore(Score score)
		{
			if( (m_highestScore == null) ||
				(score.GetScore() > m_highestScore.GetScore() && m_biggerIsHigher) ||
				(score.GetScore() < m_highestScore.GetScore() && !m_biggerIsHigher))
			{
				m_highestScore = score;
			}
		
		}

	}

}