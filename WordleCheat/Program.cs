
using WordleCheat;
using WordleCheat.interfaces;
using WordleCheat.rules;
using Autofac;

Console.WriteLine("Starting...");

ContainerBuilder containerBuilder = new ContainerBuilder();
containerBuilder.RegisterType<ScrabbleLetterValueProvider>().As<ILetterValueProvider>().SingleInstance();
containerBuilder.RegisterType<DictionaryFileReader>().As<IDictionaryFileReader>().SingleInstance();
containerBuilder.RegisterType<Wordlist>().As<IWordlist>().SingleInstance();
containerBuilder.RegisterType<WordlistItem>().As<IWordlistItem>();
containerBuilder.RegisterType<EmptyWordRuleInvoker>().Named<IRuleInvoker>("emptyWord");
containerBuilder.RegisterType<StandardRuleInvoker>().Named<IRuleInvoker>("standard");
containerBuilder.RegisterType<OutputFormatter>().AsSelf();
containerBuilder.RegisterType<KnownLettersInput>().As<IKnownLettersInput>();
var container = containerBuilder.Build();

var wordList = container.Resolve<IWordlist>();

var emptyWordRuleInvoker = container.ResolveNamed<IRuleInvoker>("emptyWord");
var standardRuleInvoker = container.ResolveNamed<IRuleInvoker>("standard");
var outputFormatter = container.Resolve<OutputFormatter>();
var knownLettersInput = container.Resolve<IKnownLettersInput>();

var startingGuesses = wordList.ChooseNextWord(knownLettersInput, "", emptyWordRuleInvoker);

Console.WriteLine($"Here are some of your best starting options:");
outputFormatter.DisplayOutput(startingGuesses);

wordList.UnhideAllWords();

do
{
    Console.Write("What was your next guess? ");
    string guess = Console.ReadLine() ?? "";

    Console.Write("What was the result of that guess? (Green=UPPERCASE,Orange=lowercase,Gray=underscore)");
    knownLettersInput.SetKnownInput(Console.ReadLine() ?? "");

    var result = wordList.ChooseNextWord(knownLettersInput, guess, standardRuleInvoker);
    Console.WriteLine($"Here are some of your best next guesses:");
    outputFormatter.DisplayOutput(result);
} while (true);


