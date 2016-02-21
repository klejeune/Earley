using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
    class Program {
        static void Main(string[] args)
        {
            var tagger = new PosTagger.StanfordPosTagger();
            var result = tagger.Tag("Le chat mange la souris.");
        }
    }
}
