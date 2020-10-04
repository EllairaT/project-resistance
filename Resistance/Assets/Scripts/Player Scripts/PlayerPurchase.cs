using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurchase : MonoBehaviour
{
    //--all the structures that the user has bought.
    [SerializeField] private List<GameObject> blockArr;
    [SerializeField] private List<GameObject> fenceArr;
    [SerializeField] private List<GameObject> gatesArr;
    [SerializeField] private List<GameObject> stairsArr;
    [SerializeField] private List<GameObject> wallsArr;
    [SerializeField] private List<GameObject> foundationArr; //all defenders have access to foundation blocks with no cost.

    public List<GameObject> BlockArr { get => blockArr; set => blockArr = value; }
    public List<GameObject> FenceArr { get => fenceArr; set => fenceArr = value; }
    public List<GameObject> GatesArr { get => gatesArr; set => gatesArr = value; }
    public List<GameObject> StairsArr { get => stairsArr; set => stairsArr = value; }
    public List<GameObject> WallsArr { get => wallsArr; set => wallsArr = value; }
    public List<GameObject> FoundationArr { get => foundationArr; set => foundationArr = value; }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<List<GameObject>> GetAllPurchases()
    {
        List<List<GameObject>> allPurchases = new List<List<GameObject>>
        {
            blockArr,
            fenceArr,
            gatesArr,
            stairsArr,
            wallsArr,
            foundationArr
        };

        return allPurchases;
    }
}
