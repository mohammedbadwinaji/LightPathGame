using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightPathPuzzle
{
    internal class Program
    {
        enum enAction
        {
            HitLight = 1,
            RotateMirror = 2
        }
        static void Main(string[] args)
        {
            Board board = new Board(3,4);

            
            board.SetCellType(U.Index(1),U.Index(3),CellType.MirrorLeftRotate);
            board.SetCellType(U.Index(1),U.Index(4),CellType.Target);
            board.SetCellType(U.Index(2), U.Index(1), CellType.Block);
            board.SetCellType(U.Index(3), U.Index(1), CellType.Source);
            board.SetCellType(U.Index(3), U.Index(3), CellType.MirrorRightRotate);

            enAction choice = enAction.HitLight;
            while(true)
            {
                
               
                Console.WriteLine(board.ToString());
                board.RemoveArrows();

                Console.Write("Enter What Do You Want To Do : [1] for hit light , [2] for rotate mirror : ");
                choice = (enAction)Convert.ToInt32(Console.ReadLine());

                ActionResult HitLightresult = null;
                ActionResult RotateMirrorresult = null;
                switch (choice)
                {
                    case enAction.HitLight:
                        Console.Write("Enter Direction : [1] Up, [2] Down , [3] Left , [4] Right  :  ");
                        Direction dir = (Direction)Convert.ToInt32(Console.ReadLine());
                        HitLightresult = board.HitLight(dir);
                        Console.WriteLine("\n" + HitLightresult.Message);

                        if (HitLightresult.Success)
                        {
                            Console.WriteLine("\n\t\t You Win :) \n");
                            Console.WriteLine(board);
                            return;
                        }
                        break;
                    case enAction.RotateMirror:
                        Console.Write("Enter Mirror Location : Ex r,c : ");
                        string[] locationStr = Console.ReadLine().Split(',');
                        int row = Convert.ToInt32(locationStr[0]);
                        int col = Convert.ToInt32(locationStr[1]);
                        Location location = new Location(U.Index(row),U.Index(col));
                        
                        RotateMirrorresult = board.RotateMirror(location);
                        Console.WriteLine("\n" + RotateMirrorresult.Message);

                        break;
                }

                
            }
                       
        }
    }
}
