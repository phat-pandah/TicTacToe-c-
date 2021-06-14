using System;

namespace TicTacToe
{
    class Game
    {
        // Game Data;
        // board = game board
        // p1_turn = player 1's turn => keep track of whose turn
        //      if p1_turn % 2 == 0 then its p1 turn
        // game_won is a flag to keep state of game
        private int[,] board = new int[3,3];
        private int turn = 0;
        private bool game_won = false;
        private int winner = 0;
        private int[] score = new int[3];
        private int[] cur_move = new int[2];
        private int[] prev_move = new int[2];
        private bool kingodthehill = false; // by default no king of hill

        // Class contructor, build 3x3 board filled w/ 0s`
        public Game() {
            for(int i=0; i<3; i++){
                for(int j=0; j<3; j++){
                    board[i,j] = 0;
                }
            }

            for(int i=0; i<score.Length; i++){
                score[i] = 0;
            }

            for(int i=0; i<2; i++){
                cur_move[i] = 0;
                prev_move[i] = 0;
            }
        }

        // Method to print the board
        public void printBoard() {
            Console.WriteLine("\n");
            for(int i=0; i<3; i++){
                for(int j=0; j<3; j++){
                    if(j==0 || j==1){
                        if(board[i,j] == 1){
                            Console.Write("  X  |");
                        } else if(board[i,j] == 2){
                            Console.Write("  O  |");
                        } else {
                            Console.Write("     |");
                        }
                    } 
                    else {
                        if(board[i,j]==1){
                            Console.Write("  X  \n");
                        } else if(board[i, j] == 2){
                            Console.Write("  O  \n");
                        } else {
                            Console.Write("    \n");
                        }
                    }
                }
               if(i==0 || i==1){
                   Console.Write("-----------------\n");
               }
            }
            Console.WriteLine("\n");

        }

        //Reset game after a game is won/loss
        public void resetGame(int whosTurn = 0) {
            turn = whosTurn;
            game_won = true;
            winner = 0;

            for(int i=0; i<3; i++){
                for(int j=0; j<3; j++){
                    board[i,j] = 0;
                }
            }
        }
        
        public int[] makeMove(){
            Console.Write("Enter x coordinate: ");
            int move_x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter y coordinate: ");
            int move_y = Convert.ToInt32(Console.ReadLine());

            int[] moves = new int[] {move_x, move_y};
            return moves;
        }

        // Helper function to catch moves already made
        public bool moveExists(int x, int y){
            if(board[x,y] != 0){
                return true;
            } else {
                return false;
            }
        }

        public bool validMove(int x, int y){
            if(x<0 || x>2 || y<0 || y>2){
                return false;
            } else {
                return true;
            }
        }
        
        // Player 1 is represented by 1 in board matrix, player 2 by 0. 
        public void updateBoard(int x, int y){
            if(turn%2 == 0){
                board[x, y] = 1;
            }
            else {
                board[x, y] = 2;
            }
        }
        

        public void undoMove(int[] prev_move){
            turn -= 1;
            board[prev_move[0], prev_move[1]] = 0;
            whosTurn();
            cur_move = makeMove();

        }
        
        // findWinner is a void function
        // Updates class variabel "winner" 
        public void findWinner(){
                    //check rows
                    for(int i=0; i < 3; i++){
                        
                        // check rows
                        if(board[i,0] == board[i,1] && board[i,1] == board[i,2] && board[i,0] == 1){
                            winner = 1;
                        }
                        if(board[i,0] == board[i,1] && board[i,1] == board[i,2] && board[i,0] == 2){
                            winner = 2;
                        }

                        //check cols
                        if(board[0,i] == board[1,i] && board[1,i] ==board[2,i] && board[0,i] ==1){
                            winner = 1;
                        }
                        if(board[0,i] == board[1,i] && board[1,i] ==board[2,i] && board[0,i] ==2){
                            winner = 2;
                        }
                        
                    }

                    // check diag

                    if(board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[2,2] == 1){
                        winner = 1;
                    }
                    if(board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[2,2] == 2){
                        winner = 2;
                    }
                    if(board[2,0] == board[1,1] && board[1,1] == board[0,2] && board[2,0] == 1){
                        winner = 1;
                    }
                    if(board[2,0] == board[1,1] && board[1,1] == board[0,2] && board[2,0] == 1){
                        winner = 2;
                    }

                    //check tie 
                    
                    int k = 0;
                        for(int i=0; i<3; i++){
                            for(int j=0; j<3; j++){
                                if(board[i,j] != 0){
                                    k++;
                                }
                            }
                        }
                        if(k == 9){
                            winner =3;
                        }
                }

        public bool endGame(){
            if(winner != 0){
                return true;
            }
            return false;
        }
        
        public void showScore(){
            Console.WriteLine("player 1 | player 2 |   tie   ");
            Console.WriteLine("------------------------------");
            Console.WriteLine("    {0}    |     {1}    |    {2}   \n", score[1], score[2], score[0]);
        }

        public void whoWins(){
            if(winner == 1){
                Console.WriteLine("Player 1 wins\n");
                score[1]++;
            } else if(winner == 2){
                Console.WriteLine("Player 2 wins\n");
                score[2]++;
            }
            else if(winner ==3){
                Console.WriteLine("There are no winners only losers!\n");
                score[0]++;
            }
            else{
                Console.WriteLine("oh poop");
            }
        }
        

        public void grandWinner(){
            int? maxScore = null;
            int index = -1;
            for(int i=0 ; i<3; i++){
                int curScore = score[i];
                if(!maxScore.HasValue || curScore > maxScore.Value){
                    maxScore = curScore;
                    index = i;
                }
            }

            switch(index){
                case 1:
                    Console.WriteLine("Player 1 is the grandmaster!!!\n");
                    break;
                case 2:
                    Console.WriteLine("Player 2 whooped player 1's derrier!!!\n");
                    break;
                case 0:
                    Console.WriteLine("Ya'll are both losers..not something we didn't already know!\n");
                    break;
                default:
                Console.WriteLine("This should never be called");
                break;
            }   
        }
        public void whosTurn(){
            if(turn%2==0){
                Console.WriteLine("Player 1's turn :D\n");
            }
            else{
                Console.WriteLine("Player 2's turn :D\n");
            }
        }        
        public void playGame(){
            Console.WriteLine("To undo move, press 99 in the next player's x move.");
            Console.WriteLine("Winner always starts?? (1 for yes, 2 for no");
            int king = Convert.ToInt32(Console.ReadLine());
            
                switch(king){
                    case 1:
                    kingodthehill=true;
                    break;

                    case 2:
                    kingodthehill=false;
                    break;

                    default:
                    break;
                }
                
            
            Console.WriteLine("Who Starts? (1 for player 1, 2 for player 2)");
            int start_player = Convert.ToInt16(Console.ReadLine());
            
            if(start_player == 1){
                turn = 0;
            }
            else if(start_player == 2){
                turn = 1;
            } 
            else{
                Console.WriteLine("WRONG! this is a two player game idiot. Go back to kindergarten\n");
            }

            bool amPlaying = true;
            
            // This is where the game happens
            while(amPlaying){
                // decide who plays each itter of loop1
            
                whosTurn();

                //read coordinates from user
                cur_move = makeMove();

                if(cur_move[0] == 99){
                    undoMove(prev_move);

                }

                Console.WriteLine("curr move: {0}, {1}", cur_move[0], cur_move[1]);
                if(!validMove(cur_move[0], cur_move[1])){
                    Console.WriteLine("Where are you trying to go?");
                    Console.WriteLine("try again you donkey!");
                    cur_move = makeMove();
                }
                

                if(moveExists(cur_move[0], cur_move[1])){
                    Console.WriteLine("Play fair, stop trying to cheat, this move already exists!!!\n");
                    cur_move = makeMove();
                }

                
                //update & print board to console
                updateBoard(cur_move[0], cur_move[1]);
                printBoard();
                findWinner();
                game_won = endGame();
                
                

                //When game is won/over ask if players want play again
                if(game_won == true){
                    whoWins();
                    showScore();
                    Console.WriteLine("Play again? (press 1 for yes, 2 for no.)");
                    int play_again = Convert.ToInt32(Console.ReadLine());

                    if(play_again == 1){
                        amPlaying = true;
                    } 
                    else if(play_again == 2){
                        amPlaying = false;
                    } else {
                        Console.Write("Wrong answer.. I am terminating your game!!!");
                        amPlaying = false;
                    }

                    // Decides who starts next game
                    if(amPlaying == true){
                        if(kingodthehill){
                            resetGame(winner);
                        } else {
                            Console.WriteLine("Who starts next game?");
                            int start_next = Convert.ToInt32(Console.ReadLine());
                            resetGame(start_next);
                        }
                    } 
                    //Display final score 
                    else if(amPlaying == false){
                        Console.WriteLine("\nFinal Score:\n");
                        showScore();
                        grandWinner();
                    }
                }

                // keep track of total turns played and update prev move
                prev_move = cur_move;
                ++turn;

            }
        }
        

    }


    //TODO: 
    
    
    // future features to add:
    //  - best of n (e.g best of 7)
    //  - king of the hill, winner always starts

    class Driver 
    {
        public static void Main(string[] args){
            var tester = new Game();
            tester.playGame();
        }
    }
}
