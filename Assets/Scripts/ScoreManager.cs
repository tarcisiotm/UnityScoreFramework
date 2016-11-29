using UnityEngine;
using System.Collections;

namespace Score{
	public class ScoreManager : MonoBehaviour {

		void Awake()
		{
			//scores.m_leaderboards = new System.Collections.Generic.List<Leaderboard> ();

			Leaderboard leaderboard = new Leaderboard ();

			Score score = new Score (5, "asd", "erd");
			Score score2 = new Score (52, "as222d", "er234234d");

			leaderboard.m_leaderboardName = "Name";

			leaderboard.m_scoreList = new System.Collections.Generic.List<Score> ();
			leaderboard.m_scoreList.Add (score);
			leaderboard.m_scoreList.Add (score2);

			//scores.m_leaderboards.Add (leaderboard);

			print (JsonUtility.ToJson (leaderboard));
			print (JsonUtility.ToJson (score));

		}

		// Use this for initialization
		void Start ()
		{
		
		}

		void AddScore(Score score)
		{
			
		}

		void LoadScore()
		{
			
		}

		Score[] GetScores()
		{
			return null;
		}

		Score GetHighestScore()
		{
			return null;
		}
	}
}
