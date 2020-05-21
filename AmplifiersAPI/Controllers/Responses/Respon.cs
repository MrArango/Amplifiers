using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmplifiersAPI.Controllers.Responses
{
    public class Respon
    {
        public bool Valid { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }


        public Respon()
        {
            Valid = true;
        }
    }
}
