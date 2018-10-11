using System.Collections.Generic;

namespace APIBase
{
    public class APIResponse
    {
        public object Item { get; set; }
        public List<string> Messages { get; set; }
    }
}