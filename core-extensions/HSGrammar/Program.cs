﻿using Antlr4.Runtime;
using HSGrammar.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HSGrammar
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\admin\Source\Repos\SabberStone\core-extensions\HSGrammar\File\";
            Regex logPattern = new Regex(@"[D][ ][0-9]{2}[:][0-9]{2}[:][0-9]{2}[.][0-9]{7}[ ](GameState|PowerTaskList|PowerProcessor)[.](DebugDump|DebugPrintPower|DebugPrintPowerList|PrepareHistoryForCurrentTaskList|DebugPrintEntityChoices|EndCurrentTaskList|DebugPrintEntitiesChosen|SendChoices|SendOption|DebugPrintOptions|DoTaskListForCard)[\(][\)][ ][-][ ]*(.*)");
            var listLines = File.ReadAllLines(path + "Power.log").ToList();

            var gameStrs = new List<StringBuilder>();
            int index = 0;
            listLines.ForEach(p =>
            {
                Match logMatch = logPattern.Match(p);
                if (logMatch.Success)
                {
                    string logType1 = logMatch.Groups[1].Value;
                    string logType2 = logMatch.Groups[2].Value;
                    string logEntry = logMatch.Groups[3].Value;

                    if (logType1 == "GameState" && (logType2 == "DebugPrintPower" || logType2 == "DebugPrintEntityChoices" || logType2 == "SendChoices" || logType2 == "SendOption" || logType2 == "DebugPrintOptions"))
                    {
                        if (logEntry.StartsWith("CREATE_GAME"))
                            gameStrs.Add(new StringBuilder());

                        gameStrs.Last()?.AppendLine(logEntry);
                    }
                    else
                    {
                        // ignore
                    }
                }
                else
                {
                    Console.WriteLine("unsuccessful logMatch: " + p);
                    Console.ReadKey();
                }
            });

            // writing files
            gameStrs.ForEach(p => WriteFile(path + $"GameLog{gameStrs.IndexOf(p)}.log", p.ToString()));

            Console.WriteLine("Starting parsing process now! (Press key)");
            Console.ReadKey();

            gameStrs.ForEach(p => {
                try
                {
                    AntlrInputStream inputStream = new AntlrInputStream(p.ToString());
                    HSGrammarLexer hsLexer = new HSGrammarLexer(inputStream);
                    CommonTokenStream commonTokenStream = new CommonTokenStream(hsLexer);
                    HSGrammarParser hsParser = new HSGrammarParser(commonTokenStream);

                    HSGrammarParser.CompileUnitContext compileUnit = hsParser.compileUnit();
                    HsGrammarVisitor visitor = new HsGrammarVisitor();
                    PowerGame powerGame = visitor.Visit(compileUnit) as PowerGame;
                    WriteFile(path + $"GameLog{gameStrs.IndexOf(p)}.json", JsonConvert.SerializeObject(powerGame, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    Console.ReadKey();
                }

            });


            Console.WriteLine("Finished! (Press key)");
            Console.ReadKey();
        }

        private static void WriteFile(string filePath, string v)
        {
            Console.WriteLine(filePath);
            File.WriteAllText(filePath, v);
        }
    }
}
