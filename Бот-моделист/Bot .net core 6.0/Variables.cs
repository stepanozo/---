using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
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
            FilterChoice = 4,
            NationChoice = 5,
            DifficultyChoice = 6,
            ClassChoice = 7


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
                                new KeyboardButton[] { "Инфо"},
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

        public static ReplyKeyboardMarkup FilterChoice = new(
                      new[]
                      {
                           new KeyboardButton[] { "По нации",},
                           new KeyboardButton[] { "По классу" },
                           new KeyboardButton[] { "По сложности"},
                           new KeyboardButton[] { "Отмена"},
                      })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup NationChoice = new(
                      new[]
                      {
                           new KeyboardButton[] { "СССР", "Германия", "США",},
                           new KeyboardButton[] { "Франция", "Великобритания", "Швеция" },
                           new KeyboardButton[] { "Италия", "Чехословакия", "Польша"},
                           new KeyboardButton[] { "Япония", "Китай", "Отмена"},
                      })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup ClassChoice = new(
                     new[]
                     {
                           new KeyboardButton[] { "Лёгкие танки", "Средние танки"},
                           new KeyboardButton[] { "Тяжёлые танки", "ПТ-САУ" },
                           new KeyboardButton[] { "САУ", "Диорамы"},
                           new KeyboardButton[] { "Отмена"},
                     })
        {
            ResizeKeyboard = true
        };



        public static InlineKeyboardMarkup USSR = new(new[]
                    {
                        new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "КВ-1Э", callbackData: "КВ-1Э"),
                           InlineKeyboardButton.WithCallbackData(text: "КВ-1", callbackData: "КВ-1"),
                           InlineKeyboardButton.WithCallbackData(text: "КВ-1 простой", callbackData: "КВ-1 простой")
                        },
                        new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Т-10М", callbackData: "Т-10М"),
                           InlineKeyboardButton.WithCallbackData(text: "Т-10М простой", callbackData: "Т-10М простой"),
                           InlineKeyboardButton.WithCallbackData(text: "С-51", callbackData: "С-51")
                        },
                          new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Т-100-ЛТ", callbackData: "Т-100-ЛТ"),
                           InlineKeyboardButton.WithCallbackData(text: "СУ-85 (СУ-122)", callbackData: "СУ-85 (СУ-122)"),
                           InlineKeyboardButton.WithCallbackData(text: "СУ-100", callbackData: "СУ-100")
                        },
                           new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Гоночный БТ-СВ", callbackData: "Гоночный БТ-СВ"),
                           InlineKeyboardButton.WithCallbackData(text: "БТ-СВ)", callbackData: "БТ-СВ"),
                           InlineKeyboardButton.WithCallbackData(text: "БТ-СВ простой", callbackData: "БТ-СВ простой")
                        },
                            new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Объект 704", callbackData: "Объект 704"),
                           InlineKeyboardButton.WithCallbackData(text: "СУ-122-54", callbackData: "СУ-122-54"),
                           InlineKeyboardButton.WithCallbackData(text: "Т-44-85/122", callbackData: "Т-44-85/122")
                        },
                             new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "ИСУ-122", callbackData: "ИСУ-122"),
                           InlineKeyboardButton.WithCallbackData(text: "Т-44-100", callbackData: "Т-44-100"),
                           InlineKeyboardButton.WithCallbackData(text: "СУ-18", callbackData: "СУ-18")
                        },
                              new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "СУ-122-44", callbackData: "СУ-122-44"),
                           InlineKeyboardButton.WithCallbackData(text: "А-44", callbackData: "А-44"),
                           InlineKeyboardButton.WithCallbackData(text: "ИС-4М", callbackData: "ИС-4М")
                        },
                                 new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "ИС-4М сложный", callbackData: "ИС-4М сложный"),
                           InlineKeyboardButton.WithCallbackData(text: "ИС-2", callbackData: "ИС-2"),
                           InlineKeyboardButton.WithCallbackData(text: "ИС-2 простой", callbackData: "ИС-2 простой")
                        },
                                    new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Т-34-85", callbackData: "Т-34-85"),
                           InlineKeyboardButton.WithCallbackData(text: "КВ-122", callbackData: "КВ-122"),
                           InlineKeyboardButton.WithCallbackData(text: "КВ-122 простой", callbackData: "КВ-122 простой")
                        },
                                      new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "«Панцер 50» трофейный", callbackData: "«Панцер 50» трофейный"),
                           InlineKeyboardButton.WithCallbackData(text: "Бронешары", callbackData: "Бронешары"),
                           InlineKeyboardButton.WithCallbackData(text: "Т-54", callbackData: "Т-54")
                        },
                                     new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Т-54 простой", callbackData: "Т-54 простой"),
                           InlineKeyboardButton.WithCallbackData(text: "Т-70 (Т-80)", callbackData: "Т-70 (Т-80)"),
                           InlineKeyboardButton.WithCallbackData(text: "ИСУ-152", callbackData: "ИСУ-152")
                        },
                                     new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "ИС-3-БН", callbackData: "ИС-3-БН"),
                           InlineKeyboardButton.WithCallbackData(text: "ИС-7", callbackData: "ИС-7"),
                           InlineKeyboardButton.WithCallbackData(text: "БТ-2", callbackData: "БТ-2")
                        },
                                   new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "Т-35А", callbackData: "Т-35А"),
                           InlineKeyboardButton.WithCallbackData(text: "Т-34", callbackData: "Т-34"),
                           InlineKeyboardButton.WithCallbackData(text: "КВ-5", callbackData: "КВ-5")
                        },
                                  new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "ИС-3", callbackData: "ИС-3"),
                           InlineKeyboardButton.WithCallbackData(text: "КВ-2", callbackData: "КВ-2"),
                           InlineKeyboardButton.WithCallbackData(text: "СУ-26", callbackData: "СУ-26")
                        },
                                   new[]
                        {
                           InlineKeyboardButton.WithCallbackData(text: "МС-1", callbackData: "МС-1"),
                           InlineKeyboardButton.WithCallbackData(text: "Пушка-гаубица МЛ-20", callbackData: "Пушка-гаубица МЛ-20"),
                           InlineKeyboardButton.WithCallbackData(text: "ИС-4М простой", callbackData: "ИС-4М простой"),
                        },




                    });








        public static Dictionary<long, User> usersDict = new Dictionary<long, User>();
    }

       
        

    



}