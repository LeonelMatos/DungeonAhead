using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// \brief Template of dialogue shown in-game.
/// Used in-editor to write a speech
/** \bug Currently works as monologue-style where it 
only allows one person to talk per dialogue.
Workaround: to have multiple characters interact, use multiple
Dialogue elements per character, per monologue.
*/
[System.Serializable]
public class Dialogue
{
    /// \brief Forces the Manager to skip this dialogue as
    /// it was already shown when true.
    public bool isDone;

    /// Name of the character speaking.
    public string name;

    /// Array of sentences stored as strings.
    [TextArea(4, 10)]
    public string[] sentences;


}

//Hello World!
