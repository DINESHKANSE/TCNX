using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCNX.Models
{
    public class HappinessLetter
    {
        public string tdate { get; set; }
        public string name { get; set; }
        public string txtmsg { get; set; }
    }
    public class News
    {
        public string tdate { get; set; }
        public string headline { get; set; }
        public string news { get; set; }
    }

    public class Testimonials
    {
        public string tdate { get; set; }
        public string headline { get; set; }
        public string news { get; set; }
        public string username { get; set; }
        public string imgurl1 { get; set; }
        public string des { get; set; }
        public string imgurl { get; set; }
    }
}