using System;

namespace LightPathPuzzle
{

    public class Board
    {
        public readonly Cell[,] grid;
        public readonly int Rows;
        public readonly int Cols;
        private void InitBoardCells()
        {
            for(int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    this.grid[r, c] = new Cell();
                }
            }
        }

        private Location GetLightLocation()
        {
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    if(this.grid[r, c].Type == CellType.Source)
                    {
                        return new Location(r, c);
                    }
                }
            }
            return new Location(-1,-1);
        }

        private bool CheckLocationValidation(int row, int col)
        {
            return CheckLocationValidation(new Location(row, col));
        }
        private bool CheckLocationValidation(Location location)
        {
            return InBound(location);
        }

        public void RemoveArrows()
        {
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    if (IsArrow(r,c))
                    {
                        this.grid[r, c].Type = CellType.Empty;
                    }
                }
            }
        }

        private bool InBound(int row,int col)
        {
            return InBound(new Location(row, col)); 
        }
        private bool InBound(Location location)
        {
            return      location.Row >= 0
                   &&   location.Row < Rows
                   &&   location.Col >= 0
                   &&   location.Col < Cols;
        }


        public Board(int Rows,int Cols)
        {
            this.Rows = Rows;
            this.Cols = Cols;   
            this.grid = new Cell[Rows, Cols];
            InitBoardCells();
        }


        public void SetCellType(int row,int col, CellType Type)
        {
            this.grid[row, col].Type = Type;
        }

        public void SetCellType(Location location,CellType Type)
        {
            if (!CheckLocationValidation(location))
                return;
            
            this.grid[location.Row,location.Col].Type = Type;
        }
        

        public override string ToString()
        {
            string board = "";
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    board += (this.grid[r, c].ToString()) + "  ";
                }
                board += "\n";
            }

            return board;


        }


        private bool IsTarget(Location loc)
        {
            return IsTarget(loc.Row, loc.Col);
        }
        private bool IsTarget(int row , int col)
        {
            return this.grid[row, col].Type == CellType.Target;
        }
        private bool IsArrow(Location loc)
        {
            return IsArrow(loc.Row,loc.Col);
        }
        private bool IsArrow(int row,int col)
        {
            CellType T = this.grid[row, col].Type;
            return     T == CellType.TopArrow
                    || T == CellType.BottomArrow
                    || T == CellType.LeftArrow
                    || T == CellType.RightArrow;
        }
        private bool IsLight(Location location)
        {
            return IsLight(location.Row,location.Col);
        }
        private  bool IsLight(int row,int col)
        {
            return this.grid[row, col].Type == CellType.Source;
        }
        private bool IsEmpty(Location location)
        {
            return IsEmpty(location.Row,location.Col);
        }
        private bool IsEmpty(int row, int col)
        {
            return this.grid[row, col].Type == CellType.Empty;
        }
        private bool IsBlock(Location location)
        {
            return IsBlock(location.Row,location.Col);
        }
        private bool IsBlock(int row,int col)
        {
            return this.grid[row, col].Type == CellType.Block;
        }


        private bool IsRightRotateMirror(Location location)
        {
            return IsRightRotateMirror(location.Row,location.Col);
        }
        private bool IsRightRotateMirror(int row,int col)
        {
            return this.grid[row, col].Type == CellType.MirrorRightRotate;
        }
        private bool IsLeftRotateMirror(Location location)
        {
            return IsLeftRotateMirror(location.Row,location.Col);
        }
        private bool IsLeftRotateMirror(int row,int col)
        {
            return this.grid[row, col].Type == CellType.MirrorLeftRotate;
        }
        private bool IsMirror(Location location)
        {
            return IsMirror(location.Row,location.Col);
        }
        private bool IsMirror (int row,int col)
        {
            return  this.grid[row,col].Type == CellType.MirrorLeftRotate
                    ||
                    this.grid[row, col].Type == CellType.MirrorRightRotate;   
        }

        private void ApplyMirrorRotation(int row, int col)
        {
            this.grid[row,col].Type =
                this.grid[row, col].Type ==
                CellType.MirrorLeftRotate 
                ? CellType.MirrorRightRotate
                : CellType.MirrorLeftRotate;
        }



        public ActionResult RotateMirror(Location location)
        {
            return RotateMirror(location.Row, location.Col);
        }
        public ActionResult RotateMirror(int row,int col)
        {
            if (! InBound(row, col))
            {
                return ActionResult.Fail($"Out Of Bound");
            }
            if(IsMirror(row,col))
            {
                ApplyMirrorRotation(row,col);
                return ActionResult.Ok("Mirror Rotated Successfully");
            } else
            {
                return ActionResult.Fail($"Cell Is Not Mirror");
            }
        }


        private Location Adjacent(Location location,Direction dir)
        {
            return Adjacent(location.Row, location.Col, dir);
        }
        private Location Adjacent(int row, int col,Direction dir)
        {
            switch(dir)
            {
                case Direction.Up:
                    return new Location(row - 1, col);
                case Direction.Down:
                    return new Location(row + 1, col);
                case Direction.Left:
                    return new Location(row, col - 1);
                case Direction.Right:
                    return new Location(row, col + 1);
                default:
                    return new Location(row, col);
            }
        }

        private CellType GetCellArrow(Direction dir)
        {
            switch(dir)
            {
                case Direction.Up:
                    return CellType.TopArrow;
                case Direction.Down:
                    return CellType.BottomArrow;
                case Direction.Left:
                    return CellType.LeftArrow;
                case Direction.Right:
                    return CellType.RightArrow;
                default:
                    return CellType.TopArrow;
            }
        }


        private Direction GetReflectedDirection(Location loc,Direction dir)
        {
            switch(dir)
            {
                case Direction.Right:
                    if (IsLeftRotateMirror(loc))
                    {
                        return Direction.Down;
                    }
                    else
                    {
                        return Direction.Up;
                    }
                case Direction.Left:
                    if(IsLeftRotateMirror(loc))
                    {
                        return Direction.Up;
                    } else
                    {
                        return Direction.Down;
                    }
                case Direction.Up:
                    if(IsLeftRotateMirror(loc))
                    {
                        return Direction.Left;
                    } else
                    {
                        return Direction.Right;
                    }
                case Direction.Down: 
                    if(IsLeftRotateMirror(loc))
                    {
                        return Direction.Right;
                    } else
                    {
                        return Direction.Left;
                    }
                default:
                    return dir;
            }
        }
        private ActionResult ApplyLightHitting(Location lightLoc, Direction dir)
        {
            return ApplyLightHitting(lightLoc.Row,lightLoc.Col, dir);
        }
        private ActionResult ApplyLightHitting(int row, int col,Direction dir)
        {
            Location currentLoc = Adjacent(row,col,dir);
            while(true)
            {
                if (!InBound(currentLoc))
                {
                    return ActionResult.Fail("Light Went Out of The Board");
                }

                else if (IsBlock(currentLoc))
                {
                    return ActionResult.Fail("Light Hit Block");
                }

                else if (IsEmpty(currentLoc) || IsArrow(currentLoc))
                {
                    this.grid[currentLoc.Row, currentLoc.Col].Type =
                        GetCellArrow(dir);
                    ;
                    currentLoc = Adjacent(currentLoc, dir);
                }

                else if (IsMirror(currentLoc))
                {   
                    dir = GetReflectedDirection(currentLoc, dir);
                    currentLoc = Adjacent(currentLoc, dir);
                } 
                
                else
                {
                    return ActionResult.Ok("Light Hit Target");
                }
            }
        }
        public ActionResult HitLight(Direction dir)
        {
            Location lightLoc = GetLightLocation();
            if(!InBound(lightLoc.Row,lightLoc.Col))
            {
                return ActionResult.Fail("There Is No Light In Board");
            } 

            return ApplyLightHitting(lightLoc,dir);
        }


    }
}