

namespace LightPathPuzzle
{
    public class ActionResult
    {
        public bool Success { get; set; }   
        public string Message { get; set; }

        public ActionResult(bool success , string msg) { 
            Success = success;
            Message = msg;
        }
        public static ActionResult Fail(string msg) {
            return new ActionResult(false, msg);
        }
        public static ActionResult Ok(string msg)
        {
            return new ActionResult(true, msg);
        }
    }
}
