using System.Collections.Generic;

namespace Pictest.Service.Response
{
    public class ReadPictureListResponse
    {
        public List<ReadPictureResponse> Pictures { get; set; }
        public string Cursor { get; set; }
    }
}
