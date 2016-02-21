using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.Regular {
    class RegularEarleyInput : IEarleyInput {
        public string Data { get; private set; }

        public RegularEarleyInput(string data) {
            this.Data = data;
        }

        public override string ToString() {
            return this.Data;
        }
    }
}
