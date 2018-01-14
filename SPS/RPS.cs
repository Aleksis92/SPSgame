using System;
using System.Security.Cryptography;
using System.Text;

namespace RPS
{
    
    public class RockPaperScissors
    {
        public const int MoveCount = 7;


        public static void Main(string[] args)
        {
            String[] move = new String[]{"scissors", "paper", "rock", "lizard", "spok" ,"sharp", "cat", "toilet", "beer", "cup"};
            Console.WriteLine("Welcome to the RPS! The game start automatically.");
            while (true) {
                Computer computer = new Computer();
                int computerMove = computer.GetMove(MoveCount, move);
                String[] hashArray = computer.Hash(move[computerMove]);
                computer.Print(hashArray);
                User user = new User();
                GamePlay gamePlay =new GamePlay();
                gamePlay.Counter(computerMove, user.GetMove(MoveCount, move), move, hashArray);
            }
        }
    } 
    
    
    public class User
    {
        
        
        public int GetMove(int moveCount, String[] move)
        {
            int back=0;
            Print(moveCount, move);
            String answer = Console.ReadLine().ToLowerInvariant();
            while (!CheckMove(answer, moveCount, move))
            {
                answer = Console.ReadLine().ToLowerInvariant();
            }

            for (int i = 0; i < moveCount; i++)
            {
                if (answer.Equals(move[i]))
                {
                    back = i;
                    break;
                }
            }
            return back;
        }

        
        public Boolean CheckMove (String answer, int moveCount, String[] move)
        {
            Boolean check = false;
            for (int i = 0; i < moveCount; i++) 
            {
                if (answer.Equals(move[i]))
                {
                    check = true;
                    Console.WriteLine("You made a move.");
                    break;
                }
                else
                {
                    if (i == (moveCount - 1))
                    {
                        Console.WriteLine("Incorrect move!");
                    }
                }
            }
            return check;
        }
        
        
        public void Print (int moveCount, String[] move)
        {
            Console.Write("Select 1 from: ");
            for (int i = 0; i < moveCount; i++) 
            {
                if (i != (moveCount - 1))
                {
                    Console.Write(move[i] + ", ");
                }
                else
                {
                    Console.WriteLine(move[i] + ".");
                }
            }
        }
    }

    
     public class Computer
     {
         
         
         public int GetMove(int moveCount, String[] move)
         {
             Random random = new Random();
             int index = random.Next(moveCount);
             return index;
         }
         
         public void Print(String [] hash)
         {
             Console.WriteLine("Computer completed the move!");
             Console.WriteLine("Hash code for computer move: " + hash[0]);
         }
         
         
         
         public String[] Hash(string text)
         {
             
             byte[] data = Encoding.Default.GetBytes(text);            
             HMACSHA512 hash = new HMACSHA512();
             String result = BitConverter.ToString(hash.ComputeHash(data));
             String hashKey = BitConverter.ToString(hash.Key);
             String[] hashArray = new String[]{result, hashKey};
             return hashArray;
         }
     }
    


    
    public class GamePlay 
    {
        public void Counter (int moveComputer, int moveUser, String[] move, String[] hash)
        {
            Console.WriteLine(moveComputer + " " + moveUser);
            int sub = moveComputer - moveUser;
            if (sub == 0)
            {
                Console.WriteLine("dead heat with " + move[moveUser] + " vs " + move[moveComputer] + ".");
                Console.WriteLine("Key for computer move: " + hash[1]);
                Console.WriteLine();
            }
            if (sub < 0)
            {
                sub = sub - 1;
                Print(sub, moveComputer, moveUser, move, hash);
            }
            if (sub > 0)
            {
                Print(sub, moveComputer, moveUser, move, hash);
            }
           
        }


        public void Print(int sub, int moveComputer, int moveUser, String[] move, String[] hash)
        {
            switch (Math.Abs(sub)%2)
            {
                case 1:
                        
                    Console.WriteLine("You win with " + move[moveUser] + " vs " + move[moveComputer] + ".");
                    Console.WriteLine("Key for computer move: " + hash[1]);
                    Console.WriteLine();
                    break;
                case 0:
                    Console.WriteLine("You lose with " + move[moveUser] + " vs " + move[moveComputer] +
                                      "! Ahahaha, loooser!!!");
                    Console.WriteLine("Key for computer move: " + hash[1]);
                    Console.WriteLine();
                    break;  
            } 
        }
    }
    
    
}
   