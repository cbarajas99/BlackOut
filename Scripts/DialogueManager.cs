using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		Debug.Log("Starting dialogue...");

		sentences.Clear();
        /* clears text on screen*/

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		/* typewriter effect on text */
		char[] array = sentence.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			char letter = array[i];
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		Debug.Log("Dialogue is done...");

		animator.SetBool("IsOpen", false);

       
	}

}