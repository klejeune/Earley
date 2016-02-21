using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.Regular {
    class RegularEarleyData : IEarleyData {
        public string Data { get; private set; }

        public RegularEarleyData(string data) {
            this.Data = data;
        }

        public bool IsTerminal {
            get { return !this.Data.All(c => char.IsUpper(c)); }
        }

        public override bool Equals(object obj) {
            return this.Data == ((RegularEarleyData)obj).Data;
        }

        public override int GetHashCode() {
            return this.Data.GetHashCode();
        }

        public override string ToString() {
            return this.Data;
        }

        public bool IsCompatibleWith(IEarleyInput input) {
            return this.Data.Equals(((RegularEarleyInput) input).Data);
        }

        public bool CanBeEmpty {
            get { return false; }
        }
    }
}
