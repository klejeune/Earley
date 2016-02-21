using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.DataTypes.Regular;
using EarleyParser.DataTypes.TagGrammar;
using EarleyParser.EarleyBase;
using EarleyParser.PartOfSpeech;
using EarleyParser.PartOfSpeech.Talismane;
using PosTagger;

namespace EarleyParser {
    class Program {
        static void Parse(Grammar grammar, EarleyParser parser, string input) {
            Console.WriteLine("Parsing: " + input);

            if (parser.Parse(grammar, input.Select(c => new RegularEarleyInput(c.ToString())).ToArray())) {
                Console.WriteLine("SUCCESS");
            }
            else {
                Console.WriteLine("FAILURE");
            }
        }

        static void ParseTag(Grammar grammar, string input) {
            Console.WriteLine("Parsing: " + input);

            //var sentence = stanfordTagger.Tag(input).Single();
            var sentence = talismane.Send(input);
            var earleyInput = ConvertToEarleyInput(sentence);

            Console.WriteLine(string.Join(" ", earleyInput.Select(i => i.PartOfSpeech)));

            if (parser.Parse(grammar, earleyInput)) {
                Console.WriteLine("SUCCESS");
            }
            else {
                Console.WriteLine("FAILURE");
            }
        }

        static void TestRegular() {
            var grammar = new Grammar();
            AddRule(grammar, "P", "S", true);
            AddRule(grammar, "S", "S + M");
            AddRule(grammar, "S", "M");
            AddRule(grammar, "M", "M * T");
            AddRule(grammar, "M", "T");
            AddRule(grammar, "T", "1");
            AddRule(grammar, "T", "2");
            AddRule(grammar, "T", "3");
            AddRule(grammar, "T", "4");

            var parser = new EarleyParser();

            Parse(grammar, parser, "2+3*4");
            Parse(grammar, parser, "2+3*");
            Parse(grammar, parser, "2+3");
            Parse(grammar, parser, "2+");
            Parse(grammar, parser, "2");
        }

        private static EarleyParser parser = new EarleyParser();
        private static Talismane talismane = new Talismane();
        private static PosTagger.StanfordPosTagger stanfordTagger = new PosTagger.StanfordPosTagger();
        
        private static void TestTag() {
            var grammar = new Grammar();
            var builder = new TagGrammarFactory();

            grammar.AddRule(
                new TagTree(
                    "nx0V",
                    builder.Root(
                        "S",
                        builder.Substitution(
                            "N"),
                        builder.Node(
                            "VP",
                            builder.Node(
                                "V",
                                builder.Anchor())))));

        

            //grammar.AddRule(
            //    new TagTree(
            //        "nx0Vnx1",
            //        builder.Root(
            //            "S",
            //            builder.Substitution(
            //                "N"),
            //            builder.Node(
            //                "VP",
            //                builder.Node(
            //                    "V",
            //                    builder.Anchor()),
            //                builder.Substitution(
            //                    "N")))));

            grammar.AddRule(
             new TagTree(
                 "",
                 builder.Root(
                     "N",
                     builder.Node("NC", builder.Anchor()))));

            grammar.AddRule(
                new TagTree(
                    "NX",
                    builder.Root(
                        "N",
                        builder.Anchor())));

            grammar.AddRule(
                new TagTree(
                    "BDETnx",
                    builder.Root(
                        "N",
                        builder.Node(
                            "DET",
                            builder.Anchor()),
                        builder.Adjunction(
                            "N")
                        )));

            grammar.AddRule(
                new TagTree(
                    "BvxADV",
                    builder.Root(
                        "VP",
                        builder.Adjunction(
                            "VP"),
                        builder.Node(
                            "ADV",
                            builder.Anchor()))));

            //ParseTag(grammar, "le chat");
            //ParseTag(grammar, "chat mange souvent");
            ParseTag(grammar, "le chat mange souvent");
            //ParseTag(grammar, "chat mange des souris");
            //ParseTag(grammar, "chat mange souvent souris");
            ParseTag(grammar, "le chat construit souvent des souris.");
            //ParseTag(grammar, "chat mange");
            //ParseTag(grammar, "chat");
            //ParseTag(grammar, "chat mange souris");
            //ParseTag(grammar, "le chat mange des souris souvent");
            //ParseTag(grammar, "chat mange chat");
            //ParseTag(grammar, "chat mange mange");
            //ParseTag(grammar, "mange");
        }

        static LexicalItem[] ConvertToEarleyInput(ISentence sentence) {
            return sentence.Tokens.Select(t => new LexicalItem(t.Word, t.PartOfSpeech)).ToArray();
        }
        static LexicalItem[] ConvertToEarleyInput(IEnumerable<TalismaneAnswer> sentence) {
            return sentence.Select(t => new LexicalItem(t.Word, t.Tag)).ToArray();
        }
        
        static void Main(string[] args) {
            //TestRegular();
            TestTag();
        }

        static void AddRule(Grammar grammar, string left, string right, bool isStartRule = false) {
            var newRight = right.Split(' ').Select(r => new RegularEarleyData(r)).Cast<IEarleyData>().ToList();

            var rule = new RegularGrammarRule {
                Left = new RegularEarleyData(left),
                Right = newRight,
                IsStartRule = isStartRule
            };

            grammar.AddRule(rule);
        }

        //static void AddTagRule(Grammar grammar, string left, string right, bool isStartRule = false) {
        //    var newRight = right.Split(' ').Select(r => new TagTree()).Cast<IEarleyData>().ToList();

        //    var rule = new RegularGrammarRule {
        //        Left = new RegularEarleyData(left),
        //        Right = newRight,
        //    };

        //    grammar.AddRule(rule, isStartRule);
        //}
    }
}
