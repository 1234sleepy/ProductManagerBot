using _RegisterBot;
using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data.Entities;

string token = "";

RegisterBot bot = new RegisterBot(token);

bot.Start();
bot.GetStatus();
Console.ReadLine();

