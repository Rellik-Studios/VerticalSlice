using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Himanshu
{
    public class Narrator : MonoBehaviour
    {
        
        [SerializeField] private TMP_Text m_textBox;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_idleRoom;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_ballRoom;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_cafeteria;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_maze;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_bathroom;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_mirrorShatter;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_hospital1950;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_hospital1870;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_hospitalFuturistic;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_hospitalPresent;

        [TextArea(4, 6)]
        [SerializeField] private List<string> m_hub1;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_hub2;
        
        [TextArea(4, 6)]
        [SerializeField] private List<string> m_finish1;

        private bool m_settingText = false;
        public void Play(List<string> _toPlay)
        {
            if (_toPlay.Count > 1 && !m_settingText)
            {
                int rand = Random.Range(1, _toPlay.Count);
                StartCoroutine(SetText(_toPlay[rand], m_textBox));
                _toPlay.RemoveAt(rand);
            }
            else if (!m_settingText)
            {
                StartCoroutine(SetText(_toPlay[0], m_textBox));
            }
        }

        IEnumerator SetText(string _text, TMP_Text _textBox, bool additive = false)
        {
            m_settingText = true;
            if(!additive) _textBox.text = "";
            bool commandStart = false;
            bool choiceStart = false;
            bool choiceEnd = false;
            string command = "";
            int pos = 0;
            List<string> choices = new List<string>();
            string currentChoice = "";
            foreach (var letter in _text)
            {
                pos++;
                if (letter == '#')
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    StartCoroutine(SetText(_text.Substring(pos + 1), _textBox));
                    yield break;
                }

                if (choiceStart)
                {
                    if (letter == '|')
                    {
                        choices.Add(currentChoice);
                        currentChoice = "";
                        continue;
                    }
                    else if (letter == '}')
                    {
                        choices.Add(currentChoice);
                        choiceEnd = true;
                        choiceStart = false;
                    }
                    else
                    {
                        currentChoice += letter;
                        continue;
                    }
                }

                if (choiceEnd)
                {
                    StartCoroutine(SetText(choices.Random(), _textBox, true));
                    yield return new WaitWhile(() => m_settingText);
                    m_settingText = true;
                    choiceEnd = false;
                    continue;
                }

                if (letter == '{')
                {
                    choiceStart = true;
                    continue;
                }
                if (commandStart)
                {
                    if (letter == ' ' || letter == '?' || letter == ',')
                    {
                        StartCoroutine(SetText(EvaluateCommand(command), _textBox, true));
                        yield return new WaitWhile(() => m_settingText);
                        m_settingText = true;
                        //_textBox.text += EvaluateCommand(command);
                        commandStart = false;
                    }
                    else
                    {
                        command += letter;
                        continue;
                    }
                    
                }
                if (letter == '@')
                {
                    commandStart = true;
                    command = "";
                }
                else if (letter == ' ')
                {
                    _textBox.text += letter;
                    yield return new WaitForSeconds(0.05f);
                }
                else
                {
                    _textBox.text += letter;
                    yield return null;
                }
            }

            m_settingText = false;
        }

        private string EvaluateCommand(string _command)
        {
            switch (_command)
            {
                case "userName":
                    return NarratorScript.m_userName;
                case "timeCategory":
                    return NarratorScript.timeCategory;
                case "day":
                    return NarratorScript.m_weekDay;
                case "time":
                    return NarratorScript.m_time;
                case "activity":
                    return NarratorScript.activity;
                default:
                    return "Not Set Yet";
            }
            return "";
        }

        public void PlayHub()
        {
            Play(m_idleRoom);
        }
    }
}