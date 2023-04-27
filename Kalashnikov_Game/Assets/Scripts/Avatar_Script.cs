using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar_Script : MonoBehaviour
{
    public string text;
    private int pointer = 0;
    private int relativelyPointer;
    public GameObject textObject;
    public GameObject spacePanel;
    private int NextWordLength(int position)
    {
        int length = 0;
        while (position < text.Length && text[position] != ' ')
        {
            position++;
            length++;
        }
        return length;
    }
    private IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(0.05f);
        
        textObject.GetComponent<TMPro.TextMeshProUGUI>().text += text[pointer];
        if (text[pointer] == ' ' && pointer < text.Length)
        {
            if(relativelyPointer + NextWordLength(pointer + 1) > 55)
            {
                spacePanel.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                spacePanel.SetActive(false);
                textObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                relativelyPointer = 0;
            }
        }
        pointer++;
        relativelyPointer++;
        if (pointer < text.Length)
            StartCoroutine(TextTimer());
        else
        {
            spacePanel.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            spacePanel.SetActive(false);
            textObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
            relativelyPointer = 0;
            gameObject.GetComponent<Animator>().Play("Avatar Close");
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
    }
    private IEnumerator StartTyping()
    {
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(TextTimer());
    }
    public void SetText(string newText)
    {
        text = newText;
        pointer = 0;
    }
    public void TextType()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().Play("Avatar Open");
        StartCoroutine(StartTyping());
    }
    private void Start()
    {
        textObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        spacePanel.SetActive(false);
    }
}
