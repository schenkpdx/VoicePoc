using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Windows.Speech;

public class VoiceManager : MonoBehaviour
{

    delegate void KeywordAction(PhraseRecognizedEventArgs args);

    private Dictionary<string, KeywordAction> _keywordCollection;
    private KeywordRecognizer _keywordRecognizer;
    private GameObject _go;
    private TextMesh _tm;

    // Use this for initialization
    void Start()
    {
        _go = new GameObject("TextWriter");
        _tm = _go.AddComponent<TextMesh>();
        _tm.transform.position = Vector3.zero;

        _keywordCollection = new Dictionary<string, KeywordAction>();

        _keywordCollection.Add("Move Left", MoveLeftCommand);
        _keywordCollection.Add("Move Right", MoveRightCommand);
        _keywordCollection.Add("Move Down", MoveDownCommand);
        _keywordCollection.Add("Move Up", MoveUpCommand);

        _keywordRecognizer = new KeywordRecognizer(_keywordCollection.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        _keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction action;

        if (_keywordCollection.TryGetValue(args.text, out action))
        {
            action.Invoke(args);
        }
    }

    private void WriteText(string text)
    {
        //StringBuilder sb = new StringBuilder("Spoken: ");
        //sb.Append(text);

        _tm.text = text;
    }

    private void MoveLeftCommand(PhraseRecognizedEventArgs args)
    {
        WriteText(args.text);
    }

    private void MoveRightCommand(PhraseRecognizedEventArgs args)
    {
        WriteText(args.text);
    }

    private void MoveUpCommand(PhraseRecognizedEventArgs args)
    {
        WriteText(args.text);
    }

    private void MoveDownCommand(PhraseRecognizedEventArgs args)
    {
        WriteText(args.text);
    }
}
