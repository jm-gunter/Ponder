using System;
namespace Ponder.Models
{
    public class PonderResult
    {
        public PonderResult(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

    }
}
