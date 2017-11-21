using System.Collections.Generic;

namespace Pictest.Service.Response
{
    public class ReadContestListResponse
    {
        public List<ReadContestResponse> Contests { get; set; }
        public string Cursor { get; set; }
    }
}
