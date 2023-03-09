using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
