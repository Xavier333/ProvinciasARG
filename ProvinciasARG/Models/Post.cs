using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciasARG.Models
{
        public class Post
        {
            public Centroide centroide { get; set; }
            public string id { get; set; }
            public string nombre { get; set; }
        }

        public class Centroide
        {
            public float lat { get; set; }
            public float lon { get; set; }
        }

}
