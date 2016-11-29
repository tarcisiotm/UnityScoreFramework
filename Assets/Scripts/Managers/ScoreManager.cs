using UnityEngine;
using System.Collections;
using System;

namespace Score{
	public class ScoreManager : Singleton<ScoreManager> {

		[Header("Options")]
		[Tooltip("Max number of scores to be saved.")]
		int m_maxNumberScore = 10;

		public override void Awake()
		{
			base.Awake ();
		}

		// Use this for initialization
		void Start ()
		{
			//Test2 ();
		}

		void AddScore(Leaderboard leaderboard, Score score)
		{
			if (leaderboard.HighestScore == null) {
				print ("Highscore is null");
			}
			if (leaderboard.HighestScore == null || score.GetScore() > leaderboard.HighestScore.GetScore())
			{
				print ("Setting highest score");
				leaderboard.SetHighScore (score);
			}

			if (leaderboard.m_scoreList.Count == m_maxNumberScore && m_maxNumberScore > 0)
			{
				Debug.Log ("Max size. Removing oldest entry.");
				leaderboard.m_scoreList.RemoveAt (0);
			}

			leaderboard.AddScore (score);
			SaveToDisk (leaderboard);
		}

		Leaderboard CreateLeaderboard(string name)
		{
			if(LeaderboardAlreadyExists(name))
			{
				Debug.LogWarning ("Trying to create a Leaderboard with a name that already exists!");
				return null;
			}

			Leaderboard leaderboard = new Leaderboard (name);
			//leaderboard.m_scoreList = new System.Collections.Generic.List<Score> ();
			SaveToDisk (leaderboard);

			return leaderboard;
		}

		#region IO

		bool LeaderboardAlreadyExists(string name)
		{
			return IOManager.Instance.FileExists (name);
		}

		bool SaveToDisk(Leaderboard leaderboard, bool overwrite = true)
		{
			string scoreJson = JsonUtility.ToJson (leaderboard);
			return IOManager.Instance.SaveFile (leaderboard.m_leaderboardName, scoreJson);
		}

		Leaderboard LoadLeaderboard(string name)
		{
			Leaderboard leaderboard = null;
			IOManager.Instance.LoadFile (name, out leaderboard);
			return leaderboard;
		}

		Leaderboard LoadOrCreateLeaderboard(string name)
		{
			if (LeaderboardAlreadyExists (name))
			{
				print ("Loading leaderboard");
				return LoadLeaderboard (name);
			}
			else
			{
				print ("Creating leaderboard");
				return CreateLeaderboard(name);
			}
		}

		#endregion IO

		#region Test
		void Test()
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
		#endregion Test

		void Test2()
		{
			Leaderboard leaderboard = LoadOrCreateLeaderboard ("Stage 1");

			Score score = new Score (65, "Stage 1 part 1", DateTime.Now.ToShortTimeString());
			Score score2 = new Score (21, "Stage 1 part 2", DateTime.Now.ToShortTimeString());

			AddScore (leaderboard, score);
			AddScore (leaderboard, score2);

			print (JsonUtility.ToJson (leaderboard));
		}

	}
}
