using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.ToneAnalyzer.V3;
using IBM.Watson.ToneAnalyzer.V3.Model;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;



public class WatsonToneAnalyzer : MonoBehaviour
{
    public Text ResponseTextField;

    private readonly string iamApikey = "9Ebz8J0vvyq0vls983LRl4jvBxhfExBE_6_innY35VHi";
    private readonly string serviceUrl = "https://api.eu-gb.tone-analyzer.watson.cloud.ibm.com/instances/19071bda-51c1-444d-a550-48f5ffc17c41";
    private readonly string versionDate = "2020-06-01";

    private bool toneTested = false;

    private ToneAnalyzerService service;

    [SerializeField]
    private SongSelector songSelector;

    void Start()
    {
        //Installing default Log reactors so we could see the Watson logs
        LogSystem.InstallDefaultReactors();

        //Run as a coroutine because we have to wait for the authorisation
        //The execution of a coroutine can be paused at any point using the yield statement. 
        //When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame.
        Runnable.Run(CreateService()); 
    }

    private IEnumerator CreateService()
    {
        //  Create credential and instantiate service
        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

        //  Wait for tokendata
        while (!authenticator.CanAuthenticate())
            yield return null;

        service = new ToneAnalyzerService(versionDate, authenticator);
        service.SetServiceUrl(serviceUrl);
    }

    public IEnumerator AnalyseTone(string inputText)
    {
        Debug.Log("ToneAnalyzer: AnalyseTone --- Starting");

        ToneInput toneInput = new ToneInput()
        {
            Text = inputText
        };

        // It detects three types of tones, including: 
        //      Emotion (anger, disgust, fear, joy and sadness), "emotion"
        //      Social propensities (openness, conscientiousness, extroversion, agreeableness, and emotional range), "language"
        //      Language styles (analytical, confident and tentative), "social"
        //For now we only need emotion
        List<string> tones = new List<string>()
        {
            "emotion"
        };

        toneTested = false;

        service.Tone(callback: OnTone, toneInput: toneInput, sentences: false, contentType: "application/json");

        while (!toneTested)
        {
            yield return null;
        }

        Debug.Log("ToneAnalyzer: AnalyseTone --- Analysis complete");
    }

    private void OnTone(DetailedResponse<ToneAnalysis> response, IBMError error)
    {
        if (error != null)
        {
            Log.Debug("ExampleToneAnalyzerV3.OnTone()", "Error: {0}: {1}", error.StatusCode, error.ErrorMessage);
        }
        else
        {
            string finalText = "";
            response.Result.DocumentTone.Tones.ForEach(delegate (ToneScore tone)
            {
                finalText += (tone.ToneName + " ");
                if(tone.Score is double value)
                {
                    finalText += (value + System.Environment.NewLine);
                }
                else
                {
                    finalText += ("null" + System.Environment.NewLine);
                }
            });

            ResponseTextField.text = finalText;

            //Send result to the SongSelector
            songSelector.FindMatch(response.Result.DocumentTone.Tones);
        }

        toneTested = true;
    }
}
