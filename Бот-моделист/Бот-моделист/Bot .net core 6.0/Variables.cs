using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Бот_1
{

    internal class User
    {
        public bool photoSent;
        public string destinationFilePath;
        public string photoHashName;
        public string photoName;


        public User()
        {
            photoSent = false;
            destinationFilePath = "";
            photoHashName = "";
    }

    }



    internal static class Variables
    {
        public static List<string> png = "89 50 4E 47".Split().ToList();
        public static List<string> jpg = "FF D8 FF DB".Split().ToList();
        public static List<string> jpeg = "FF D8 FF E0".Split().ToList();

        public static Process photoshop;
  



        public static ReplyKeyboardMarkup DefaultKeyboard = new(
                            new[]
                            {
                                new KeyboardButton[] { "Просто кнопка"},
                                new KeyboardButton[] { "Помощь"},
                            })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup YesOrNoKeyboard = new(
                            new[]
                            {
                                new KeyboardButton[] { "Да"},
                                new KeyboardButton[] { "Нет"},
                            })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup EditOptionsKeyboard = new(
                            new[]
                            {
                                new KeyboardButton[] { "Обработка светлого танка"},
                                new KeyboardButton[] { "Обработка тёмного танка"},
                                new KeyboardButton[] { "Отмена"},
                            })
        {
            ResizeKeyboard = true
        };



        public static Dictionary<long, User> usersDict = new Dictionary<long, User>();

    }



}