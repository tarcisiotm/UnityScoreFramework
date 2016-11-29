using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace Score{
	public class IOManager : Singleton<IOManager> {

		readonly string m_pathToFile = "/Scores/";
	
		public bool SaveFile (string fileName, string fileToSave, bool overwrite = true)
		{ 
			//output = obj.ToJsonPrettyPrintString ();
			print ("output: " + fileToSave);
			string path = Application.persistentDataPath + m_pathToFile + fileName;

			try
			{
				using (StreamWriter fileWriter = overwrite ? File.CreateText(path) : File.AppendText(path))
				{
					fileWriter.Write (fileToSave);
					return true;
				}
			}
			catch (System.Exception ex)
			{
				fileToSave = "Exceção de IO na escrita: " + ex.Message;
				return false;
			}
		}

		public bool LoadFile (string nameOfFile, out Leaderboard leaderboard)
		{
			string output = String.Empty;
			leaderboard = new Leaderboard ();

			string fileName = Application.persistentDataPath + m_pathToFile + nameOfFile;
			try
			{
				using (StreamReader reader = new StreamReader (fileName)) {
					while (true) {
						string line = reader.ReadLine ();
						if (line == null) {
							break;
						}
						output += line;
					}
					leaderboard = JsonUtility.FromJson<Leaderboard>(output);
					print("Reading from file: "+output);
					return true;
				}
			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				return false;
			}
		}

		public bool FileExists(string name)
		{
			//"*.txt"
			System.IO.Directory.CreateDirectory ("" + Application.persistentDataPath + "/Scores");

			string[] files = Directory.GetFiles (Application.persistentDataPath + m_pathToFile);

			for (int i = 0; i < files.Length; i++)
			{
				print (Path.GetFileName(files [i]) + "   " + name);
				if (Path.GetFileName(files [i]) == name)
				{
					return true;
				}
			}

			return false;
		}

		public bool DeleteFile(string file, out string output)
		{
			output = "here";	
			string fileName = Application.persistentDataPath + "/" + file;
			try
			{
				//UnityEditor.FileUtil.DeleteFileOrDirectory(fileName);
				File.Delete(fileName);
				output += "here";
				return true;
			}
			catch (IOException ex)
			{
				output += ex.Message.ToString ();
				return false;
			}
		}

		public void DeleteAllFiles()
		{
			string[] files = Directory.GetFiles (Application.persistentDataPath + "/");
			string output = "";
			for (int i = 0; i < files.Length; i++)
			{
				DeleteFile (files [i],out output);
			}
		}
			
	}
}
