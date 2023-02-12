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

        //Асинхронный метод чтобы чёта в потоках?
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            //это контент, который мы получаем
            var message = update.Message;

            if (!Variables.usersDict.ContainsKey(message.Chat.Id)) //Есть такой юзер или нет
            {
                Variables.usersDict[message.Chat.Id] = new User();
                Console.WriteLine($"{message.Chat.FirstName} - зарегистрирован новый пользователь");
                Console.WriteLine($"{message.Chat.FirstName} with id {message.Chat.Id} started the bot");
                await botClient.SendTextMessageAsync(message.Chat.Id, text: "Я пробудился. Мои команды:\n\nРеплики\nФото\nПомощь\nИгра\nСтоп", replyMarkup: Variables.DefaultKeyboard);
                return;
            }
            

            if (message.Text != null)
            {

                //  Console.WriteLine($"{message.Chat.FirstName} | {message.Text}");

                if (message.Text == "/start")
                {
                    Console.WriteLine($"{message.Chat.FirstName} with id {message.Chat.Id} started the bot");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Мои команды:\n\nРеплики\nФото\nПомощь\nИгра\nСтоп");

                    return;
                }
                else
                {

                    if (message.Text.ToLower().Contains("здорова"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Здоровей видали");
                        return;
                    }




                }



            }

            if (message.Document != null && !Variables.usersDict[message.Chat.Id].photoSent)
            {
                try
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Сейчас обработаем фото...", replyMarkup: Variables.DefaultKeyboard);

                    var fileId = update.Message.Document.FileId;        //Id файла получаем
                    var fileInfo = await botClient.GetFileAsync(fileId); //Создаём переменную, в которой будет вся инфа по файлу.
                    var filePath = fileInfo.FilePath;                      //Создаём переменную, в которую кладём путь к файлу

                    Variables.usersDict[message.Chat.Id].photoName = message.Document.FileName;
                    Variables.usersDict[message.Chat.Id].photoHashName = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(5) + ".jpg"; //создали крутой короткий хэш по гайду с ютуба.
                    Variables.usersDict[message.Chat.Id].destinationFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\" + Variables.usersDict[message.Chat.Id].photoHashName;
                    await using FileStream fileStream = System.IO.File.OpenWrite(Variables.usersDict[message.Chat.Id].destinationFilePath); //Открываем файловый поток туда, куда мы указали, то есть на наш новый файл
                    await botClient.DownloadFileAsync(          //и сохраняем  туда наш файл
                        filePath: filePath,
                        destination: fileStream
                        );
                    fileStream.Close();     //после чего можно и нужно закрыть поток

                    List<string> fileHead = new List<string>();
                    using (FileStream stream = System.IO.File.OpenRead(Variables.usersDict[message.Chat.Id].destinationFilePath))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            string bit = stream.ReadByte().ToString("X2");
                            fileHead.Add(bit);                          //Читаем по очереди 4 байта файла и записываем их в fileHead
                        }
                    }
                    string type = "unknown format";

                    if (!Variables.png.Except(fileHead).Any()) //если исключим все байты от fileHead из png и ничего не останется, то это png
                    {
                        type = ".png";
                    }
                    else if (!Variables.jpg.Except(fileHead).Any())
                    {
                        type = ".jpg";
                    }
                    else if (!Variables.jpeg.Except(fileHead).Any())
                    {
                        type = ".jpeg";
                    }

                    if (type == ".jpg" || type == ".jpeg")
                    {

                        Variables.usersDict[message.Chat.Id].photoSent = true;
                        string editedFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[message.Chat.Id].photoHashName;
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, выберите опцию обработки", replyMarkup: Variables.EditOptionsKeyboard);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия", replyMarkup: Variables.DefaultKeyboard);
                    }
                    return;
                }
                catch (Exception e)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Что-то пошло не так, и вашу фотографию не получилось скачать. Попробуйте снова.", replyMarkup: Variables.DefaultKeyboard);

                    return;
                }

            }



            if (Variables.usersDict[message.Chat.Id].photoSent)
            {

                if (message.Text == "Обработка светлого танка" || message.Text == "Обработка тёмного танка")
                {

                    try
                    {

                        string editorName = "LightColorEdit.exe";
                        if (message.Text == "Обработка тёмного танка")
                            editorName = "LightColorEdit";
                        else if (message.Text == "Обработка светлого танка")
                            editorName = "ModelEdit.exe";



                        Process processOfEditing = Process.Start(@"C:\Users\чтепоноза\Desktop\Обработанные танки\" + editorName, $@"""{Variables.usersDict[message.Chat.Id].destinationFilePath}"""); //@ нужно для использования недопустимых вещей в строке

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

                        string editedFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + Variables.usersDict[message.Chat.Id].photoHashName;

                        await using Stream stream = System.IO.File.OpenRead(editedFilePath); //открываем снова поток на наш файл
                        await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, Variables.usersDict[message.Chat.Id].photoName.Replace(".jpg", " (edited).jpg")), replyMarkup: Variables.EditOptionsKeyboard); //тут просто указываем, с каким именем бот вернёт файл
                        stream.Close(); //закрываем поток, потому что иначе у нас оказывается будет ошибка при попытке удалить файл.



                        System.IO.File.Delete(editedFilePath); //Удаляем обработанный файл

                        
                        return;
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "TooMuchTime")
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Превышено время ожидания. Не удалось обработать фотографию.", replyMarkup: Variables.DefaultKeyboard);
                        }
                        else
                        {
                            Process[] ps = Process.GetProcessesByName("Photoshop");
                            foreach (Process p in ps)
                                p.Kill();

                            Process[] editor = Process.GetProcessesByName("LightColorEdit"); //Вот это надо бы как-то переделать, чтобы он закрывал именно тот дроплет, который сейчас работает. !!!
                            foreach (Process p in editor)
                                p.Kill();
                            await botClient.SendTextMessageAsync(message.Chat.Id, text: "Что-то пошло не так, и вашу фотографию не получилось обработать. Попробуйте снова.", replyMarkup: Variables.DefaultKeyboard);
                            Variables.photoshop = Process.Start(@"C:\Program Files\Adobe\Adobe Photoshop CS6 (64 Bit)\Photoshop.exe");
                        }


                        return;
                    }




                }

                if (message.Photo != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия, в виде документа", replyMarkup: Variables.DefaultKeyboard);
                    return;
                }

                else if (message.Text == "Отмена")
                {
                    Variables.usersDict[message.Chat.Id].photoSent = false;
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Обработка фотографии отменена", replyMarkup: Variables.DefaultKeyboard);
                    System.IO.File.Delete(Variables.usersDict[message.Chat.Id].destinationFilePath); //Удаляем скачанный файл
                    return;
                }




                return;
            }

        }


        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
