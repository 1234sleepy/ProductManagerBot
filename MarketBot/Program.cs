using _RegisterBot;

string token = "";

RegisterBot bot = new RegisterBot(token);

bot.Start();
bot.GetStatus();
Console.ReadLine();