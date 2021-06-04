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
    

        // Class contructor, build 3x3 board filled w/ 0s
        public Game() {
            for(int i=0; i<3; i++){
                for(int j=0; j<3; j++){
                    board[i,j] = 0;
                }
            }

            
        }

        // public method to access boar
        public int[,] GetBoard() {
            return board;
        }

        //Method to print the board
        // Prints a pretty-ish board
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
        
        // Player 1 is represented by 1 in board matrix, player 2 by 0. 
        public void updateBoard(int x, int y){
            if(turn%2 == 0){
                board[x, y] = 1;
            }
            else {
                board[x, y] = 2;
            }
        }
        // findWinner is a void function
        // Updates class variabel "winner" 
        public void findWinner(){
                    //check rows
                    int p1_counter = 0;
                    int p2_counter = 0;
                    for(int i=0; i<3; i++){
                        for(int j=0; j<3; j++){
                            if(board[i, j] == 1){
                                ++p1_counter;
                            }
                            if(board[i, j] == 2){
                                ++p2_counter;
                            }
                        }
                    }
                    if(p1_counter == 3){
                        winner = 1;
                    }
                    if(p2_counter == 3){
                        winner = 2;
                    }

                    // check columns
                    for(int i=0; i<3; i++){
                        for(int j=0 ;j<3; j++){
                            if(board[j, i] == 1){
                                ++p1_counter;
                            }
                            if(board[j, i] == 2){
                                ++p2_counter;
                            }
                        }
                    }

                    if(p1_counter == 3){
                        winner = 1;
                    }
                    if(p2_counter == 3){
                        winner = 2;
                    }

                    // check diag

                    if(board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[2,2] == 1){
                        winner = 1;
                    }
                    if(board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[2,2] == 2){
                        winner = 2;
                    }
                    if(board[2,0] == board[1,1] && board[1,1] == board[2,0] && board[2,0] == 1){
                        winner = 1;
                    }
                    if(board[2,0] == board[1,1] && board[1,1] == board[2,0] && board[2,0] == 1){
                        winner = 2;
                    }
                }

        public bool endGame(){
            if(winner != 0){
                return true;
            }
            return false;
        }

        public void whoWins(){
            if(winner == 1){
                Console.WriteLine("Player 1 wins\n");
            } else if(winner == 2){
                Console.WriteLine("Player 2 wins\n");
            }
            else {
                Console.WriteLine("There are no winners only losers!\n");
            }
        }
        

        public void whosTurn(){
            if(turn%2==0){
                Console.WriteLine("Player 1's turn :D");
            }
            else{
                Console.WriteLine("Player 2's turn :D");
            }
        }        
        public void playGame(){
            
            Console.WriteLine("Who Starts?\n");
            int start_player = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("start player = {0}", start_player);
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
            bool curr_game_won = false;
            // This is where the game happens
            while(amPlaying){
                whosTurn();
                Console.WriteLine("Enter x coordinate: ");
                int temp_x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter y coordinate: ");
                int temp_y = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Coord entered: {0}, {1}", temp_x, temp_y);
                updateBoard(temp_x, temp_y);
                printBoard();

                if(turn >= 4){
                    findWinner();
                    curr_game_won = endGame();
                }

                if(curr_game_won == true || turn == 9){
                    whoWins();
                    Console.WriteLine("Play again? (press 1 for yes, 2 for no.)");
                    int play_again = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("entered play_again: {0}", play_again);

                    if(play_again == 1){
                        amPlaying = true;
                    } 
                    else if(play_again == 2){
                        amPlaying = false;
                    } else {
                        Console.Write("Wrong answer.. I am terminating your game!!!");
                        amPlaying = false;
                    }
                    if(amPlaying == true){
                        
                        Console.WriteLine("Who starts next game?");
                        int start_next = Convert.ToInt32(Console.ReadLine());
                        resetGame(start_next);
                    }
                }
                ++turn;

            }
        }
        

    }


    //TODO: 
    // Fix play again option from playgame() method
    // add an if statement in game to pull up instructions at any time
    // add a score method to keep track of winners over multiple games
    // make printed msgs on console clearer (maybe the enter x and enter y msg could be on same line?)
    // check for redundant methods. Maybe some could be merged or absorbed by others.
    // clean code
    // clean comments ==> add more, make more explicit/clear
    // add functionallity do ask who starts after a game is won.
    // long term, catch errors/exceptions 
    //  - ex: coordinates out of bounds, etc
            

    class Driver 
    {
        public static void Main(string[] args){
            var tester = new Game();
            tester.playGame();
        }
    }
}
