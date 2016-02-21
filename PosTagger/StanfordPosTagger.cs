using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.ling.tokensregex;
using edu.stanford.nlp.tagger.maxent;
using java.io;
using java.util;
using Console = System.Console;

namespace PosTagger {
    public class StanfordPosTagger {
        private MaxentTagger tagger;

        public StanfordPosTagger() {
            var jarRoot = @"d:\code\NLP\Tests\Test0\lib\stanford-postagger-full-2015-01-30";
            var modelsDirectory = jarRoot + @"\models";

            // Loading POS Tagger
            tagger = new MaxentTagger(modelsDirectory + @"\french.tagger");
        }

        private static IDictionary<string, string> Mapping = new Dictionary<string, string> {
            {"NC", "N"},
        }; 

        private string TranslatePartOfSpeech(string partOfSpeech) {
            string newPos;

            if (!Mapping.TryGetValue(partOfSpeech, out newPos)) {
                newPos = partOfSpeech;
            }

            return newPos;
        }

        public IEnumerable<ISentence> Tag(string text) {
            // Text for tagging
            //var text = "Lorie est jolie. Elle est aussi très intelligente. Je l'aime.";

            var sentences = MaxentTagger.tokenizeText(new StringReader(text)).toArray();

            var list = new List<Sentence>();

            foreach (ArrayList sentence in sentences) {
                var taggedSentence = tagger.tagSentence(sentence);
                var coreLabels = edu.stanford.nlp.ling.Sentence.toCoreLabelList(taggedSentence);
                var tokens = new List<Token>();

                var iterator = coreLabels.listIterator();

                while (iterator.hasNext()) {
                    var label = (CoreLabel)iterator.next();

                    tokens.Add(new Token {
                        Word = label.value(),
                        StartPosition = label.beginPosition(),
                        EndPosition = label.endPosition(),
                        PartOfSpeech = this.TranslatePartOfSpeech(label.tag()),
                    });
                }

                list.Add(new Sentence(tokens));

                //Console.WriteLine(Sentence.listToString(taggedSentence, false));
            }

            return list;
        }
    }
}
