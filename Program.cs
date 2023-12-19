using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// See https://aka.ms/new-console-template for more information


//var lineInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\n
//Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\n
//Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\n
//Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\n
//Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

var inputfile = @"C:\Users\SaLiVa\source\repos\AdventOfCode2023Day2\Input.txt";

CubeGame referenceGame = new CubeGame(12, 13, 14);
int result = 0;

using (StreamReader stringReader = File.OpenText(inputfile))
{
    while (true)
    {
        var line = stringReader.ReadLine();
        if (line != null)
        {
            CubeGame newGame = new CubeGame(line);
            Console.WriteLine(newGame.GameID + ": Red " + newGame.RedCubes + ", Green " + newGame.GreenCubes + ", Blue " + newGame.BlueCubes + ". Compare: " + newGame.Compare(referenceGame));

            //result += newGame.Compare(referenceGame);
            result += newGame.Power;
        }
        else { break; }
    }
};

Console.WriteLine(result.ToString());



public class CubeGame
{
    public CubeGame(string lineInput)
    {
        RedCubes = 0;
        GreenCubes = 0;
        BlueCubes = 0;

        string gameIDPattern = @"Game \d+";
        string redCubesPattern = @"\d+ red";
        string greenCubesPattern = @"\d+ green";
        string blueCubesPattern = @"\d+ blue";

        Match gameIDmatch = Regex.Match(lineInput, gameIDPattern);
        if (gameIDmatch.Success)
        {
            var gameIDnumber = Regex.Replace(gameIDmatch.Value, @"Game ", "");
            try { GameID = Int32.Parse(gameIDnumber); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
            
        Match redCubesmatch = Regex.Match(lineInput, redCubesPattern);
        while (redCubesmatch.Success)
        {
            var redcubecount = 0;
            var redcubenumber = Regex.Replace(redCubesmatch.Value, @" red", "");
            try { redcubecount = Int32.Parse(redcubenumber); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            if(redcubecount > RedCubes)
                RedCubes = redcubecount;
            redCubesmatch = redCubesmatch.NextMatch();
        }

        Match greenCubesmatch = Regex.Match(lineInput, greenCubesPattern);
        while (greenCubesmatch.Success)
        {
            var greencubecount = 0;
            var greencubenumber = Regex.Replace(greenCubesmatch.Value, @" green", "");
            try { greencubecount = Int32.Parse(greencubenumber); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            if (greencubecount > GreenCubes)
                GreenCubes = greencubecount;
            greenCubesmatch = greenCubesmatch.NextMatch();
        }

        Match blueCubesmatch = Regex.Match(lineInput, blueCubesPattern);
        while (blueCubesmatch.Success)
        {
            var bluecubecount = 0;
            var bluecubenumber = Regex.Replace(blueCubesmatch.Value, @" blue", "");
            try { bluecubecount = Int32.Parse(bluecubenumber); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            if (bluecubecount > BlueCubes)
                BlueCubes = bluecubecount;
            blueCubesmatch = blueCubesmatch.NextMatch();
        }
    }

    public CubeGame(int redCubes, int greenCubes, int blueCubes)
    {
        RedCubes = redCubes;
        GreenCubes = greenCubes;
        BlueCubes = blueCubes;
    }

    public int Compare (CubeGame referenceGame)
    {
        if (RedCubes > referenceGame.RedCubes || GreenCubes > referenceGame.GreenCubes || BlueCubes > referenceGame.BlueCubes)
            return 0;
        else
            return GameID;
    }

    public int GameID { get; private set; }
    public int RedCubes { get; private set; }
    public int GreenCubes { get; private set; }
    public int BlueCubes { get; private set; }

    public int Power { get { return RedCubes * GreenCubes * BlueCubes; } }
}