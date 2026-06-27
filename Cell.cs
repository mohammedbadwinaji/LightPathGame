namespace LightPathPuzzle
{

    public class Cell
    {
        public CellType Type { get; set; }

        private string CellShap()
        {
            switch (Type) {
                case CellType.Empty:
                    return ".";
                case CellType.Block:
                    return "#";
                case CellType.Source:
                    return "S";
                case CellType.Target:
                    return "T";
                case CellType.MirrorLeftRotate:
                    return "\\";
                case CellType.MirrorRightRotate:
                    return "/";
                case CellType.TopArrow:
                    return "^";
                case CellType.BottomArrow:
                    return "v";
                case CellType.LeftArrow:
                    return "<";
                case CellType.RightArrow:
                        return ">";
                default:
                    return " ";
            }

            
        }

        public Cell()
        {
            this.Type = Global.DefaultCellType;
        }
        public Cell(CellType Type)
        {
            this.Type = Type;
        }

        public override string ToString()
        {
            return this.CellShap();
        }

    }

}