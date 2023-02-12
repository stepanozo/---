using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Бот_1
{

    internal class User
    {
        public bool phraseRequest;
        public int gameLevel;
        public int trueAnswers;
        public string heroName;
        public string heroPhrase;
        public bool stopGameForReplicsQuestion;
        public bool continueGameQuestion;
        public bool restartGameQuestion;
        public bool continueReplicsQuestion;
        public bool stopReplicsForGameQuestion;



        public User()
        {
            phraseRequest = false;
            gameLevel = 0;
            trueAnswers = 0;
        }

    }



    internal static class Variables
    {

        public static ReplyKeyboardMarkup DefaultKeyboard = new(
                            new[]
                            {
                                new KeyboardButton[] { "Реплики", "Фото"},
                                new KeyboardButton[] { "Помощь", "Игра"},
                                new KeyboardButton[] { "Стоп"}
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

        public static ReplyKeyboardMarkup GameAndReplicsKeyboard = new(
                           new[]
                           {
                                new KeyboardButton[] { "Граф Морт", "Советник Гориус"},
                                new KeyboardButton[] { "Великус", "Хаз'Гуул Собиратель черепов"},
                                new KeyboardButton[] { "Болотный доктор", "Говард Альтеренас"},
                                new KeyboardButton[] { "Стоп"}
                           })
        {
            ResizeKeyboard = true
        };

        public static Dictionary<long, User> usersDict = new Dictionary<long, User>();

        public static string[] gameQuestions = { "Так значит, этот рычаг откроет дверь к твоей свободе, трус?", "Натрезимы - коварный демонический народ. Я совсем немного читал о них в нашей библиотеке. Они крайне опасны.", "Эти пещеры - развалины древнего города дворфов, но сейчас его населяют только жалкие кобольды, и нам нечего бояться.", "Не подходи, сова-переросток! Поверь, пожалеешь!", "Стоп! На нас будут нападать?!", "Что? Кто это там шастает по собору посреди ночи?", "Прошу вас ещё поразмыслить над предложением.", "Поверь, Говард. Начав эту битву, ты умрёшь. Я же великодушно предлагаю тебе сложить оружие и уйти.", "Пора свершиться правосудию света!", "Каждый выживает как может, служитель света. Каждый сам за себя." };
        public static string[] gameCorrectAnswers = { "Верно! Это он сказал Морту в пещере.", "Да, это был именно советник. Он пытался предупредить Морта об опасности, исходящей от Великуса.", "Да. Великус поразительно хорошо знает географию и историю Азерота.", "Именно. Совой-переростком позже оказался Великус...", "И снова реплика графа Морта!", "Да. Именно этой ночью советник слишком много узнал...", "Ну конечно! Не стоило графу принимать предложение Болотного Доктора...", "Да. Против Хаз'Гуула действительно сложно драться.", "И правосудие свершилось.", "Да, это был главный герой кампании" };
        public static string[] gameWrongAnswers = { "А вот и неправда.", "А вот и нет.", "Нет, этот ответ неправильный", "Неверно.", "Не-а.", "Ну уж нет.", "Ответ неправильный", "Нет, это произнёс другой персонаж.", "Нет, это неверно.", "Нет, неправильно" };
        public static string[] gameCorrectVariants = { "Говард Альтеренас", "Советник Гориус", "Великус", "Граф Морт", "Граф Морт", "Советник Гориус", "Болотный доктор", "Хаз'Гуул Собиратель черепов", "Говард Альтеренас", "Граф Морт" };
    }



}