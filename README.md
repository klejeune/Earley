# Earley parser
A C# implementation of an Earley parser working on Tree-adjoining grammars (working on natural languages)

# NLP
The Natural Language examples must use a Part-of-Speech Tagger. 

## Stanford POS Tagger
Download the Stanford POS Tagger at http://nlp.stanford.edu/software/tagger.html#Download

Make sure it is well referenced in the PosTagger class constructor.

In the examples, make sure you are using the StanfordPosTagger class to generate the ISentence objects.

## Talismane
Download Talismane at http://redac.univ-tlse2.fr/applications/talismane.html

Download a language pack like English or French at https://github.com/urieli/talismane/releases

Run Talismane with a command line like:
java -Xmx1G -jar talismane-core-2.4.7b.jar languagePack=frenchLanguagePack-2.4.4b.zip command=analyse mode=server encoding=UTF-8 port=7171

In the examples, make sure you are using the Talismane class to generate the ISentence objects.

# References
See:
- https://en.wikipedia.org/wiki/Earley_parser
- https://en.wikipedia.org/wiki/Tree-adjoining_grammar