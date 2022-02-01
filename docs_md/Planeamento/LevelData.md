# LVLDAT

O LevelData irá criar um ficheiro lvldat.bruh no menu inicial pela primeira vez e depois irá verificar sempre se existe. 

## TODO
1. Limpar código antigo de MenuManager e de SaveSystem.
2. 

## Algoritmo

**Apanhar/Droppar**
- Quando o jogador apanhar um item irá verificar se esse foi pré-colocado, se sim: irá adicionar esse item ao levelData a dizer para não criar o item quando o nível for reiniciado (no script SpawnItemWorld, verifica antes de criar o item e se sim salta para destruir o gameobject, se não cria o item porque ainda não foi apanhado), se não: irá verificar os item e encontrar o equivalente e removê-lo de levelData. Quando uma entidade droppar um item, este será guardado no levelData.
- Definir um bool isPrePlaced que por default é falso. No script SpawnItemWorld, ao definir o item define isPrePlaced = true. Quando o jogador apanhar um item irá verificar se isPrePlaced = true, se sim: escrever no levelData com informação para remover o gameObject quando reiniciar o nível; se isPrePlaced = false, faz o normal...

---

**A última coisa que fiz**:
- enviar uma string para lvldat.bruh e tentar retirar ou modificar para outra string. ✓
|-> o código consegue ler a string no ficheiro após dar Deserialize.
- modificar a string e guardar no ficheiro lvldat.bruh.
|-> é aqui que deixa de funcionar, eu consigo ler o ficheiro, mas não consigo escrever de volta. Tentei ler a string de teste e funciona, mas ao tentar mudar a string irá simplesmente ignorar.



## Le Pain
```cs
using System.Collections;
using UnityEngine;

public class Player: MonoBehaviour
{
	public LevelData levelData;
	
	public int health;
	
	private void Start()
		health = 100;

	private void Update()
	{
		while (!levelData.IsDone)
			health -= 20;	//User takes damage
	}						//while lvldat isn't done.
}

```
#DungeonAhead! #Pain #Problem #Code	#Plan #TODO
