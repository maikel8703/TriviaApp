using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Be careful about using static variables when they're not necessary
    private GameObject player1, player2;
    // Add references to the player piece scripts
    private PlayerPiece player1Piece;
    private PlayerPiece player2Piece;

    public int whosTurn = 1;

    private static GameObject whoWinsTextShadow, player1MoveText, player2MoveText, PanelTextShadow;

    public bool moveplayer1=false;

    public bool moveplayer2=false;

    private QuizManager quizManager;

    public GameObject quizMG; 

    public bool dadoFallido1=false;

    public bool dadoFallido2=false;

    public bool gameOver = false;

    public GameObject pM;

    private PlayerMove playerMove2;


    // Start is called before the first frame update
    void Awake()
    {
        player1 = GameObject.Find("Player1-Piece");
        player2 = GameObject.Find("Player2-Piece");

        quizManager = quizMG.GetComponent<QuizManager>();

        // Set the reference to the script so you don't have to call GetComponent() each time
        player1Piece = player1.GetComponent<PlayerPiece>();
        player2Piece = player2.GetComponent<PlayerPiece>();

        playerMove2= pM.GetComponent<PlayerMove>();
    }

    void Start()
    {
        //playerMove2.SetPlayerMovement(false);

        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        PanelTextShadow = GameObject.Find("PanelWins");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");

        PanelTextShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {    
        //playerMove2.SetPlayerMovement(false);

        /*if(player1Piece.routePosition>7)
            {
               Debug.Log("Ruta Mayor que 7: "+player1Piece.routePosition);
               //playerMove2.SetPlayerMovement(true);
            }*/

        if(quizManager.lifeRemaining<=1)
           {
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(true);
           }else  if(QuizUI.perder==true){

        player2MoveText.gameObject.SetActive(true);
        player1MoveText.gameObject.SetActive(false);
            }

           if(player1Piece.routePosition==10)
           {
               PanelTextShadow.gameObject.SetActive(true);
               whoWinsTextShadow.GetComponent<Text>().text="Ganaste";
               gameOver = true;
           }else  if(player2Piece.routePosition==10)
           {
               PanelTextShadow.gameObject.SetActive(true);
               whoWinsTextShadow.GetComponent<Text>().text="Perdiste";
               gameOver = true;
           }
       
        
        /*if(QuizUI.perder==true)
        {
            MovePlayer1(0);
            //PlayerPiece.moviendose=false;
            Debug.Log("Moviendose Falso ");
        }*/
        
       // Clean this out and we'll handle movement directly in the PlayerMove
    }
   // Change variable to handle steps to move
   public void MovePlayer(int steps)
    {

        if(QuizUI.perder==false && dadoFallido1==false){

            whosTurn=1;
        }

        if(QuizUI.perder==false && dadoFallido1==true){

            QuizUI.perder=true;
            whosTurn=-1;
        }else if(QuizUI.perder==true && dadoFallido2==true){

            QuizUI.perder=false;
            whosTurn=1;


        }
        
       if (whosTurn == 1){   
                             

           if(player1Piece.routePosition+steps<player1Piece.currentRoute.childNodeList.Count){
                StartCoroutine(player1Piece.Move(steps));

                
                moveplayer1=true;
                moveplayer2=false;
                dadoFallido2=false;

            
           }else{

            Debug.Log("Player1 Dado"+dadoFallido1);
            dadoFallido1=true;
            dadoFallido2=false;
            QuizUI.perder=true;
            
        }
            
           }else if (whosTurn == -1)
        {                       

            if(player2Piece.routePosition+steps<player1Piece.currentRoute.childNodeList.Count){
            StartCoroutine(player2Piece.Move(steps));
            
            moveplayer1=false;
            moveplayer2=true;
            dadoFallido1=false;
            quizManager.lifeRemaining=3;

           

        player2MoveText.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);

            Debug.Log("Player2 perder: "+QuizUI.perder);
            }else{

                QuizUI.perder=false;
                Debug.Log("Player2 Dado"+QuizUI.perder);
                dadoFallido2=true;
                dadoFallido1=false;
                

            }      
            

        }
        

        whosTurn*=-1;
    }

    public void MovePlayer1(int steps){

        StartCoroutine(player1Piece.Move(steps));
        //QuizUI.perder=false;
        

    }

}
