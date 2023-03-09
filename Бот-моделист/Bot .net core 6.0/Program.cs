using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;
using System.Diagnostics;
using System.Windows.Input;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Telegram.Bots.Requests;
using Telegram.Bots.Http;
using System.IO;
using Telegram.Bot.Types;
using Bot_.net_core_6._0;
using System.Text.RegularExpressions;
using System.Runtime.Versioning;

namespace Бот_1
{
    internal class Program
    {

        static void Main(string[] args)
        {


            var client = new TelegramBotClient("5690602086:AAFaP56k6_TTMWN-gmpH6loROE4JG12BAzg");
            Variables.photoshop = Process.Start(@"C:\Program Files\Adobe\Adobe Photoshop CS6 (64 Bit)\Photoshop.exe"); //@ нужно для использования недопустимых вещей в строке

            client.StartReceiving(Update, Error); //этих двух методов нет
            Console.ReadLine();
        }

        static async Task StepBackward(ITelegramBotClient botClient, long chatId, ReplyKeyboardMarkup keyboardAfterProcess)
        {

            if (System.IO.File.Exists(@"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName + "old.jpg"))
            {
                System.IO.File.Delete(@"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName);   //Удалили новый файл, старый old переименовали в новый.
                System.IO.File.Move(@"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName + "old.jpg", @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName);
                string editedFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName;
                await using Stream stream = System.IO.File.OpenRead(editedFilePath); //Открываем поток на файл без old и отправляем пользователю. Потом поток закрываем.
                await botClient.SendDocumentAsync(chatId, new InputOnlineFile(stream, Variables.usersDict[chatId].photoName.Replace(".jpg", " (edited).jpg")), replyMarkup: keyboardAfterProcess);
                stream.Close();

            }
            else
            {

                await botClient.SendTextMessageAsync(chatId, text: "Изменение некуда откатывать! Бот хранит 1 старую версию фотографии, если было произведено хотя бы одно изменение.", replyMarkup: keyboardAfterProcess);
            }
        }


        static async Task HandleCallbackQuery(ITelegramBotClient botClient, Telegram.Bot.Types.CallbackQuery callbackQuery) //Метод, перехватывающий данные из встроенной в сообщение клавиатуры
        {
            try
            {
                List<Model>.Enumerator p = Variables.Models.GetEnumerator(); //по сути элемент списка, будем сейчас перебирать их

                while (p.MoveNext()) //пока получается пройти по списку дальше, ищем модель с таким же именем, которое через callbackQuery.Data вернулось
                {
                    if (p.Current.name == callbackQuery.Data)
                        break;
                }

                try
                {
                    await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile(p.Current.PictureLink)); //Отправляем фото и файл по той ссылке, которая в объекте p.Сurrent лежит, то есть в том, который нашли по имени
                }
                catch
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: "По какой-то причине картинку загрузить не получилось. Извините.", replyMarkup: Variables.NationChoice);
                }
                try
                {
                    await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile(p.Current.FileLink));
                }
                catch
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: p.Current.FileLink, replyMarkup: Variables.NationChoice);
                }

             
            }
            catch
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: "Файл слишком большой или не существует. Сорян. Эту модель вам лучше скачать в интернете.", replyMarkup: Variables.NationChoice);
            }
            return;
        }

        static async Task EditModel(string editorName, ITelegramBotClient botClient, long chatId, ReplyKeyboardMarkup keyboardAfterProcess)
        {
            try
            {
                //Делаем резервную старую версию фотки

                System.IO.File.Copy(@"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName, @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName + "old.jpg", true);


                string destinationFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName;
                Process processOfEditing = Process.Start(@"C:\Users\чтепоноза\Desktop\Обработанные танки\" + editorName, $@"""{destinationFilePath}""");

                int secCount = 0;

                while (!processOfEditing.HasExited) //Ждём, пока этот процесс не завершится.
                {


                    await Task.Delay(1000);
                    secCount++;
                    processOfEditing.Refresh();
                    if (secCount > 10)
                    {

                        throw new Exception("TooMuchTime");
                    }
                }
                string editedFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[chatId].photoHashName;

                await using Stream stream = System.IO.File.OpenRead(editedFilePath); //открываем снова поток на наш файл, который сейчас отправим пользователю обратно
                await botClient.SendDocumentAsync(chatId, new InputOnlineFile(stream, Variables.usersDict[chatId].photoName.Replace(".jpg", " (edited).jpg")), replyMarkup: keyboardAfterProcess); //тут просто указываем, с каким именем бот вернёт файл
                stream.Close(); //закрываем поток, потому что иначе у нас оказывается будет ошибка при попытке удалить файл.



                //System.IO.File.Delete(editedFilePath); //Удаляем обработанный файл

                return;
            }
            catch (Exception e)
            {
                if (e.Message == "TooMuchTime")
                {
                    await botClient.SendTextMessageAsync(chatId, text: "Превышено время ожидания. Не удалось обработать фотографию.", replyMarkup: Variables.DefaultKeyboard);
                }
                else
                {
                    Process[] ps = Process.GetProcessesByName("Photoshop");
                    foreach (Process p in ps)
                        p.Kill();

                    Process[] editor = Process.GetProcessesByName("LightColorEdit"); //Вот это надо бы как-то переделать, чтобы он закрывал именно тот дроплет, который сейчас работает. !!!
                    foreach (Process p in editor)
                        p.Kill();
                    await botClient.SendTextMessageAsync(chatId, text: "Что-то пошло не так, и вашу фотографию не получилось обработать. Попробуйте снова.", replyMarkup: Variables.DefaultKeyboard);
                    Variables.photoshop = Process.Start(@"C:\Program Files\Adobe\Adobe Photoshop CS6 (64 Bit)\Photoshop.exe");
                }


                return;
            }
        }

        //Асинхронный метод чтобы чёта в потоках?
        async static Task Update(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken token)
        {
            //это контент, который мы получаем
            var message = update.Message;

            if (update.CallbackQuery != null)
                await HandleCallbackQuery(botClient, update.CallbackQuery);


            if (message == null)
                return;

            if (!Variables.usersDict.ContainsKey(message.Chat.Id)) //Есть такой юзер или нет
            {

                Variables.usersDict[message.Chat.Id] = new User();
                Console.WriteLine($"{message.Chat.FirstName} - зарегистрирован новый пользователь");
                Console.WriteLine($"{message.Chat.FirstName} with id {message.Chat.Id} started the bot");
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Нажмите \"Инфо\", чтобы узнать о боте или \"хочу модельку\" , чтобы получить модельку", replyMarkup: Variables.DefaultKeyboard);
                Variables.usersDict[message.Chat.Id].state = User.States.AfterStart;
                return;
            }


            if (message.Photo != null) //Если пользователь прислал фото в сжатом качестве, просим прислать в виде документа
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия, в виде документа");
                return;
            }

            if (message.Document != null) //Если пользователь реально прислал фото в виде документа
            {


                try
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Начинаем обработку новой фотографии.");
                    if (System.IO.File.Exists(Variables.usersDict[message.Chat.Id].destinationFilePath))
                        System.IO.File.Delete(Variables.usersDict[message.Chat.Id].destinationFilePath);
                    if (System.IO.File.Exists(Variables.usersDict[message.Chat.Id].destinationFilePath + "old.jpg"))
                        System.IO.File.Delete(Variables.usersDict[message.Chat.Id].destinationFilePath + "old.jpg");        //Удалим старую фотографию, и старую фотографию old тоже удалим.


                    var fileId = update.Message.Document.FileId;        //Id файла получаем
                    var fileInfo = await botClient.GetFileAsync(fileId); //Создаём переменную, в которой будет вся инфа по файлу.
                    var filePath = fileInfo.FilePath;                      //Создаём переменную, в которую кладём путь к файлу

                    Variables.usersDict[message.Chat.Id].photoName = message.Document.FileName;
                    Variables.usersDict[message.Chat.Id].photoHashName = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(5) + ".jpg"; //создали крутой короткий хэш по гайду с ютуба.
                    Variables.usersDict[message.Chat.Id].destinationFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[message.Chat.Id].photoHashName;
                    await using FileStream fileStream = System.IO.File.OpenWrite(Variables.usersDict[message.Chat.Id].destinationFilePath); //Открываем файловый поток туда, куда мы указали, то есть на наш новый файл
                    await botClient.DownloadFileAsync(          //и сохраняем  туда наш файл
                        filePath: filePath,
                        destination: fileStream
                        );
                    fileStream.Close();     //после чего можно и нужно закрыть поток

                    string fileHead = "";
                    using (FileStream stream = System.IO.File.OpenRead(Variables.usersDict[message.Chat.Id].destinationFilePath))
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            string bit = stream.ReadByte().ToString("X2");
                            fileHead += bit;                          //Читаем по очереди 4 байта файла и записываем их в fileHead
                        }
                    }
                    string type = "unknown format";

                    if (fileHead == "FFD8FFDB" || fileHead == "FFD8FFE0" || fileHead == "FFD8FFEE" || fileHead == "FFD8FFE0" || fileHead == "FFD8FFE1")
                    {
                        type = ".jpg";
                    }

                    if (type == ".jpg")
                    {

                        Variables.usersDict[message.Chat.Id].state = User.States.DarkOrLight; //Задаём нужное состояние и выводим нужную клавиатуру
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, выберите опцию обработки", replyMarkup: Variables.DarkOrLightKeyboard);
                    }
                    else
                    {
                        System.IO.File.Delete(Variables.usersDict[message.Chat.Id].destinationFilePath);
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия");
                    }
                    return;
                }
                catch (Exception e)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Что-то пошло не так, и вашу фотографию не получилось скачать. Попробуйте снова.", replyMarkup: Variables.DefaultKeyboard);
                    Variables.usersDict[message.Chat.Id].state = User.States.AfterStart;
                    Variables.usersDict[message.Chat.Id].photoName = "";
                    Variables.usersDict[message.Chat.Id].photoHashName = ""; //обнуляем переменные при ошибке
                    Variables.usersDict[message.Chat.Id].destinationFilePath = "";
                    return;
                }




            }


            if (Variables.usersDict[message.Chat.Id].state == User.States.DarkOrLight)
            {
                switch (message.Text)
                {
                    case "Обработка тёмного танка":
                        await EditModel("LightColorEdit.exe", botClient, message.Chat.Id, Variables.DarkOrLightKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.DarkOrLight;
                        break;
                    case "Обработка светлого танка":
                        await EditModel("ModelEdit.exe", botClient, message.Chat.Id, Variables.DarkOrLightKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.DarkOrLight;

                        break;
                    case "Лёгкое редактирование":
                        Variables.usersDict[message.Chat.Id].state = User.States.LightEditChoice;
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите опцию обработки:", replyMarkup: Variables.LightEditChoice);
                        break;
                    case "Цветовой баланс":
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите цвет, содержание которого надо повысить:", replyMarkup: Variables.ColorChoiceKeyboard);
                        break;

                    case "Откатить изменение":

                        await StepBackward(botClient, message.Chat.Id, Variables.DarkOrLightKeyboard);

                        break;

                    case "Отмена":
                        System.IO.File.Delete(Variables.usersDict[message.Chat.Id].destinationFilePath); //Удаляем фотки, которые есть на компе
                        if (System.IO.File.Exists(Variables.usersDict[message.Chat.Id].destinationFilePath + "old.jpg"))
                            System.IO.File.Delete(Variables.usersDict[message.Chat.Id].destinationFilePath + "old.jpg");
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Обработка фотографии отменена.", replyMarkup: Variables.DefaultKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.AfterStart;
                        break;

                    default:

                        return;
                }
                return;
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.LightEditChoice)
            {
                switch (message.Text)
                {
                    case "Осветлить фон":
                        await EditModel("Background.exe", botClient, message.Chat.Id, Variables.LightEditChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.LightEditChoice;
                        break;
                    case "Повысить контрастность":
                        await EditModel("Contrast.exe", botClient, message.Chat.Id, Variables.LightEditChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.LightEditChoice;

                        break;
                    case "Смягчить блики":
                        await EditModel("Lights.exe", botClient, message.Chat.Id, Variables.LightEditChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.LightEditChoice;
                        break;
                    case "Осветлить тени":
                        await EditModel("Shadows.exe", botClient, message.Chat.Id, Variables.LightEditChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.LightEditChoice;
                        break;
                    case "Повысить резкость":
                        await EditModel("Sharpness.exe", botClient, message.Chat.Id, Variables.LightEditChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.LightEditChoice;
                        break;

                    case "Откатить изменение":

                        await StepBackward(botClient, message.Chat.Id, Variables.LightEditChoice);

                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите опцию обработки", replyMarkup: Variables.DarkOrLightKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.DarkOrLight;
                        break;

                    default:

                        return;
                }
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.ColorChoice)
            {
                switch (message.Text)
                {
                    case "Голубой":
                        await EditModel("LightBlue.exe", botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        break;
                    case "Красный":
                        await EditModel("Red.exe", botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        break;

                    case "Пурпурный":
                        await EditModel("Purple.exe", botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        break;
                    case "Зелёный":
                        await EditModel("Green.exe", botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        break;
                    case "Жёлтый":
                        await EditModel("Yellow.exe", botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        break;
                    case "Синий":
                        await EditModel("Blue.exe", botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.ColorChoice;
                        break;

                    case "Откатить изменение":

                        await StepBackward(botClient, message.Chat.Id, Variables.ColorChoiceKeyboard);

                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите опцию обработки", replyMarkup: Variables.DarkOrLightKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.DarkOrLight;
                        break;

                    default:
                        return;
                }
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.AfterStart)
            {
                switch (message.Text)
                {
                    case "Инфо":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Я создан для простой тренировки моего создателя-студента. Если вы отправите мне фотографию, я смогу её обработать. Если вы хотите собрать модельку из бумаги, я её вам отправлю. Раньше я умел проводить весёлые викторины, но мой создатель их вырезал, поскольку они не подходили по смыслу бота. Удачного пользования!", replyMarkup: Variables.DefaultKeyboard);
                        break;
                    case "Хочу модельку":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите фильтр моделек", replyMarkup: Variables.FilterChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.FilterChoice;
                        break;

                    default:
                        return;
                }
                return;
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.FilterChoice)
            {
                switch (message.Text)
                {
                    case "По нации":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите нацию", replyMarkup: Variables.NationChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.NationChoice;
                        break;
                    case "По классу":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите класс", replyMarkup: Variables.ClassChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.ClassChoice;
                        break;
                    case "По сложности":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выберите сложность", replyMarkup: Variables.DifficultyChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.DifficultyChoice;
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.DefaultKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.AfterStart;
                        break;

                    default:
                        return;

                }
                return;
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.NationChoice)
            {
                Model.Nations feature;
                switch (message.Text) 
                {
                    case "СССР":
                        feature = Model.Nations.USSR;
                        break;
                    case "Германия":
                        feature = Model.Nations.Germany;
                        break;
                    case "США":
                        feature = Model.Nations.USA;
                        break;
                    case "Франция":
                        feature = Model.Nations.France;
                        break;
                    case "Великобритания":
                        feature = Model.Nations.Britain;
                        break;
                    case "Швеция":
                        feature = Model.Nations.Sweden;
                        break;
                    case "Чехословакия":
                        feature = Model.Nations.Czechoslovakia;
                        break;
                    case "Италия":
                        feature = Model.Nations.Italy;
                        break;
                    case "Япония":
                        feature = Model.Nations.Japan;
                        break;
                    case "Китай":
                        feature = Model.Nations.China;
                        break;
                    case "Польша":
                        feature = Model.Nations.Poland;
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.FilterChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.FilterChoice;
                        return;

                    default:
                        return;



                }
                await ModelsKeyboardRequest(botClient, message.Chat.Id, feature); //Запрашиваем клавиатуру по выбранному признаку
                return;
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.ClassChoice)
            {
                Model.Class feature;
                switch (message.Text)
                {
                    case "Лёгкие танки":
                        feature = Model.Class.light;
                        break;
                    case "Средние танки":
                        feature = Model.Class.medium;
                        break;
                    case "Тяжёлые танки":
                        feature = Model.Class.heavy;
                        break;
                    case "ПТ-САУ":
                        feature = Model.Class.PT;
                        break;
                    case "САУ":
                        feature = Model.Class.SAU;
                        break;
                    case "Диорамы":
                        feature = Model.Class.diorama;
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.FilterChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.FilterChoice;
                        return;

                    default:
                        return;
                }
                await ModelsKeyboardRequest(botClient, message.Chat.Id, feature); //Запрашиваем клавиатуру по выбранному признаку
                return;
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.DifficultyChoice)
            {
                Model.Difficulties feature;
                switch (message.Text)
                {
                    case "Низкая":
                        feature = Model.Difficulties.light;
                        break;
                    case "Средняя":
                        feature = Model.Difficulties.medium;
                        break;
                    case "Высокая":
                        feature = Model.Difficulties.difficult;
                        break;
                    case "Очень высокая":
                        feature = Model.Difficulties.difficultPlus;
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.FilterChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.FilterChoice;
                        return;

                    default:
                        return;
                }
                await ModelsKeyboardRequest(botClient, message.Chat.Id, feature); //Запрашиваем клавиатуру по выбранному признаку
            }
            return;
        }

        // Три версии этой функции: для класса, для нации и для сложности модели. 
        // Вообще хотелось бы как-нибудь одной обойтись и передавать туда переменную неопределённого типа просто, но я пока не знаю как это сделать. Ничего пока не выходит по этой части.
        public static async Task ModelsKeyboardRequest(ITelegramBotClient botClient, long chatId, Model.Nations feature)
        {


            //Здесь мы формируем клавиатуру по выбранному признаку, p - элемент списка моделей. Проходим через енумератор по списку, чтобы не вызывать по индексу. Ведь тогда придётся сто раз неявно пройти через весь список. А мы проходим только один раз благодаря енумератору.
            List<List<InlineKeyboardButton>> ButtonsList = new List<List<InlineKeyboardButton>>();// двумерный список кнопок
            List<Model>.Enumerator p = Variables.Models.GetEnumerator(); //по сути элемент списка
            bool endOfModelList = false;
            int j = 0;
            int i = 0;
            while (!endOfModelList)
            {
                ButtonsList.Add(new List<InlineKeyboardButton>());
                i = 0;
                while (i < 3)
                {

                    if (p.MoveNext()) //если получилось успешно перейти на следующий элемент
                    {
                        if (FeatureMatch(p.Current, feature))
                        {
                            ButtonsList[j].Add(InlineKeyboardButton.WithCallbackData(text: p.Current.name, callbackData: p.Current.name));
                            i++;
                        }
                    }
                    else
                    {
                        endOfModelList = true;
                        break;
                    }
                }
                j++;
            } //j просто успели на 1 как минимум повысить, так что небольшой костыльчик
            if (j == 1 && i==0) //если ни одной кнопки не создали
                await botClient.SendTextMessageAsync(chatId, text: "По вашему запросу ничего не нашлось. ");
            else
            {
                var Keyboard = new InlineKeyboardMarkup(ButtonsList);
                await botClient.SendTextMessageAsync(chatId, text: "Вот все танки по вашему запросу: ", replyMarkup: Keyboard);
            }
            return;


        }

        public static async Task ModelsKeyboardRequest(ITelegramBotClient botClient, long chatId, Model.Class feature)
        {


            //Здесь мы формируем клавиатуру по выбраннjve ghbpyfre, p - элемент списка моделей. Проходим через енумератор по списку, чтобы не вызывать по индексу. Ведь тогда придётся сто раз неявно пройти через весь список. А мы проходим только один раз благодаря енумератору.
            List<List<InlineKeyboardButton>> ButtonsList = new List<List<InlineKeyboardButton>>();// двумерный список кнопок
            List<Model>.Enumerator p = Variables.Models.GetEnumerator(); //по сути элемент списка
            bool endOfModelList = false;
            int j = 0;
            int i = 0;
            while (!endOfModelList)
            {
                ButtonsList.Add(new List<InlineKeyboardButton>());
                i = 0;
                while (i < 3)
                {

                    if (p.MoveNext()) //если получилось успешно перейти на следующий элемент
                    {
                        if (FeatureMatch(p.Current, feature))
                        {
                            ButtonsList[j].Add(InlineKeyboardButton.WithCallbackData(text: p.Current.name, callbackData: p.Current.name));
                            i++;
                        }
                    }
                    else
                    {
                        endOfModelList = true;
                        break;
                    }
                }
                j++;
            }
            if (j == 1 && i == 0) //если ни одной кнопки не создали
                await botClient.SendTextMessageAsync(chatId, text: "По вашему запросу ничего не нашлось. ");
            else
            {
                var Keyboard = new InlineKeyboardMarkup(ButtonsList);
                await botClient.SendTextMessageAsync(chatId, text: "Вот все танки по вашему запросу: ", replyMarkup: Keyboard);
            }
            return;


        }

        public static async Task ModelsKeyboardRequest(ITelegramBotClient botClient, long chatId, Model.Difficulties feature)
        {


            //Здесь мы формируем клавиатуру по выбраннjve ghbpyfre, p - элемент списка моделей. Проходим через енумератор по списку, чтобы не вызывать по индексу. Ведь тогда придётся сто раз неявно пройти через весь список. А мы проходим только один раз благодаря енумератору.
            List<List<InlineKeyboardButton>> ButtonsList = new List<List<InlineKeyboardButton>>();// двумерный список кнопок
            List<Model>.Enumerator p = Variables.Models.GetEnumerator(); //по сути элемент списка
            bool endOfModelList = false;
            int j = 0;
            int i = 0;
            while (!endOfModelList)
            {
                ButtonsList.Add(new List<InlineKeyboardButton>());
                i = 0;
                while (i < 3)
                {

                    if (p.MoveNext()) //если получилось успешно перейти на следующий элемент
                    {
                        if (FeatureMatch(p.Current, feature))
                        {
                            ButtonsList[j].Add(InlineKeyboardButton.WithCallbackData(text: p.Current.name, callbackData: p.Current.name));
                            i++;
                        }
                    }
                    else
                    {
                        endOfModelList = true;
                        break;
                    }
                }
                j++;
            }
            if (j == 1 && i==0) //если ни одной кнопки не создали
                await botClient.SendTextMessageAsync(chatId, text: "По вашему запросу ничего не нашлось. ");
            else
            {
                var Keyboard = new InlineKeyboardMarkup(ButtonsList);
                await botClient.SendTextMessageAsync(chatId, text: "Вот все танки по вашему запросу: ", replyMarkup: Keyboard);
            }
            return;


        }

        static public bool FeatureMatch(Model model, Model.Nations nation)
        {
            return model.nation == nation;
        }
        static public bool FeatureMatch(Model model, Model.Class myClass)
        {
            return model.classOfModel == myClass;
        }
        static public bool FeatureMatch(Model model, Model.Difficulties difficulty)
        {
            return model.difficulty == difficulty;
        }

            private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
