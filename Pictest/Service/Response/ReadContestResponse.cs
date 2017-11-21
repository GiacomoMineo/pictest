using System;

namespace Pictest.Service.Response
{
    public class ReadContestResponse
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Closed { get; set; }
    }
}
