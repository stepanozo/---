using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
//using Telegram.Bot.Exceptions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;
using System.Diagnostics;
using System.Windows.Input;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Telegram.Bots.Requests;
using Telegram.Bots.Http;
using Telegram.Bots.Types;
using System.IO;

namespace Бот_1
{



    internal class Program
    {

        static void Main(string[] args)
        {


            var client = new TelegramBotClient("6019171965:AAELlnnmqAqz0vu2RnxneFsFX3Syxu-n0zU");
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
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия, в виде документа", replyMarkup: Variables.DefaultKeyboard);
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
                                for (int i = 0; i < 4; i++)
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
                                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия", replyMarkup: Variables.DefaultKeyboard);
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
                    case "Обработка светлого танка":
                        await EditModel("LightColorEdit.exe", botClient, message.Chat.Id, Variables.DarkOrLightKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.DarkOrLight;
                        break;
                    case "Обработка тёмного танка":
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

                        break;
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

                        break;
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

                        break;
                }
            }








        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
