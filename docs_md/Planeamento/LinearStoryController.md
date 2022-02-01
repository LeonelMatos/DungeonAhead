# LinearStoryController
## Introdução

O LSC é uma sequência ordenada de comandos personalizados que definem uma ordem de eventos que irão auxiliar o desenvolvimento da história, num ambiente linear e pré-definido.

**Exemplo de Lista**
```cs
1. ChangeVar("speed", 1f);		//Allows to change the speed of the character
2. MoveCharacter(player, pos1);	//Moves the player at 1f speed (speed will reset)
3. WaitForSeconds(5f);
4. MoveCamera(character2);		//Maybe use gameobjects for the camera to follow
5. TurnCharacter(character2);	//Inverts the character's sprite
6. StartDialogue(character2);	//GetComponent<DialogueTrigger>()
7. WaitForDialogue(character2)	//while(!dialogue.IsDone)
8. MoveCamera(player);
9. CharacterJump(character2);	//Makes the character jump
10. MoveCharacter(character3, pos2);

```