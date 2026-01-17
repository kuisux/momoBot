
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using momoBot.other;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace momoBot.commands
{
    public class testCommands : BaseCommandModule
    {

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //status check   

        [Command("ping")]
        [Description("Check if the bot is online.")]
        public async Task pingPong(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong!");
        }

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //ban

        [Command("banish")]
        [Aliases("ban")]
        [Description("Ban a user from the server.")]
        public async Task banUser(CommandContext ctx, DiscordMember member, [Description("Reason for the ban.")] string reason = "No reason provided.")
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.BanMembers))
            {
                try
                {
                    await member.BanAsync(0, reason);
                    await ctx.Channel.SendMessageAsync($"{member.DisplayName} has been banned. Reason: {reason}");
                }
                catch (Exception)
                {
                    await ctx.Channel.SendMessageAsync("I couldn't ban that user. Check my perms and the user ID");
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("You do not have permission to ban members.");
            }
        }


        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //kick

        [Command ("kick")]
        [Aliases("boot")]
        [Description("Kick a user from the server.")]

        public async Task kickUser(CommandContext ctx, DiscordMember member, [Description("Reason for the kick.")] string reason = "No reason provided.")
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.KickMembers))
            {
                try
                {
                    await member.RemoveAsync(reason);
                    await ctx.Channel.SendMessageAsync($"{member.DisplayName} has been kicked. Reason: {reason}");
                }
                catch (Exception)
                {
                    await ctx.Channel.SendMessageAsync("I couldn't ban that user. Check my perms and the user ID");
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("You do not have permission to kick members.");
            }
        }



        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //pfp Grabber      

        [Command("pfp")]
        [Description("Get the profile picture of a user by their user ID.")]
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
        [Description("Play a simple card game against the bot. Draw a card each, higher card wins")]
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
        //dice

        [Command("roll")]
        [Aliases("d")]
        [Description("Roll a dice with a specified maximum number.")]
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

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //compatibility smoothie

        [Command("smoothie")]
        [Aliases("compatability")]
        [Description("Check the compatibility between two users.")]
        public async Task SmoothieCommand(CommandContext ctx, DiscordMember member1, DiscordMember member2 = null)
        {
            DiscordMember first = member2 == null ? ctx.Member : member1;
            DiscordMember second = member2 == null ? member1 : member2;

            string id1 = first.Id.ToString();
            string id2 = second.Id.ToString();
            string combinedIds = string.Compare(id1, id2) < 0 ? id1 + id2 : id2 + id1;

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8 .GetBytes(combinedIds));
                int hashValue = BitConverter.ToInt32(hashBytes, 0);
                int percentage = Math.Abs(hashValue % 101);
                string hearts = string.Concat(Enumerable.Repeat("❤️", percentage / 10));
                string empty = string.Concat(Enumerable.Repeat("🖤", 10 - (percentage / 10)));
                string progressBar = hearts + empty;

                var embed = new DiscordEmbedBuilder
                {
                    Title = "🥤 Smoothie Compatibility",
                    Description = $"**{first.DisplayName}** + **{second.DisplayName}**\n`{progressBar}`\n**{percentage}%**",
                    Color = new DiscordColor("#FFCCBB"),
                };
                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}/*
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
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢷⣭⣷⣼⣥⣿⣶⠿⠾⠟⠛⠛⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀.JMML  JMML  JMML.`Ybmd9'.JMML  JMML  JMML.`Ybmd9'    BY KUISUX */











