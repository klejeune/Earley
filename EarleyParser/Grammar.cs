using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser {
    class Grammar {
        private List<IEarleyGrammarRule> rules = new List<IEarleyGrammarRule>();

        public IEnumerable<IEarleyGrammarRule> Rules { get { return this.rules; } }

        public IEnumerable<IEarleyGrammarRule> StartRules { get { return this.rules.Where(rule => rule.IsStartRule); } }

        public void AddRule(IEarleyGrammarRule rule) {
            this.rules.Add(rule);
        }

        public IEnumerable<IEarleyGrammarRule> GetRulesWithLeft(IEarleyData left) {
            return this.rules.Where(rule => rule.IsDerivableFrom(left));
        }
    }
}
