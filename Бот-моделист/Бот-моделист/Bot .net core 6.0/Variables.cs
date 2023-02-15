using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Http;

namespace Бот_1
{

    internal class User
    {
        public bool photoSent;
        public bool lightEditingRequest;
        public bool firstEditing;
        public string destinationFilePath;
        public string photoHashName;
        public string photoName;
        


        public User()
        {
            photoSent = false;
            lightEditingRequest = false;
            destinationFilePath = "";
            photoHashName = "";
            firstEditing = true;

    }

    }



    internal static class Variables
    {

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
                                new KeyboardButton[] { "Лёгкое редактирование"},
                                new KeyboardButton[] { "Отмена"},
                            })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup LightEditingKeyboard = new(
                            new[]
                            {
                                new KeyboardButton[] { "Осветлить фон" , "Повысить контрастность" },
                                new KeyboardButton[] { "Осветлить тени", "Смягчить блики"},
                                new KeyboardButton[] { "Повысить резкость", "Отмена"},
                            })
        {
            ResizeKeyboard = true
        };



        public static Dictionary<long, User> usersDict = new Dictionary<long, User>();

       
        

    }



}