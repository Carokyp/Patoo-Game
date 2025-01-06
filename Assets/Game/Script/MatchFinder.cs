using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MatchFinder : MonoBehaviour
{
    private UIManager uiMan;
    public bool isSad = false;
    public GameObject noMatchPanel;
    private Board board;
    public List<Leaf> currentMatches = new List<Leaf>();
    public List<Leaf> currentMatches1 = new List<Leaf>();
    public List<Leaf> possibleMatches = new List<Leaf>();
    public int matches;
    public bool test;
    public bool canShuffle = true;
    public int availableMatches = 0;
    bool firstMatch = false;
    public bool woodLevel = false;
    private RoundManager roundManager;
    

    private void Awake()
    {
        board = FindObjectOfType<Board>();
        uiMan = FindObjectOfType<UIManager>();
        roundManager = FindObjectOfType<RoundManager>();

    }



    public void FindAllMatches()
    {
        currentMatches.Clear();
        currentMatches1.Clear();
        matches = 0;
        test = false;
        availableMatches = 0;
        possibleMatches.Clear();
        

        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                Leaf currentLeaf = board.allLeafs[x, y];

                if (currentLeaf != null && currentLeaf.type != Leaf.LeafType.woodBlock)
                {
                    if (x > 0 && x < board.width - 1)
                    {
                        Leaf leftLeaf = board.allLeafs[x - 1, y];
                        Leaf rightLeaf = board.allLeafs[x + 1, y];

                        if (leftLeaf != null && rightLeaf != null)
                        {
                            if (leftLeaf.type == currentLeaf.type && rightLeaf.type == currentLeaf.type)
                            {
                                bool inList = false;

                                currentLeaf.isMatched = true;
                                leftLeaf.isMatched = true;
                                rightLeaf.isMatched = true;

                                if (currentMatches1.Count != 0)
                                {
                                    for (int i = 0; i < currentMatches1.Count; i++)
                                    {
                                        if (currentLeaf == currentMatches1[i])
                                        {
                                            inList = true;
                                        }

                                        if (rightLeaf == currentMatches1[i])
                                        {
                                            inList = true;
                                        }

                                        if (leftLeaf == currentMatches1[i])
                                        {
                                            inList = true;
                                        }
                                   
                                    }
                                  
                                }

                                if (inList == false)
                                {
                                    currentMatches1.Add(currentLeaf);
                                    currentMatches1.Add(leftLeaf);
                                    currentMatches1.Add(rightLeaf);
                                    matches++;
                                }

                                    currentMatches.Add(currentLeaf);
                                    currentMatches.Add(leftLeaf);
                                    currentMatches.Add(rightLeaf);



                            }
                            else if (currentLeaf.type != Leaf.LeafType.spider && currentLeaf.type != Leaf.LeafType.woodBlock)
                            {
                                
                                #region checkLeftSide

                                if (rightLeaf.type == currentLeaf.type)
                                {
                                  

                                    if (leftLeaf.type != Leaf.LeafType.woodBlock)
                                    {
                                        // check side matches
                                        if (currentLeaf.posIndex.x > 1)
                                        {

                                            Leaf leftGapLeaf = board.allLeafs[x - 2, y];

                                            if (leftGapLeaf.type == currentLeaf.type)
                                            {
                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }
                                        // check diaganol matches
                                        if (currentLeaf.posIndex.y < board.height - 1)
                                        {
                                            Debug.Log("currentLeaf " + currentLeaf.posIndex);

                                            Leaf aboveDiagonal = board.allLeafs[x - 1, y + 1];
                                            if (aboveDiagonal.type == currentLeaf.type)
                                            {
                                            
                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }

                                        if (currentLeaf.posIndex.y > 0)
                                        {
                                            Leaf belowDiagonal = board.allLeafs[x - 1, y - 1];
                                            if (belowDiagonal.type == currentLeaf.type)
                                            {
                                                Debug.Log("belowDiagonal " + belowDiagonal);

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }
                                    }
                                    
                                    

                                }
                                #endregion

                                #region checkRightSide 
                                
                                if (leftLeaf.type == currentLeaf.type)
                                {
                                    if (rightLeaf.type != Leaf.LeafType.woodBlock)
                                    {
                                        // check side matches
                                        if (currentLeaf.posIndex.x < board.width - 2)
                                        {

                                            Leaf rightGapLeaf = board.allLeafs[x + 2, y];

                                            if (rightGapLeaf.type == currentLeaf.type)
                                            {
                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }

                                        if (currentLeaf.posIndex.y < board.height - 1)
                                        {
                                            Debug.Log("currentLeaf " + currentLeaf.posIndex);

                                            Leaf aboveDiagonal = board.allLeafs[x + 1, y + 1];
                                            if (aboveDiagonal.type == currentLeaf.type)
                                            {

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }

                                        if (currentLeaf.posIndex.y > 0)
                                        {
                                            Leaf belowDiagonal = board.allLeafs[x + 1, y - 1];
                                            if (belowDiagonal.type == currentLeaf.type)
                                            {
                                                Debug.Log("belowDiagonal " + belowDiagonal);

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }
                                    }



                                }
                                #endregion 

                                #region checkMiddleMatch
                                if (currentLeaf.posIndex.y < board.height - 1 )
                                {
                                    if (board.allLeafs[x, y +1].type != Leaf.LeafType.woodBlock)
                                    {
                                        Leaf diagonalRightLeaf = board.allLeafs[x + 1, y + 1];
                                        Leaf diagonalLeftLeaf = board.allLeafs[x - 1, y + 1];

                                        if (currentLeaf.type == diagonalRightLeaf.type && currentLeaf.type == diagonalLeftLeaf.type)
                                        {
                                            availableMatches++;
                                            possibleMatches.Add(currentLeaf);
                                        }
                                    }
                                }

                                if (currentLeaf.posIndex.y > 0)
                                {
                                    if (board.allLeafs[x, y - 1].type != Leaf.LeafType.woodBlock)
                                    {
                                        Leaf diagonalRightLeaf = board.allLeafs[x + 1, y - 1];
                                        Leaf diagonalLeftLeaf = board.allLeafs[x - 1, y - 1];

                                        if (currentLeaf.type == diagonalRightLeaf.type && currentLeaf.type == diagonalLeftLeaf.type)
                                        {
                                            availableMatches++;
                                            possibleMatches.Add(currentLeaf);
                                        }
                                    }
                                }
                                #endregion 

                                
                            }
                          
                        }
                    }

                    if (y > 0 && y < board.height - 1)
                    {
                        Leaf aboveLeaf = board.allLeafs[x, y + 1];
                        Leaf belowLeaf = board.allLeafs[x, y - 1];

                        if (aboveLeaf != null && belowLeaf != null)
                        {
                            if (aboveLeaf.type == currentLeaf.type && belowLeaf.type == currentLeaf.type )
                            {
                                bool inList = false;

                                currentLeaf.isMatched = true;
                                aboveLeaf.isMatched = true;
                                belowLeaf.isMatched = true;

                                if (currentMatches1.Count != 0)
                                {
                                    for (int i = 0; i < currentMatches1.Count; i++)
                                    {
                                        if (currentLeaf == currentMatches1[i])
                                        {
                                            inList = true;
                                        }

                                        if (aboveLeaf == currentMatches1[i])
                                        {
                                            inList = true;
                                        }

                                        if (belowLeaf == currentMatches1[i])
                                        {
                                            inList = true;
                                        }
                                    }
                                }

                                if (inList == false)
                                {
                                    currentMatches1.Add(currentLeaf);
                                    currentMatches1.Add(aboveLeaf);
                                    currentMatches1.Add(belowLeaf);

                                    matches++;
                                }

                                currentMatches.Add(currentLeaf);
                                currentMatches.Add(aboveLeaf);
                                currentMatches.Add(belowLeaf);

                            }

                            else if (currentLeaf.type != Leaf.LeafType.spider && currentLeaf.type != Leaf.LeafType.woodBlock)
                            {

                                #region checkBelow

                                if (aboveLeaf.type == currentLeaf.type)
                                {


                                    if (belowLeaf.type != Leaf.LeafType.woodBlock)
                                    {
                                        // check side matches
                                        if (currentLeaf.posIndex.y > 1)
                                        {

                                            Leaf belowGapLeaf = board.allLeafs[x, y - 2];

                                            if (belowGapLeaf.type == currentLeaf.type)
                                            {
                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }
                                        // check diaganol matches
                                        if (currentLeaf.posIndex.x < board.width - 1)
                                        {
                                            Debug.Log("currentLeaf " + currentLeaf.posIndex);

                                            Leaf rightDiagonal = board.allLeafs[x + 1, y - 1];
                                            if (rightDiagonal.type == currentLeaf.type)
                                            {

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }

                                        if (currentLeaf.posIndex.x > 0)
                                        {
                                            Leaf leftDiagonal = board.allLeafs[x - 1, y - 1];
                                            if (leftDiagonal.type == currentLeaf.type)
                                            {
                                                Debug.Log("belowDiagonal " + leftDiagonal);

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }
                                    }



                                }
                                #endregion 

                                #region checkAboveSide 

                                if (belowLeaf.type == currentLeaf.type)
                                {
                                    if (aboveLeaf.type != Leaf.LeafType.woodBlock)
                                    {
                                        // check side matches
                                        if (currentLeaf.posIndex.y < board.height - 2)
                                        {

                                            Leaf aboveGapLeaf = board.allLeafs[x , y + 2];

                                            if (aboveGapLeaf.type == currentLeaf.type)
                                            {
                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }

                                        if (currentLeaf.posIndex.x < board.width - 1)
                                        {
                                            Debug.Log("currentLeaf " + currentLeaf.posIndex);

                                            Leaf rightDiagonal = board.allLeafs[x + 1, y + 1];
                                            if (rightDiagonal.type == currentLeaf.type)
                                            {

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }

                                        if (currentLeaf.posIndex.x > 0)
                                        {
                                            Leaf leftDiagonal = board.allLeafs[x - 1, y + 1];
                                            if (leftDiagonal.type == currentLeaf.type)
                                            {
                                                Debug.Log("belowDiagonal " + leftDiagonal);

                                                availableMatches++;
                                                possibleMatches.Add(currentLeaf);
                                            }
                                        }
                                    }



                                }
                                #endregion 

                                #region checkMiddleMatch
                                if (currentLeaf.posIndex.x < board.width - 1)
                                {
                                    if (board.allLeafs[x , y - 1].type != Leaf.LeafType.woodBlock)
                                    {
                                        Leaf diagonalAboveLeaf = board.allLeafs[x + 1, y + 1];
                                        Leaf diagonalBelowLeaf = board.allLeafs[x + 1, y - 1];

                                        if (currentLeaf.type == diagonalAboveLeaf.type && currentLeaf.type == diagonalBelowLeaf.type)
                                        {
                                            availableMatches++;
                                            possibleMatches.Add(currentLeaf);
                                        }
                                    }
                                }

                               if (currentLeaf.posIndex.x > 0)
                                {
                                    if (board.allLeafs[x - 1, y].type != Leaf.LeafType.woodBlock)
                                    {
                                        Leaf diagonalAboveLeaf = board.allLeafs[x - 1, y + 1];
                                        Leaf diagonalBelowLeaf = board.allLeafs[x - 1, y - 1];

                                        if (currentLeaf.type == diagonalAboveLeaf.type && currentLeaf.type == diagonalBelowLeaf.type)
                                        {
                                            availableMatches++;
                                            possibleMatches.Add(currentLeaf);
                                        }
                                    }
                                }
                                #endregion 


                            }

                            Debug.Log("availableMatches " + availableMatches);

                            

                            

                           
                        }
                    }

                }
            }

        }
        Debug.Log("Match count " + matches);
        if (currentMatches.Count > 0)
        {
            currentMatches = currentMatches.Distinct().ToList();

        }
        

        CheckForSpiders();
        firstMatch = true;

        if (woodLevel == true && availableMatches <= 0)
        {
            roundManager.WinCheck();
            roundManager.shufflePanel.SetActive(false);
            roundManager.endingRound = false;
        }
    }

    

    public void CheckForSpiders() 
    {
        for (int i = 0; i < currentMatches.Count; i++)
        {
            Leaf leaf = currentMatches[i];

            int x = leaf.posIndex.x;
            int y = leaf.posIndex.y;

            if (leaf.posIndex.x > 0)
            {
                if (board.allLeafs[x - 1, y] != null)
                {
                    if (board.allLeafs[x-1,y].type == Leaf.LeafType.spider)
                    {
                        MarkSpiderArea(new Vector2Int(x - 1, y), board.allLeafs[x-1,y]);
                        
                    }
                }
            }

            if (leaf.posIndex.x < board.width - 1)
            {
                if (board.allLeafs[x + 1, y] != null)
                {
                    if (board.allLeafs[x + 1, y].type == Leaf.LeafType.spider)
                    {
                        MarkSpiderArea(new Vector2Int(x + 1, y), board.allLeafs[x + 1, y]);
                    }
                }
            }

            if (leaf.posIndex.y > 0)
            {
                if (board.allLeafs[x , y - 1] != null)
                {
                    if (board.allLeafs[x, y - 1 ].type == Leaf.LeafType.spider)
                    {
                        MarkSpiderArea(new Vector2Int(x, y - 1 ), board.allLeafs[x, y - 1]);
                    }
                }
            }

            if (leaf.posIndex.y < board.height - 1)
            {
                if (board.allLeafs[x, y + 1] != null)
                {
                    if (board.allLeafs[x, y + 1].type == Leaf.LeafType.spider)
                    {
                        MarkSpiderArea(new Vector2Int(x, y + 1), board.allLeafs[x, y + 1]);
                        
                    }
                }
            }
        }
    }

    public void MarkSpiderArea(Vector2Int spiderPos, Leaf theSpider ) 
    {
        for (int x = spiderPos.x - theSpider.blastSize; x <= spiderPos.x + theSpider.blastSize; x++ )
        {
            for (int y = spiderPos.y - theSpider.blastSize; y <= spiderPos.y + theSpider.blastSize; y++)
            {
                if (x >= 0 && x < board.width && y >= 0 && y < board.height)
                {
                    if (board.allLeafs[x, y] != null && board.allLeafs[x, y].type != Leaf.LeafType.woodBlock)
                    {
                        board.allLeafs[x, y].isMatched = true;
                        currentMatches.Add(board.allLeafs[x, y]);
                        if (firstMatch == true)
                        {
                            StartCoroutine(SadDog());
                        }
                        

                    }

                }
            }
        }
        currentMatches = currentMatches.Distinct().ToList();

        
    }

     public IEnumerator SadDog()
     {
          isSad = true;
          yield return new WaitForSeconds(0.2f);
          uiMan.sadDog.SetActive(true);
          
          yield return new WaitForSeconds(2f);
          isSad = false;
          uiMan.sadDog.SetActive(false);

     }

    public IEnumerator NoMatch() 
    {
        yield return new WaitForSeconds(1f);
        noMatchPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        noMatchPanel.SetActive(false);

         Debug.Log("Shuffle");

         yield return new WaitForSeconds(1f);

         for (int x = 0; x < board.width; x++)
         {
             for (int y = 0; y < board.height; y++)
             {
                 board.DestroyNoneMatchedLeaf(new Vector2Int(x, y));

             }

         }

         board.ReFillBoard();
         Debug.Log("looking for matches");
         FindAllMatches();
         board.DestroyMatches();

    }


}
