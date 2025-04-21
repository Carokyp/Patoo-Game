using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject bgTilePrefab;
    public Leaf[] leafs;

    public Leaf[,] allLeafs;

    public float leafSpeed;

    private UIManager uiMan;

    [HideInInspector]
    public MatchFinder matchFind;

    public enum BoardState {wait, move}
    public BoardState currentState = BoardState.move;

    public Leaf spider;
    public Leaf superLeaf;
    public Leaf woodblock;
    public float spiderChance = 2f;
    public float superLeafChance = 0f;
    public float woodblockChance = 0f;


    [HideInInspector]
    public RoundManager roundMan;

    private float bonusMulti;
    public float bonusAmount = .5f;
    

    private BoardLayout boardLayout;
    private Leaf[,] layoutStore;

    public float resizeRatio;

    public Vector3 hight;

    private void Awake()
    {
        uiMan = FindObjectOfType<UIManager>();
        matchFind = FindObjectOfType<MatchFinder>();
        roundMan = FindObjectOfType<RoundManager>();
        boardLayout = GetComponent<BoardLayout>();

    }

    void Start()
    {
        allLeafs = new Leaf[width, height];

        layoutStore = new Leaf[width, height];

        resizeRatio = transform.parent.localScale.x; 

        Setup();

    }

    private void Update()
    {

    }

    private void Setup()
    {

        if (boardLayout != null)
        {
           layoutStore = boardLayout.GetLayout();
        }
       
       
     
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 offSet;
                offSet = Camera.main.transform.position;
                offSet = (offSet * resizeRatio) - offSet;

                Vector2 pos = new Vector2(x, y) * resizeRatio;
                pos -= offSet;

                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.transform.localScale *= resizeRatio;
                bgTile.name = "BG Tile - " + x + " , " + y;

                if (layoutStore[x, y] != null)
                {
                    NoSpawnLeaf(new Vector2Int(x, y), layoutStore[x, y]);
                }
                else
                {

                    int leafToUse = Random.Range(0, leafs.Length);

                    int iteration = 0;

                    while (MatcheAt(new Vector2Int(x, y), leafs[leafToUse]) && iteration < 100)
                    {
                        leafToUse = Random.Range(0, leafs.Length);
                        iteration++;
                    }

                    SpawnLeaf(new Vector2Int(x, y), leafs[leafToUse]);
                }

            }
        }

        if(boardLayout.leafToFreeze != null)
        {
            foreach(Vector2Int position in boardLayout.leafToFreeze)
            {
                allLeafs[position.x, position.y].freezePosition = true;
            }
        }

        /*hight = transform.localPosition;
        hight.y *= resizeRatio;
        transform.localPosition = hight;*/
    }
    
    private void SpawnLeaf(Vector2Int pos, Leaf leafToSpawn)
    {
        if (Random.Range(0f,100f) < spiderChance)
        {
            leafToSpawn = spider;
           
        }

        if (Random.Range(0f, 100f) < superLeafChance)
        {
            leafToSpawn = superLeaf;

        }

        if (Random.Range(0f, 100f) < woodblockChance)
        {
            leafToSpawn = woodblock;

        }

        Vector2 newPos = pos;

        newPos *= resizeRatio;

        Leaf leaf = Instantiate(leafToSpawn, new Vector3(newPos.x, newPos.y + (height * resizeRatio), 0f) , Quaternion.identity, transform);
        //leaf.transform.parent = transform;
        //leaf.transform.localScale *= transform.localScale.x;
        
        leaf.name = "Leaf - " + pos.x + " , " + pos.y;
        allLeafs[pos.x, pos.y] = leaf;

        leaf.SetupLeaf(pos, resizeRatio, this);
    }


    private void NoSpawnLeaf(Vector2Int pos, Leaf leafToSpawn)
    {


        Vector2 newPos = pos;
        newPos *= resizeRatio;
        Leaf leaf = Instantiate(leafToSpawn, new Vector3(newPos.x, newPos.y + (height * resizeRatio), 0), Quaternion.identity, transform);
        //leaf.transform.parent = transform;
        //leaf.transform.localScale *= transform.localScale.x;
        leaf.name = "Leaf - " + pos.x + " , " + pos.y;
        allLeafs[pos.x, pos.y] = leaf;

        leaf.SetupLeaf(pos,resizeRatio, this);

       
    }

    bool MatcheAt(Vector2Int posToCheck, Leaf leafToCheck)
    {
        if (posToCheck.x > 1)
        {
            if (allLeafs[posToCheck.x - 1, posToCheck.y].type == leafToCheck.type && allLeafs[posToCheck.x - 2, posToCheck.y].type == leafToCheck.type)
            {
                return true;
            }
        }
        if (posToCheck.y > 1)
        {
            if (allLeafs[posToCheck.x, posToCheck.y - 1].type == leafToCheck.type && allLeafs[posToCheck.x, posToCheck.y - 2].type == leafToCheck.type)
            {
                return true;
            }
        }

        return false;
    }

    public void DestroyMatchedLeafAt(Vector2Int pos)
    {

        if (allLeafs[pos.x, pos.y] != null)
        { 
            if (allLeafs[pos.x, pos.y].isMatched)
            {
               

                if (allLeafs[pos.x, pos.y].type == Leaf.LeafType.spider)
                {

                    SFXManager.instance.PlaySpiderMatch();       
                }
                else
                {
                    SFXManager.instance.PlayLeafMatch();
                }

                GameObject effect = Instantiate(allLeafs[pos.x, pos.y].destroyEffect, new Vector2(pos.x, pos.y) * transform.localScale.x, Quaternion.identity);
                effect.transform.localScale *= transform.localScale.x;
                Destroy(allLeafs[pos.x, pos.y].gameObject);
                allLeafs[pos.x, pos.y] = null;
            }
        }
    }

    public void DestroyNoneMatchedLeaf(Vector2Int pos) 
    {

        if (allLeafs[pos.x, pos.y] != null)
        {
            if (!matchFind.IsWoodblock(allLeafs[pos.x, pos.y].type)) 
            {

            
                if (allLeafs[pos.x, pos.y].type == Leaf.LeafType.spider)
                {

                    SFXManager.instance.PlaySpiderMatch();
                }
                else
                {
                    SFXManager.instance.PlayLeafMatch();
                }

                GameObject effect = Instantiate(allLeafs[pos.x, pos.y].destroyEffect, new Vector2(pos.x, pos.y) * transform.localScale.x, Quaternion.identity);
                effect.transform.localScale *= transform.localScale.x;
                Destroy(allLeafs[pos.x, pos.y].gameObject);
                allLeafs[pos.x, pos.y] = null;

            }

        }
           

    }

    public void DestroyMatches()
    {
        matchFind.crackTimer();

        for (int i = 0; i < matchFind.currentMatches.Count; i++)
        {
            if (matchFind.currentMatches[i] != null)
            {
                ScoreCheck(matchFind.currentMatches[i]);
                DestroyMatchedLeafAt(matchFind.currentMatches[i].posIndex);
            }
        }

        StartCoroutine(DecreaseRowCo());
        
    }

    private IEnumerator DecreaseRowCo()
    {
        Debug.Log("Row");

        yield return new WaitForSeconds(0f);

        int nullCounter = 0;
        int wood = 0;
        int spaceBeforeWood = 0;
        int collum = 0;

        for (int x = 0; x < width; x++)
        {
            if (collum != x)
            {
                 nullCounter = 0;
                 wood = 0;
                 spaceBeforeWood = 0;
                 collum = x;
            }
            for (int y = 0; y < height; y++)
            {
                if (allLeafs[x, y] == null)
                {
                    nullCounter++;

                }
                else if (nullCounter > 0)

                {
                    if (!matchFind.IsWoodblock(allLeafs[x, y].type) )
                    {
                        if (allLeafs[x, y].freezePosition == false)
                        {
                   
                          allLeafs[x, y].posIndex.y -= nullCounter;
                          allLeafs[x, y - nullCounter] = allLeafs[x, y];

                          allLeafs[x, y] = null;

                          if (wood > 0)
                          {

                            if (spaceBeforeWood > 1)
                            {
                                spaceBeforeWood--;
                            }
                            else
                            {
                                nullCounter -= wood;
                                wood = 0;
                            }
                          }
                        
                        }
                        else if (allLeafs[x, y].freezePosition == true)
                        {
                            Debug.LogWarning(allLeafs[x, y]);
                            nullCounter++;
                            wood++;
                            spaceBeforeWood = nullCounter - wood;
                        }

                    }
                    else if (matchFind.IsWoodblock(allLeafs[x, y].type) )
                    {
                        
                        nullCounter++;
                        wood++;
                        spaceBeforeWood = nullCounter - wood;

                    }

                }
                

                

            }
           
            nullCounter = 0;
        }

        StartCoroutine(FillBoardCo());
    }

    private IEnumerator FillBoardCo()
    {
        Debug.Log("FillBoard");
        yield return new WaitForSeconds(0f);
        ReFillBoard();

        yield return new WaitForSeconds(0.7f);

        
        matchFind.FindAllMatches();

        if (matchFind.currentMatches.Count > 0)
        {          
            bonusMulti++;
         
            yield return new WaitForSeconds(.5f);
            DestroyMatches();

        }
        else
        {
      
            yield return new WaitForSeconds(.5f);
            currentState = BoardState.move;

            bonusMulti = 0f;

            if (matchFind.availableMatches <= 0)
            {
             Debug.Log("callingShuffle");

                if (matchFind.woodLevel == true)
                {
                    if (matchFind.shuffleCounter < 1)
                    {
                        matchFind.shuffleCounter++;
                        StartCoroutine(matchFind.NoMatch());
                    }
                    else
                    {
                        roundMan.WinCheck();
                        roundMan.shufflePanel.SetActive(false);
                        roundMan.endingRound = false;
                    }


                }
                else
                {
                    StartCoroutine(matchFind.NoMatch());
                }
        
            }

        }
        
       

        if (matchFind.matches > 1)
        {
            
            if (matchFind.isSad == false)
            {
                uiMan.happyDog.SetActive(true);
            }
            
        }
        
        if (bonusMulti == 0f)
        {
            uiMan.happyDog.SetActive(false);
        }
  
    }

    public void ReFillBoard()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allLeafs[x, y] == null)
                {
                    int leafToUse = Random.Range(0, leafs.Length);

                    SpawnLeaf(new Vector2Int(x, y), leafs[leafToUse]);
                }

            }
        }

        CheckMisplacedLeaf();
    }

    private void CheckMisplacedLeaf()
    {
        List<Leaf> foundLeafs = new List<Leaf>();

        foundLeafs.AddRange(FindObjectsOfType<Leaf>());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (foundLeafs.Contains(allLeafs[x,y]))
                {
                    foundLeafs.Remove(allLeafs[x, y]);
                }

            }
        }

        foreach (Leaf g in foundLeafs)
        {
            Destroy(g.gameObject);
        }
    }

    public void ShuffleBoard() 
    {

        if (currentState != BoardState.wait)
        {
            currentState = BoardState.wait;

            List<Leaf> leafsFromBoard = new List<Leaf>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    leafsFromBoard.Add(allLeafs[x, y]);
                    allLeafs[x, y] = null;
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int leafToUse = Random.Range(0, leafsFromBoard.Count);

                    int iteration = 0;

                    while(MatcheAt(new Vector2Int(x,y), leafsFromBoard[leafToUse]) && iteration < 100 && leafsFromBoard.Count > 1 ) 
                    {
                        leafToUse = Random.Range(0, leafsFromBoard.Count);
                        iteration++;

                    }

                    leafsFromBoard[leafToUse].SetupLeaf(new Vector2Int(x, y), resizeRatio, this);
                    allLeafs[x, y] = leafsFromBoard[leafToUse]; 


                    leafsFromBoard.RemoveAt(leafToUse);
                }
            }

            StartCoroutine(FillBoardCo());
        }
    
    }

    public void ScoreCheck(Leaf leafToCheck) 
    {
        
        roundMan.currentScore += leafToCheck.scoreValue;

        if (bonusMulti > 0)
        {
            float bonusToAdd = leafToCheck.scoreValue * bonusMulti * bonusAmount;
            roundMan.currentScore += Mathf.RoundToInt(bonusToAdd);
           
        }

    }

}

