using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Ink.Runtime;

public class BasicInkExample : MonoBehaviour 
{
    public static event Action<Story> OnCreateStory;
    
    [SerializeField] private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField] private Canvas canvas = null;

    [SerializeField] TMP_Text narratorText;


    private void Awake()
    {
	    story = new Story (inkJSONAsset.text);
	    story.EvaluateFunction($"SetUserName({NarratorScript.m_userName})");
	    story.EvaluateFunction($"SetDeviceName({NarratorScript.m_deviceName})");
    }

    void StartStory ()
    {
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	void RefreshView () 
	{
		
		while (story.canContinue) {
			string text = story.Continue ();
			text = text.Trim();
			CreateContentView(text);
		}
    }

	void CreateContentView (string text) 
	{
		narratorText.SetText(text);
	}



	public void Play(string _option)
	{
		story.EvaluateFunction("Start", _option);
		story.EvaluateFunction($"SetTime({NarratorScript.m_time})");
		story.EvaluateFunction($"SetDate({NarratorScript.m_date})");
	}
}
