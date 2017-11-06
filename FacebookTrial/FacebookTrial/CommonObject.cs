using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookTrial
{
    public class FacebookLoginResult
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }

    public class FacebookProfile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string linkUri { get; set; }
        public string middleName { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string iconUrl { get; set; }
    }
}
