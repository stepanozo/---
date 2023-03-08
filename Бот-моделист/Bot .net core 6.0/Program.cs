using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
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
using Telegram.Bot.Types;

namespace Бот_1
{



    internal class Program
    {

        static void Main(string[] args)
        {


            var client = new TelegramBotClient("");
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
                switch (callbackQuery.Data)
                {
                    case "КВ-1Э":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/image/000_second_name_(684x243)_01.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf"));
                        break;
                    case "КВ-1":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/190/61890767.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/049_kv1_v10.pdf"));
                        break;
                    case "КВ-1 простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/188/63018849.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/055_simple_kv1_v10.pdf"));
                        break;
                    case "Т-10М":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/153/98960428.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/041_t-10m_v10.pdf"));
                        break;
                    case "Т-10М простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/153/98960428.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/047_simple_t-10m_v10.pdf"));
                        break;
                    case "С-51":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/136/15085131.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/042_simple_s-51_v10.pdf"));
                        break;
                    case "Т-100-ЛТ":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/128/93454318.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/document/039_simple_t-100_lt_v10.pdf"));
                        break;
                    case "СУ-85 (СУ-122)":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/107/80851302.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/document/02_su-85-122.pdf"));
                        break;
                    case "СУ-100":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/107/62346578.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/document/01_simple_su-100_v10.pdf"));
                        break;
                    case "Гоночный БТ-СВ":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/105/62582113.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/document/021_second_yellow_bt-sv_v10.pdf"));
                        break;
                    case "БТ-СВ простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/cd/cdc37a1bde581c507803a06d96a7bad0_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/006_simple_bt-sv_v10.pdf"));
                        break;
                    case "БТ-СВ":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/74/s25708512.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/030_bt-sv_v10.pdf"));
                        break;
                    case "Объект 704":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/101/60398123.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/document/029_simple_obj-704_v10.pdf"));
                        break;
                    case "СУ-122-54":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/83/26891213.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/025_su-122-54_v10.pdf"));
                        break;
                    case "ИСУ-122":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/55/42215194.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/012_second_isu-122_v10.pdf"));
                        break;
                    case "Т-44-100":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/55/24109417.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/020_simple_t-44-100(p)_v10.pdf"));
                        break;
                    case "СУ-18":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/43/48599346.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/017_simple_su-18_v10.pdf"));
                        break;
                    case "СУ-122-44":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/40/21261274.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/008_second_su-122-44_v10.pdf"));
                        break;
                    case "А-44":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/9c/9c6d672ae0c652be88873d518495a158_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/016_simple_a-44_v10.pdf"));
                        break;
                    case "ИС-4М":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/21/88561304.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf"));
                        break;
                    case "ИС-4М простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/18/69959246.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/007_simple_is-4_v10.pdf"));
                        break;
                    case "ИС-4М сложный":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/images/models-pub/wopt/front/005-2.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/005_second_is-4m_v10.pdf"));
                        break;
                    case "Т-44-85/122":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/image/011_second_t-44-85(122)(684x243)_01.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/011_second_T-44-85(122)_v10.pdf"));
                        break;
                    case "ИС-2":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/2f/2fb948c3c0b759e1c8c58639ba2bf311_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/032_is-2_v10.pdf"));
                        break;
                    case "ИС-2 простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/images/models-pub/wopt/box/008-1.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/007_simple_is-2_v10.pdf"));
                        break;
                    case "Т-34-85":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/201/53504644.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/028_kv-122_hangar_v10.pdf"));
                        break;
                    case "КВ-122":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/73/45551584.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf"));
                        break;
                    case "КВ-122 простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/58/19767664.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/004_simple_kv-122_v10.pdf"));
                        break;
                    case "«Панцер 50» трофейный":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/51/51368067.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/009_second_e-50m_v10.pdf"));
                        break;
                    case "Бронешары":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/40/81730495.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://content-wg.gcdn.co/wot/files/997_spheroids_v10.pdf"));
                        break;
                    case "Т-54":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/97/40879991.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf"));
                        break;
                    case "Т-54 простой":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/97/23888357.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/file/001_simple_t-54_v10.pdf"));
                        break;
                    case "Т-70 (Т-80)":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/40/56053320.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/file/0243_t_70_80_v10.pdf"));
                        break;
                    case "ИСУ-152":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/f7/f783e5b71a2191190043afa49c6f8cb8_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/document/023_isu-152_v10.pdf"));
                        break;
                    case "ИС-3-БН":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/images/models-pub/wopt/maket/998-1.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/file/998_is-3-bn_v10_2.pdf"));
                        break;
                    case "ИС-7":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/103/29558080.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf"));
                        break;
                    case "БТ-2":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/39/48536091.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/bt_2/016_bt-2_v01.pdf"));
                        break;
                    case "Т-35А":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/ba/ba8eb66139f3412191b9813d3f57e8bd_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/t35a/015_t-35_v1_0.pdf?MEDIA_PREFIX=/dcont/fb/"));
                        break;
                    case "Т-34":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/images/models-pub/wopt/maket/013-1-1.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/013_1_t-34_v1_0_new.pdf"));
                        break;
                    case "КВ-5":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/39/24567515.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/kv5/011_kv-5_v1_0.pdf"));
                        break;
                    case "ИС-3":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/63/63d7f97cf1061421cf0e393b5ac5e02c_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/is-3/010_is-3_v1_0.pdf"));
                        break;
                    case "КВ-2":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/74/61452991.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/008_kv-2/008_kv-2_v1_0.pdf"));
                        break;
                    case "СУ-26":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://paper-models.ru/cache_images/8f/8fc558bc913f0f72f2741b1e96d1ba84_optimize.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/004_su-26/004_su-26_v1_1.pdf"));
                        break;
                    case "МС-1":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/11/35235470.jpg"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://tanki.su/dcont/fb/media/models/001_ms-1/001_ms-1_v1_1.pdf"));
                        break;
                    case "Пушка-гаубица МЛ-20":
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://only-paper.ru/_ld/157/45020462.png"));
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, new InputOnlineFile("https://ru-wotp.lesta.ru/dcont/fb/file/992_ml-20(152-mm)_v10.pdf"));
                        break;

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

            if(update.CallbackQuery != null)
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

                        break;
                }
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
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.FilterChoice);
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.DefaultKeyboard);
                        Variables.usersDict[message.Chat.Id].state = User.States.AfterStart;
                        break;

                    default:

                        break;
                }
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.NationChoice)
            {
                switch (message.Text)
                {
                    case "СССР":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Вот все советские танки: ", replyMarkup: Variables.USSR);
                        break;
                    case "Германия":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "США":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Франция":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Великобритания":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Швеция":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Чехословакия":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Италия":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Япония":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Китай":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;
                    case "Польша":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.NationChoice);
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.FilterChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.FilterChoice;
                        break;

                    default:

                        break;
                }
            }

            if (Variables.usersDict[message.Chat.Id].state == User.States.ClassChoice)
            {
                switch (message.Text)
                {
                    case "Лёгкие танки":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.ClassChoice);
                        break;
                    case "Средние танки":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.ClassChoice);
                        break;
                    case "Тяжёлые танки":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.ClassChoice);
                        break;
                    case "ПТ-САУ":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.ClassChoice);
                        break;
                    case "САУ":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.ClassChoice);
                        break;
                    case "Диорамы":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Это демонстрационная версия, тут ничего не происходит и эта кнопка вообще не работает! Попробуйте танки СССР, они точно доступны.", replyMarkup: Variables.ClassChoice);
                        break;

                    case "Отмена":
                        await botClient.SendTextMessageAsync(message.Chat.Id, text: "Выбор отменён", replyMarkup: Variables.FilterChoice);
                        Variables.usersDict[message.Chat.Id].state = User.States.FilterChoice;
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
