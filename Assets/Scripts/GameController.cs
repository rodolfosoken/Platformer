using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private int[][] level = new int[][]
{
new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
new int[]{1, 0, 3, 3, 0, 0, 0, 0, 3, 0, 3, 3, 0, 3, 3, 0, 0, 4, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
new int[]{1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
new int[]{1, 0, 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
new int[]{1, 0, 0, 0, 3, 3, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1, 1},
new int[]{1, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1, 1, 1},
new int[]{1, 0, 0, 0, 3, 3, 0, 3, 3, 0, 0, 0, 0, 0, 3, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 3, 3, 0, 0, 0, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
new int[]{1, 0, 0, 0, 1, 1, 3, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
new int[]{1, 2, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
};
    public Transform wall;
	public Transform player;
	public Transform orb;
	public Transform goal;
	public ParticleSystem goalPS;

	public static GameController _instance;
	private int orbsCollected;
	private int orbsTotal;

	public GUIText scoreText;

    void Builtlevel()
    {
        //Tome os objetos Dynamicobjectscriados na cena e torne-os pais
        GameObject dynamicParent = GameObject.Find("DynamicObjects");

        //Para cada elemento neste  nivel
        for (int yPos = 0; yPos < level.Length; yPos++)
        {
            for (int xPos = 0; xPos < (level[yPos]).Length; xPos++)
            {


				Transform toCreate = null;
				switch (level [yPos][xPos]){

				case 0:
					//nao faz nada, pois nao deve aparecer nada
					break;

				case 1:
					toCreate = wall;
					break;

				case 2:
					toCreate = player;
					break;

				case 3:
					toCreate = orb;
					break;

				case 4 :
					toCreate = goal;
					break;

				default : 
					print("Numero Invalido: "+(level[yPos][xPos]).ToString());
					break;
				}
				if(toCreate != null){
					Transform newObject = Instantiate (toCreate, new Vector3(xPos, (level.Length - yPos), 0), Quaternion.identity) as Transform;

					if(toCreate == goal){
						goalPS = newObject.gameObject.GetComponent<ParticleSystem>();
					}

					//organiza o hierarchy
					newObject.parent = dynamicParent.transform;
				}


		
            }
            
        }
    }


	// Use this for initialization
	void Start () {

        Builtlevel();

		GameObject[] orbs;
		orbs = GameObject.FindGameObjectsWithTag("Orb");

		orbsCollected = 0;
		orbsTotal = orbs.Length;

		scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;
	
	}

	void Awake(){
		_instance = this;
		}

	public void CollectedOrb(){
		orbsCollected++;
		scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;

		if(orbsCollected >= orbsTotal){
			goalPS.Play ();

		}

		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
