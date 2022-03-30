using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/* make it serializable to show up in inspector */

public class Dialogue
{

	[TextArea(3, 10)]
    /* 3 min. lines and 10 max lines for text */

	public string[] sentences;

}