
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using momoBot.other;
using System;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;

namespace momoBot.commands
{
    public class testCommands : BaseCommandModule
    {

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //status check   

        [Command("ping")]
        public async Task pingPong(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong!");
        }


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //pfp Grabber      

        [Command("pfp")]
        public async Task pfpGrabber(CommandContext ctx, ulong userId)
        {
            try
            {
                var user = await ctx.Client.GetUserAsync(userId);
                string avatarUrl = user.GetAvatarUrl(DSharpPlus.ImageFormat.Auto, 1024);

                var message = new DiscordEmbedBuilder
                {
                    Title = $"Here is the profile picture of **{user.Username}**",
                    ImageUrl = avatarUrl,
                    Color = new DiscordColor("#FFCCBB")
                };

                await ctx.Channel.SendMessageAsync(embed: message);
            }
            catch (Exception)
            {
                await ctx.RespondAsync("I couldn't find a user with that ID.");
            }
        }


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //Gambling


        [Command("gamba")]
        public async Task cardGame(CommandContext ctx)
        {
            var userCard = new cardSystem();

            var userCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"your card is {userCard.selectedCard}",
                Color = new DiscordColor("#FFCCBB")
            };

            await ctx.Channel.SendMessageAsync(embed: userCardEmbed);

            var botCard = new cardSystem();

            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"my card is {botCard.selectedCard}",
                Color = new DiscordColor("#FFCCBB")
            };

            await ctx.Channel.SendMessageAsync(embed: botCardEmbed);

            if (userCard.selectedNumber > botCard.selectedNumber)
            {
                var winEmbed = new DiscordEmbedBuilder
                {
                    Title = "you win!",
                    Color = new DiscordColor("#C1E1C1")
                };
                await ctx.Channel.SendMessageAsync(embed: winEmbed);
            }
            else if (userCard.selectedNumber < botCard.selectedNumber)
            {
                var loseEmbed = new DiscordEmbedBuilder
                {
                    Title = "i win!",
                    Color = new DiscordColor("#FF6655")
                };
                await ctx.Channel.SendMessageAsync(embed: loseEmbed);
            }
            else
            {
                var tieEmbed = new DiscordEmbedBuilder
                {
                    Title = "its a tie!",
                    Color = new DiscordColor("FF8833")
                };
                await ctx.Channel.SendMessageAsync(embed: tieEmbed);
            }
        }

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

        [Command("idk")]
        public async Task idkCommand(CommandContext ctx)
        {
            var interactivity = Program.client.GetInteractivity();

            var messageToRetrieve = await interactivity.WaitForMessageAsync(message => message.Content == "hello");

            if (messageToRetrieve.Result.Content == "hello")
            {
                await ctx.Channel.SendMessageAsync($"You said: {messageToRetrieve.Result.Content}");
            }
        }

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

        [Command("roll")]
        public async Task rollCommand(CommandContext ctx, int max)
        {
            try
            {
                Random rnd = new Random();
                int result = rnd.Next(1, max + 1);

                var rollEmbed = new DiscordEmbedBuilder
                {
                    Title = $"🎲 You rolled a {result}",
                    Color = new DiscordColor("#FFCCBB")
                };
                await ctx.Channel.SendMessageAsync(embed: rollEmbed);
            }
            catch (Exception)
            {
                await ctx.Channel.SendMessageAsync("Please provide a valid maximum number greater than 1.");
            }

        }
    }
}


/*
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡿⢦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡏⢀⠉⠳⠦⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣇⣂⣌⣐⢀⠂⡙⠳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⣀⣤⠿⡛⣛⠛⡟⠻⢿⣿⣤⡠⠸⣧⠀⠀⠀⠀⠀⢀⣀⣀
⠀⠀⠀⠀⢀⣤⠞⠭⢊⡒⡱⢨⠱⢌⢓⠢⣗⢎⠿⣷⣼⣆⣠⡴⠞⠛⡉⢉⠉⡉⠛⠛⠶⣤⣀⠀
⠀⠀⠀⣠⡞⠁⠀⣌⠱⢌⡡⢃⠞⡨⢌⣳⢍⣮⣷⡾⠟⡟⢻⠛⠿⣳⢶⣤⣆⡄⠡⠈⠄⠠⢉⣷⠂
⠀⠀⣰⢯⠀⡄⢪⢄⢫⠰⡡⢍⡒⡱⣬⣷⢟⠫⣁⠖⡩⢌⡑⢎⡱⢂⢯⡜⣹⢻⣶⣅⢨⣰⠞⠁
⠀⢰⣏⢆⠣⠜⣡⢊⡔⠣⠜⢢⢼⣼⡟⠒⢎⡱⢌⢒⡱⠌⡜⢂⠖⡩⡘⣮⢱⢣⠞⡽⣿
⠀⣿⡘⠤⢋⡜⡐⠦⢌⢣⡙⣼⠿⠊⠀⡜⡐⢆⠎⡒⢤⠋⡴⢉⠲⢡⡑⣏⢎⢧⡛⡴⣛⣷
⢀⡇⠣⢍⠲⡐⡍⣒⡉⠦⣼⠯⠃⢀⢊⠴⣉⠲⢌⡱⢊⡜⢰⠩⣘⢡⠒⣏⠞⣦⡙⢶⡡⢿⡆
⠈⡇⢃⢎⡱⢘⡰⠡⡜⣱⡟⡡⢔⡡⢎⠒⣌⠲⢡⠒⡥⡘⢆⠓⡌⠦⣹⢭⡚⡴⣙⠦⣝⢺⡧
⠀⣿⡌⡒⢬⢡⢒⡱⠌⣿⡅⠳⡨⢔⢊⡱⢂⡍⠦⢩⠔⣑⢊⡱⢌⡑⣏⠶⣙⠶⣩⠞⣬⢻⡇
⠀⢸⣧⢉⠖⠢⣅⠲⡉⢆⡜⣡⢑⡊⠦⣑⢊⠴⣉⠲⡘⠤⣃⠲⢌⡼⢎⡳⣍⠞⣥⢛⡴⣹⠃
⠀⠀⢻⡎⡜⡡⢆⢣⢉⢆⠲⢄⠣⠜⡡⢆⡩⢒⢌⡱⢌⢃⠦⣉⡶⣙⢮⡱⢎⡝⢦⣋⢶⡏⠀⠀⠀   `7MMpMMMb.pMMMb.  ,pW"Wq.`7MMpMMMb.pMMMb.  ,pW"Wq.  
⠀⠀⠀⢻⣖⡡⢎⠢⢍⠢⡍⡜⢌⢣⡑⠦⣑⠪⠔⡒⡌⢎⡴⣋⠶⣩⢖⡹⢎⡜⣣⢾⡟⠀⠀⠀⠀     MM    MM    MM 6W'   `Wb MM    MM    MM 6W'   `Wb 
⠀⠀⠀⠀⠹⣶⡡⠍⡆⡓⠴⡘⡌⠦⣘⠒⡤⢋⡜⣡⡜⢮⠳⣍⢞⡱⢎⡵⢫⣜⣵⠋⠀⠀⠀⠀⠀     MM    MM    MM 8M     M8 MM    MM    MM 8M     M8 
⠀⠀⠀⠀⠀⠘⢿⣞⡴⢭⣒⣡⢎⡑⢦⡍⣖⢣⡝⢦⣙⢎⡳⡜⢮⡱⢫⣼⡵⠟⠁⠀⠀⠀⠀⠀⠀     MM    MM    MM 8M     M8 MM    MM    MM 8M     M8 
⠀⠀⠀⠀⠀⠀⠀⠙⢷⣇⡞⡴⢫⡜⣣⠞⣬⠳⣜⢣⠞⣬⣓⣝⣦⡿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀      MM    MM    MM YA.    A9 MM    MM    MM YA.    A9 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢷⣭⣷⣼⣥⣿⣶⠿⠾⠟⠛⠛⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀.JMML  JMML  JMML.`Ybmd9'.JMML  JMML  JMML.`Ybmd9'    BY KUISUX
*/











