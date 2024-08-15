class Progra
{

    public static void Main(string[] args)
    {
        string h = @"effect{Name: ""Damage"",Params: {Amount: Number}Action: (targets, context) => {for target in targets {i = 0;while (i++ < Amount)target.Power-=1;};}}";
        string h2 = @"effect {Name: ""Boost"",Params: {amount: Number,duration: Number}Action: (targets, context) =>{for target in targets {target.Power += amount;context.Board.Shuffle();};}}";
        string h3 = @"card {Type: ""Oro"",Name: ""Geralt"",Faction: ""Neutral"",Power: 15,Range: [""Melee""],OnActivation: [{Effect: ""Damage"",Amount: 10,Selector: {Source: ""deck"",Single: true,Predicate: (unit) => unit.Faction == ""Monster""}}]}";
        string h4 = @"card {Type: ""Oro"",Name: ""Triss"",Faction: ""Northern Realms"",Power: 12,Range: [""Ranged""],OnActivation: [{Effect: ""Draw"",Selector: {Source: ""deck"",Single: true,Predicate: (unit) => true}PostAction: {Type: ""Damage"",Amount: 5,Selector: {Source: ""deck"",Single: false,Predicate: (unit) => unit.Power < 5}}}]}";
        Scanner a = new Scanner(h2);
        List<Token> tokens = a.ScanToken();
        foreach (Token t2 in tokens)
        {

            Console.WriteLine(t2.type);
        }
        Parser parser = new Parser(tokens);
        Node effect = parser.Parse();
        Console.WriteLine("Parsed Effect:");
        effect.Print();
    }
}