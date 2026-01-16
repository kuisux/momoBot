using DSharpPlus;
using DSharpPlus.CommandsNext;
using momoBot.commands;
using momoBot.config;
using System.Threading.Tasks;

namespace momoBot
{
    internal class Program
    {
        private static DiscordClient client { get; set; }
        private static CommandsNextExtension commands { get; set; }
        static async Task Main(string[] args)
        {
            var jsonReader = new jsonReader();
            await jsonReader.readJson();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            client = new DiscordClient(discordConfig);

            client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { jsonReader.prefix },
                EnableDms = true,
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            commands = client.UseCommandsNext(commandsConfig);
            commands.RegisterCommands<testCommands>();

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}


/*
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡿⢦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡏⢀⠉⠳⠦⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣇⣂⣌⣐⢀⠂⡙⠳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⣀⣤⠿⡛⣛⠛⡟⠻⢿⣿⣤⡠⠸⣧⠀⠀⠀⠀⠀⢀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢀⣤⠞⠭⢊⡒⡱⢨⠱⢌⢓⠢⣗⢎⠿⣷⣼⣆⣠⡴⠞⠛⡉⢉⠉⡉⠛⠛⠶⣤⣀⠀⠀
⠀⠀⠀⣠⡞⠁⠀⣌⠱⢌⡡⢃⠞⡨⢌⣳⢍⣮⣷⡾⠟⡟⢻⠛⠿⣳⢶⣤⣆⡄⠡⠈⠄⠠⢉⣷⠂
⠀⠀⣰⢯⠀⡄⢪⢄⢫⠰⡡⢍⡒⡱⣬⣷⢟⠫⣁⠖⡩⢌⡑⢎⡱⢂⢯⡜⣹⢻⣶⣅⢨⣰⠞⠁⠀
⠀⢰⣏⢆⠣⠜⣡⢊⡔⠣⠜⢢⢼⣼⡟⠒⢎⡱⢌⢒⡱⠌⡜⢂⠖⡩⡘⣮⢱⢣⠞⡽⣿⠁⠀⠀⠀
⠀⣿⡘⠤⢋⡜⡐⠦⢌⢣⡙⣼⠿⠊⠀⡜⡐⢆⠎⡒⢤⠋⡴⢉⠲⢡⡑⣏⢎⢧⡛⡴⣛⣷⠀⠀⠀
⢀⡇⠣⢍⠲⡐⡍⣒⡉⠦⣼⠯⠃⢀⢊⠴⣉⠲⢌⡱⢊⡜⢰⠩⣘⢡⠒⣏⠞⣦⡙⢶⡡⢿⡆⠀⠀
⠈⡇⢃⢎⡱⢘⡰⠡⡜⣱⡟⡡⢔⡡⢎⠒⣌⠲⢡⠒⡥⡘⢆⠓⡌⠦⣹⢭⡚⡴⣙⠦⣝⢺⡧⠀⠀
⠀⣿⡌⡒⢬⢡⢒⡱⠌⣿⡅⠳⡨⢔⢊⡱⢂⡍⠦⢩⠔⣑⢊⡱⢌⡑⣏⠶⣙⠶⣩⠞⣬⢻⡇⠀⠀
⠀⢸⣧⢉⠖⠢⣅⠲⡉⢆⡜⣡⢑⡊⠦⣑⢊⠴⣉⠲⡘⠤⣃⠲⢌⡼⢎⡳⣍⠞⣥⢛⡴⣹⠃⠀⠀
⠀⠀⢻⡎⡜⡡⢆⢣⢉⢆⠲⢄⠣⠜⡡⢆⡩⢒⢌⡱⢌⢃⠦⣉⡶⣙⢮⡱⢎⡝⢦⣋⢶⡏⠀⠀⠀   `7MMpMMMb.pMMMb.  ,pW"Wq.`7MMpMMMb.pMMMb.  ,pW"Wq.  
⠀⠀⠀⢻⣖⡡⢎⠢⢍⠢⡍⡜⢌⢣⡑⠦⣑⠪⠔⡒⡌⢎⡴⣋⠶⣩⢖⡹⢎⡜⣣⢾⡟⠀⠀⠀⠀     MM    MM    MM 6W'   `Wb MM    MM    MM 6W'   `Wb 
⠀⠀⠀⠀⠹⣶⡡⠍⡆⡓⠴⡘⡌⠦⣘⠒⡤⢋⡜⣡⡜⢮⠳⣍⢞⡱⢎⡵⢫⣜⣵⠋⠀⠀⠀⠀⠀     MM    MM    MM 8M     M8 MM    MM    MM 8M     M8 
⠀⠀⠀⠀⠀⠘⢿⣞⡴⢭⣒⣡⢎⡑⢦⡍⣖⢣⡝⢦⣙⢎⡳⡜⢮⡱⢫⣼⡵⠟⠁⠀⠀⠀⠀⠀⠀     MM    MM    MM 8M     M8 MM    MM    MM 8M     M8 
⠀⠀⠀⠀⠀⠀⠀⠙⢷⣇⡞⡴⢫⡜⣣⠞⣬⠳⣜⢣⠞⣬⣓⣝⣦⡿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀      MM    MM    MM YA.    A9 MM    MM    MM YA.    A9 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢷⣭⣷⣼⣥⣿⣶⠿⠾⠟⠛⠛⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀.JMML  JMML  JMML.`Ybmd9'.JMML  JMML  JMML.`Ybmd9'    BY KUISUX
*/
