using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

namespace Score
{
	public class IOManager : Singleton<IOManager>
	{

		readonly string m_pathToFile = "/Scores/";

		public bool SaveFile (string fileName, string fileToSave, bool overwrite = true)
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

		public bool LoadFile (string nameOfFile, out Leaderboard leaderboard)
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

					print ("?: " + output);
					//output = XOREncrypt.DecryptStringFromBytes(output);
					print ("Output: " + output);
					leaderboard = JsonUtility.FromJson<Leaderboard> (output);
					print ("Reading from file: " + output);
					return true;
				}
			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				print ("Deu merda");
				return false;
			}
		}

		#region Binary

		//private static readonly int CHUNK_SIZE = 1024;

		public bool SaveBytesToFile (string fileName, string fileToSave, bool overwrite = true)
		{ 
			string path = Application.persistentDataPath + m_pathToFile + fileName;

			try {
				//fileToSave = XOREncrypt.EncryptStringToBytes(fileToSave); //TODO SCRAMBLE this with unique ID from device
				print("Unique ID: "+SystemInfo.deviceUniqueIdentifier);
				byte[] encrypted = XOREncrypt.EncryptStringToBytes (fileToSave);

				//int numChunks = 1024;
				//int chunkSize = (encrypted.Length) / numChunks;

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

		public bool LoadBinaryFile (string nameOfFile, out Leaderboard leaderboard)
		{
			string output = String.Empty;
			leaderboard = new Leaderboard ();

			string fileName = Application.persistentDataPath + m_pathToFile + nameOfFile;

			//TODO check if file exists
			try {
				
				byte[] encrypted;
				encrypted = File.ReadAllBytes (fileName); //TODO Read in chunks

				const int chunkSize = 1024; // read the file by chunks of 1KB

				using (var file = File.OpenRead(fileName))
				{
					//int bytesRead;
					var buffer = new byte[chunkSize];
					while ((file.Read(buffer, 0, buffer.Length)) > 0) //bytesread = 
					{
						
					}
				}

				const int bufferSize = 4096;
			
				output = XOREncrypt.DecryptStringFromBytes (encrypted);

				print ("Output: " + output);
				leaderboard = JsonUtility.FromJson<Leaderboard> (output);
				print ("Reading from file: " + output);
				return true;

			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				print ("Deu merda");
				return false;
			}
		}
		#endregion Binary

		public bool FileExists (string name)
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

		public bool DeleteFile (string file, out string output)
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

		public void DeleteAllFiles ()
		{
			string[] files = Directory.GetFiles (Application.persistentDataPath + "/");
			string output = "";
			for (int i = 0; i < files.Length; i++) {
				DeleteFile (files [i], out output);
			}
		}
			
	}
}
