using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FullSerializer;

public class WordChecker : MonoBehaviour
{
	public Dictionary<string, string[]> wordDict;

	// Use this for initialization
	void Start () 
	{
		// Set the `wordDict` property by getting a json string and deserializing it
		string json = getJSONString ();
		wordDict = Deserialize(typeof(Dictionary<string, string[]>),
		                              json);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Returns the JSON string within the wordlist_json file
	private string getJSONString ()
	{
		// Load the text of the json file as a string
		// (Note: You may have to change the string path to the location of the `wordlist_json.json` file)
		// For Unity, it should be in the Resources folder alongside this file
		TextAsset txt = Resources.Load ("Scripts/wordlist_json") as TextAsset;
		string jsonString = txt.text;	

		return jsonString;
	}

	// Deserializes a JSON string into a useable <string, string[]> dictionary type
	private Dictionary<string, string[]> Deserialize (Type type, string serializedState)
	{
		fsData data = fsJsonParser.Parse (serializedState);

		object deserialized = null;
		fsSerializer serializer = new fsSerializer ();
		serializer.TryDeserialize (data, type, ref deserialized);

		return deserialized as Dictionary<string, string[]>;
	}

	// Returns true if word exists within the `wordDict` dictionary
	public bool WordExists (string word)
	{
		// Determine what key should be used to search the `wordDict` (first letter + length of word)
		string key = word [0].ToString () + word.Length.ToString ();

		// Search for word using key
		string[] arr = wordDict [key];
		foreach (string w in arr) 
		{
			if (w == word)
				return true;
		}

		return false;
	}
}
