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

namespace Бот_1
{



    internal class Program
    {

        static void Main(string[] args)
        {


            var client = new TelegramBotClient("6019171965:AAELlnnmqAqz0vu2RnxneFsFX3Syxu-n0zU");

            client.StartReceiving(Update, Error); //этих двух методов нет
            Console.ReadLine();
        }

        //Асинхронный метод чтобы чёта в потоках?
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            //это контент, который мы получаем
            var message = update.Message;



            if(message.Text == null && message.Document != null)
            {

                try
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Сейчас обработаем фото...", replyMarkup: Variables.DefaultKeyboard);

                    var fileId = update.Message.Document.FileId;        //Id файла получаем
                    var fileInfo = await botClient.GetFileAsync(fileId); //Создаём переменную, в которой будет вся инфа по файлу.
                    var filePath = fileInfo.FilePath;                      //Создаём переменную, в которую кладём путь к файлу

                    string photoHashName = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(5) + ".jpg" ; //создали крутой короткий хэш по гайду с ютуба.
                    string destinationFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\" + photoHashName;
                    await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath); //Открываем файловый поток туда, куда мы указали, то есть на наш новый файл
                    await botClient.DownloadFileAsync(          //и сохраняем  туда наш файл
                        filePath: filePath,
                        destination: fileStream
                        );
                    fileStream.Close();     //после чего можно и нужно закрыть поток
                                            //пока не совсем понимаю почему путь именно так записан странно
                    if (message.Document.FileName.Substring(message.Document.FileName.Length - 4) == ".jpg")
                    {
                        Process processOfEditing = Process.Start(@"C:\Users\чтепоноза\Desktop\Обработанные танки\LightColorEdit.exe", $@"""{destinationFilePath}"""); //@ нужно для использования недопустимых вещей в строке
                       // Process[] photoshop = Process.GetProcessesByName("Photoshop");
                        int secCount = 0;

                        while (!processOfEditing.HasExited) //Ждём, пока этот процесс не завершится.
                        {
                         
                            
                            await Task.Delay(1000);
                            secCount++;
                            processOfEditing.Refresh();
                            if (secCount > 10)
                            {
                                
                                throw new Exception();
                            }
                        }

                        string editedFilePath = @"C:\Users\чтепоноза\Desktop\Обработанные танки\Edited\" + photoHashName;

                        await using Stream stream = System.IO.File.OpenRead(editedFilePath); //открываем снова поток на наш файл
                        await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, message.Document.FileName.Replace(".jpg", " (edited).jpg"))); //тут просто указываем, с каким именем бот вернёт файл
                        stream.Close(); //закрываем поток, потому что иначе у нас оказывается будет ошибка при попытке удалить файл.


                        System.IO.File.Delete(destinationFilePath); //была неоднозначность, так что указали, что это именно файл системы, а не телеграмма.
                        System.IO.File.Delete(editedFilePath); //Удаляем и старый, и обработанный файлы.

                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Пожалуйста, отправьте файл в формате jpg без сжатия", replyMarkup: Variables.DefaultKeyboard);
                    }
                    return;
                }
                catch
                {
                    Process[] photoshop = Process.GetProcessesByName("Photoshop");
                    foreach (Process p in photoshop)
                        p.Kill();

                    Process[] editor = Process.GetProcessesByName("LightColorEdit");
                    foreach (Process p in editor)
                        p.Kill();

                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Что-то пошло не так, и вашу фотографию не получилось обработать. Попробуйте снова.", replyMarkup: Variables.DefaultKeyboard);
                    return;
                }
                



            }


            if (message.Text != null)
            {

                //  Console.WriteLine($"{message.Chat.FirstName} | {message.Text}");

                if (!Variables.usersDict.ContainsKey(message.Chat.Id)) //Есть такой юзер или нет
                {
                    Variables.usersDict[message.Chat.Id] = new User();
                    Console.WriteLine($"{message.Chat.FirstName} - зарегистрирован новый пользователь");
                    Console.WriteLine($"{message.Chat.FirstName} with id {message.Chat.Id} started the bot");
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Я пробудился. Мои команды:\n\nРеплики\nФото\nПомощь\nИгра\nСтоп", replyMarkup: Variables.DefaultKeyboard);
                    return;
                }
                else if (message.Text == "/start")
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
            return;

        }


        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
