using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterWithHTMLStrings : MonoBehaviour
{
	const string initialString = "Leave this blank and it will pull up what is in the UI.Text field already.";
	[Tooltip(initialString)]
	[Multiline]
	public string originalString;

	public string cursor;

	public float interval;

	// If you prefer to use TMPro, then do two things:
	//	add using TMPro; at the top of this script
	//	replace this Text declaration with TextMeshProUGUI
	public TextMeshProUGUI output;

	void Reset()
	{
		print("reset text");
		originalString = initialString;
		cursor = "_";
		interval = 0.05f;
	}

	int GetPayloadLength( string s)
	{
		int count = 0;
		bool payload = true;

		for (int i = 0; i < s.Length; i++)
		{
			char c = s[i];   // one character

			if (payload)
			{
				if (c == '<')
				{
					payload = false;  // we just entered a formatting block
				}
				else
				{
					count++;   // tally payload character
				}
			}
			else
			{
				if (c == '>') payload = true;  // done with this formatting block, back to payload
			}
		}

		return count;
	}

	string  GetPartialPayload( string s, int typedSoFar)
	{
		int count = 0;
		bool payload = true;
		string tempString = "";

		for (int i = 0; i < s.Length; i++)
		{
			char c = s[i];   // one character

			if (payload)
			{
				if (c == '<')
				{
					tempString = tempString + c.ToString();
					payload = false;  // we just entered a formatting block
				}
				else
				{
					if (count < typedSoFar) tempString = tempString + c.ToString();
					count++;   // tally payload character
				}
			}
			else
			{
				tempString = tempString + c.ToString();
				if (c == '>')
				{
					payload = true;  // done with this formatting block, back to payload
				}
			}
		}

		return tempString;
	}

	IEnumerator Start ()
	{
		// if blank, pick up what's in the UI.Text field to start with
		if (string.IsNullOrEmpty(originalString))
		{
			originalString = output.text;
		}

		int i = 0;
		while( true)
		{
			output.text = GetPartialPayload( originalString, i) + cursor;

			yield return new WaitForSeconds( interval);

			i++;

			if (i > GetPayloadLength(originalString))
			{
				break;
			}
		}

		// remove cursor
		output.text = originalString;
	}
}
