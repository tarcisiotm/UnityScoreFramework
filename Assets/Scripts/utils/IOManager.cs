using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

namespace Score
{
	public static class IOManager
	{
        
		static string m_pathToFile = "/Scores/";

		public static bool SaveFile (string fileName, string fileToSave, bool overwrite = true)
		{ 
			//output = obj.ToJsonPrettyPrintString ();
			//print ("output: " + fileToSave);
			string path = Application.persistentDataPath + m_pathToFile + fileName;

			try {
				//fileToSave = XOREncrypt.EncryptStringToBytes(fileToSave); //TODO SCRAMBLE this somehow

				using (StreamWriter fileWriter = overwrite ? File.CreateText (path) : File.AppendText (path)) {
					fileWriter.Write (fileToSave);
					return true;
				}
			} catch (System.Exception ex) {
				fileToSave = "Exceção de IO na escrita: " + ex.Message;
				return false;
			}
		}
        
		public static bool LoadFile (string nameOfFile, out Leaderboard leaderboard)
		{
			string output = String.Empty;
			leaderboard = new Leaderboard ();

			string fileName = Application.persistentDataPath + m_pathToFile + nameOfFile;
			try {
				using (StreamReader reader = new StreamReader (fileName)) {
					while (true) {
						string line = reader.ReadLine ();
						if (line == null) {
							break;
						}
						output += line;
					}

					Debug.Log ("?: " + output);
					//output = XOREncrypt.DecryptStringFromBytes(output);
					Debug.Log  ("Output: " + output);
					leaderboard = JsonUtility.FromJson<Leaderboard> (output);
					Debug.Log  ("Reading from file: " + output);
					Debug.Log("High: " + leaderboard.m_leaderboardName);
                    
					Debug.Log("High: " + leaderboard.HighestScore.GetIntScore());
                    
					return true;
				}
			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				Debug.Log  ("Deu merda");
				return false;
			}
		}

		#region Binary

		//private static readonly int CHUNK_SIZE = 1024;

		public static bool SaveBytesToFile (string fileName, string fileToSave, bool overwrite = true)
		{ 
			string path = Application.persistentDataPath + m_pathToFile + fileName;

			try {
				//fileToSave = XOREncrypt.EncryptStringToBytes(fileToSave); //TODO SCRAMBLE this with unique ID from device
				Debug.Log("Unique ID: "+SystemInfo.deviceUniqueIdentifier);
				byte[] encrypted = XOREncrypt.EncryptStringToBytes (fileToSave);
				using (BinaryWriter binaryWriter = new BinaryWriter (File.Open (path, FileMode.OpenOrCreate))) { //overwrite ? File.CreateText(path) : File.AppendText(path)
					//TODO write in chunks
					binaryWriter.Write (encrypted);
					return true;
				}
			} catch (System.Exception ex) {
				fileToSave = "Exceção de IO na escrita: " + ex.Message;
				return false;
			}
		}

		public static bool LoadBinaryFile (string nameOfFile, out Leaderboard leaderboard)
		{
			string output = String.Empty;
			leaderboard = new Leaderboard ();

			string fileName = Application.persistentDataPath + m_pathToFile + nameOfFile;

			//TODO check if file exists
			try {
				
				byte[] encrypted;
				encrypted = File.ReadAllBytes (fileName); //TODO Read in chunks

				output = XOREncrypt.DecryptStringFromBytes (encrypted);

				Debug.Log  ("Output: " + output);
				leaderboard = JsonUtility.FromJson<Leaderboard> (output);
				Debug.Log  ("Reading from file: " + output);
				Debug.Log("High: " + leaderboard.HighestScore.GetIntScore());
				return true;

			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				Debug.Log  ("Deu merda");
				return false;
			}
		}
		#endregion Binary

		public static bool FileExists (string name)
		{
			//"*.txt"
			System.IO.Directory.CreateDirectory ("" + Application.persistentDataPath + "/Scores");

			string[] files = Directory.GetFiles (Application.persistentDataPath + m_pathToFile);

			for (int i = 0; i < files.Length; i++) {
				//print (Path.GetFileName(files [i]) + "   " + name);
				if (Path.GetFileName (files [i]) == name) {
					return true;
				}
			}

			return false;
		}

		public static bool DeleteFile (string file, out string output)
		{
			output = "here";	
			string fileName = Application.persistentDataPath + "/" + file;
			try {
				//UnityEditor.FileUtil.DeleteFileOrDirectory(fileName);
				File.Delete (fileName);
				output += "here";
				return true;
			} catch (IOException ex) {
				output += ex.Message.ToString ();
				return false;
			}
		}

		public static void DeleteAllFiles ()
		{
			string[] files = Directory.GetFiles (Application.persistentDataPath + "/");
			string output = "";
			for (int i = 0; i < files.Length; i++) {
				DeleteFile (files [i], out output);
			}
		}
			
	}
}
