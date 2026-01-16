
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using momoBot.other;
using System;
using System.Threading.Tasks;

namespace momoBot.commands
{
    public class testCommands : BaseCommandModule
    {

        //✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦//
        //status check   

        [Command("ping")]
        public async Task pingPong(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong!");
        }


        //✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦//
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
                    Color = DiscordColor.Lilac
                };

                await ctx.Channel.SendMessageAsync(embed: message);
            }
            catch (Exception)
            {
                await ctx.RespondAsync("I couldn't find a user with that ID.");
            }
        }


        //✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦ . 　⁺ 　 . ✦//
        //Gambling


        [Command("gamba")]
        public async Task cardGame(CommandContext ctx)
        {
            var userCard = new cardSystem();

            var userCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"your card is {userCard.selectedCard}",
                Color = DiscordColor.Lilac
            };

            await ctx.Channel.SendMessageAsync(embed: userCardEmbed);

            var botCard = new cardSystem();

            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"my card is {botCard.selectedCard}",
                Color = DiscordColor.Lilac
            };

            await ctx.Channel.SendMessageAsync(embed: botCardEmbed);

            if (userCard.selectedNumber > botCard.selectedNumber)
            {
                var winEmbed = new DiscordEmbedBuilder
                {
                    Title = "you win!",
                    Color = DiscordColor.Green
                };
                await ctx.Channel.SendMessageAsync(embed: winEmbed);
            }
            else if (userCard.selectedNumber < botCard.selectedNumber)
            {
                var loseEmbed = new DiscordEmbedBuilder
                {
                    Title = "i win!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: loseEmbed);
            }
            else
            {
                var tieEmbed = new DiscordEmbedBuilder
                {
                    Title = "its a tie!",
                    Color = DiscordColor.Orange
                };
                await ctx.Channel.SendMessageAsync(embed: tieEmbed);
            }
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











