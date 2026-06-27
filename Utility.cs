namespace LightPathPuzzle
{

    public static class U
    {
        public static int Index(int ind)
        {
            if(Settings.IndexBased == Settings.IndexNumberBased.One)
            {
                return ind - 1;                 
            }
            else
            {
                return ind;
            }
        }
    }
    

}