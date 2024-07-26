using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DadJokes.Models
{
    public class DadjokesList
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public DadjokesList()
        {

        }
    }
}
