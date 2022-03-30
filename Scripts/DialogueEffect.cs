using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEffect : MonoBehaviour
{
    
    public float textDelay = 10f;
    [TextArea(14, 10)]  public string fullText;
     private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
        
    }
    IEnumerator ShowText()
    {

        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i<fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(textDelay);
        }

        for (int n = 0; n < 1000; n++)
        {
            this.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(0.05f);
            this.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(0.05f);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(3.0f);
        }
    }


}
