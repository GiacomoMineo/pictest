using System;

namespace Pictest.Service.Response
{
    public class ReadPictureResponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Votes { get; set; }
    }
}
