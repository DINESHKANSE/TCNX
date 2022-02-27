using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCNX.Models
{
    public class Node
    {
        public Node()
        {
            children = new System.Collections.Generic.List<Node>();
        }

        public string id { get; set; }

        public string text { get; set; }

        public System.Collections.Generic.List<Node> children { get; set; }
    }
}