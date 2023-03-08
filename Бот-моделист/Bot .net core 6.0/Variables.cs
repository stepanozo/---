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
        public States state;
        public string photoHashName; //Имя фотографии на компьютере бота вместе с .jpg
        public string photoName; //Имя фотографии пользователя
        public string destinationFilePath; //Путь, по которому сохранена фотография пользователя вместе с именем фотки и .jpg
        public bool NewPhotoQuestion; //Когда задаётся вопрос о новой фотке, ставим на true;

        public enum States //Состояния, в которых может находиться пользователь
        {
            AfterStart = 0, //Когда пользователь зарегистрирован и у него дефолтная клавиатура выбора помощи или модельки
            DarkOrLight = 1, //Выбор между тёмной обработкой и светлой
            LightEditChoice = 2, //Пользователь выбирает, как ему отредактировать фотографию легкой обработкой
            ColorChoice = 3,
        }

        public User()
        {
            state = States.AfterStart;
            NewPhotoQuestion = false;
        }

    }



    internal static class Variables
    {

        public static Process photoshop; //Процесс, который сразу же будет запускать фотошоп прямо при старте бота.

        public static ReplyKeyboardMarkup DefaultKeyboard = new( //Для состояния AfterStart
                            new[]
                            {
                                new KeyboardButton[] { "Хочу модельку"},
                                new KeyboardButton[] { "Помощь"},
                            })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup DarkOrLightKeyboard = new( //Для состояния DarkOrLight
                           new[]
                           {
                                new KeyboardButton[] { "Обработка светлого танка"},
                                new KeyboardButton[] { "Обработка тёмного танка"},
                                new KeyboardButton[] { "Лёгкое редактирование", "Цветовой баланс"},
                                new KeyboardButton[] { "Откатить изменение" ,"Отмена"},
                           })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup NewPhotoOrNot = new( //Для  NewPhotoQuestion
                         new[]
                         {
                                new KeyboardButton[] { "Да"},
                                new KeyboardButton[] { "Нет"}
,
                         })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup LightEditChoice = new(
                       new[]
                       {
                           new KeyboardButton[] { "Осветлить фон", "Повысить контрастность" },
                           new KeyboardButton[] { "Осветлить тени", "Смягчить блики" },
                           new KeyboardButton[] { "Повысить резкость", "Откатить изменение", "Отмена" },
                       })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup ColorChoiceKeyboard = new(
                      new[]
                      {
                           new KeyboardButton[] { "Голубой", "Красный" },
                           new KeyboardButton[] { "Пурпурный", "Зелёный" },
                           new KeyboardButton[] { "Жёлтый", "Синий"},
                           new KeyboardButton[] { "Откатить изменение", "Отмена"},
                      })
        {
            ResizeKeyboard = true
        };






        public static Dictionary<long, User> usersDict = new Dictionary<long, User>();
    }

       
        

    



}