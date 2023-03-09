using Bot_.net_core_6._0;
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

        public static ReplyKeyboardMarkup DifficultyChoice = new(
                  new[]
                  {
                           new KeyboardButton[] { "Низкая", "Средняя"},
                           new KeyboardButton[] { "Высокая", "Очень высокая" },
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

        public static List<Model> Models = new List<Model>() {
            new Model ("Waffenträger auf E 100", Model.Nations.Germany, Model.Difficulties.difficult, Model.Class.PT, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/048_waffentrager_auf_e100_v10_(1).pdf", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/image/048_waffentrager_e100_(684x243)_01.png"),
            new Model ("КВ-1Э",             Model.Nations.USSR, Model.Difficulties.medium, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/image/000_second_name_(684x243)_01.jpg"),
            new Model ("КВ-1",              Model.Nations.USSR, Model.Difficulties.medium, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/049_kv1_v10.pdf", PictureLink: "https://only-paper.ru/_ld/190/61890767.png"),
            new Model ("КВ-1 простой",      Model.Nations.USSR, Model.Difficulties.light, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/055_simple_kv1_v10.pdf", PictureLink: "https://only-paper.ru/_ld/188/63018849.png"),
            new Model ("Т-10М",             Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/041_t-10m_v10.pdf", PictureLink: "https://only-paper.ru/_ld/153/98960428.png"),
            new Model ("Т-10М простой",     Model.Nations.USSR, Model.Difficulties.light, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/047_simple_t-10m_v10.pdf", PictureLink: "https://only-paper.ru/_ld/153/98960428.png"),
            new Model ("С-51",              Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.SAU, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/042_simple_s-51_v10.pdf", PictureLink: "https://only-paper.ru/_ld/136/15085131.jpg"),
            new Model ("Т-100-ЛТ",          Model.Nations.USSR, Model.Difficulties.light, Model.Class.light, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/document/039_simple_t-100_lt_v10.pdf", PictureLink: "https://only-paper.ru/_ld/128/93454318.jpg"),
            new Model ("СУ-85 (СУ-122)",    Model.Nations.USSR, Model.Difficulties.medium, Model.Class.PT, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/document/02_su-85-122.pdf", PictureLink: "https://only-paper.ru/_ld/107/80851302.jpg"),
            new Model ("СУ-100",            Model.Nations.USSR, Model.Difficulties.medium, Model.Class.PT, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/document/01_simple_su-100_v10.pdf", PictureLink: "https://only-paper.ru/_ld/107/62346578.jpg"),
            new Model ("Гоночный БТ-СВ",    Model.Nations.USSR, Model.Difficulties.medium, Model.Class.light, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/document/021_second_yellow_bt-sv_v10.pdf", PictureLink: "https://only-paper.ru/_ld/105/62582113.jpg"),
            new Model ("БТ-СВ простой", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.light, FileLink: "https://content-wg.gcdn.co/wot/files/006_simple_bt-sv_v10.pdf", PictureLink: "https://paper-models.ru/cache_images/cd/cdc37a1bde581c507803a06d96a7bad0_optimize.jpg"),
            new Model ("БТ-СВ", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.light, FileLink: "https://content-wg.gcdn.co/wot/files/030_bt-sv_v10.pdf", PictureLink: "https://only-paper.ru/_ld/74/s25708512.jpg"),
            new Model ("Объект 704", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.PT, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/document/029_simple_obj-704_v10.pdf", PictureLink: "https://only-paper.ru/_ld/101/60398123.jpg"),
            new Model ("СУ-122-54", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.PT, FileLink: "https://content-wg.gcdn.co/wot/files/025_su-122-54_v10.pdf", PictureLink: "https://only-paper.ru/_ld/83/26891213.jpg"),
            new Model ("ИСУ-122", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.PT, FileLink: "https://content-wg.gcdn.co/wot/files/012_second_isu-122_v10.pdf", PictureLink: "https://only-paper.ru/_ld/55/42215194.png"),
            new Model ("Т-44-100", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.medium, FileLink: "https://content-wg.gcdn.co/wot/files/020_simple_t-44-100(p)_v10.pdf", PictureLink: "https://only-paper.ru/_ld/55/24109417.png"),
            new Model ("СУ-18", Model.Nations.USSR, Model.Difficulties.light, Model.Class.SAU, FileLink: "https://content-wg.gcdn.co/wot/files/017_simple_su-18_v10.pdf", PictureLink: "https://only-paper.ru/_ld/43/48599346.jpg"),
            new Model ("СУ-122-44", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.PT, FileLink: "https://content-wg.gcdn.co/wot/files/008_second_su-122-44_v10.pdf", PictureLink: "https://only-paper.ru/_ld/40/21261274.jpg"),
            new Model ("А-44", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.medium, FileLink: "https://content-wg.gcdn.co/wot/files/016_simple_a-44_v10.pdf", PictureLink: "https://paper-models.ru/cache_images/9c/9c6d672ae0c652be88873d518495a158_optimize.jpg"),
            new Model ("ИС-4М простой", Model.Nations.USSR, Model.Difficulties.light, Model.Class.heavy, FileLink: "https://content-wg.gcdn.co/wot/files/007_simple_is-4_v10.pdf", PictureLink: "https://only-paper.ru/_ld/18/69959246.jpg"),
            new Model ("ИС-4М сложный", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://content-wg.gcdn.co/wot/files/005_second_is-4m_v10.pdf", PictureLink: "https://paper-models.ru/images/models-pub/wopt/front/005-2.jpg"),
            new Model ("Т-44-85/122", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.medium, FileLink: "https://content-wg.gcdn.co/wot/files/011_second_T-44-85(122)_v10.pdf", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/image/011_second_t-44-85(122)(684x243)_01.jpg"),
            new Model ("ИС-2", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://content-wg.gcdn.co/wot/files/032_is-2_v10.pdf", PictureLink: "https://paper-models.ru/cache_images/2f/2fb948c3c0b759e1c8c58639ba2bf311_optimize.jpg"),
            new Model ("ИС-2 простой", Model.Nations.USSR, Model.Difficulties.light, Model.Class.heavy, FileLink: "https://content-wg.gcdn.co/wot/files/007_simple_is-2_v10.pdf", PictureLink: "https://paper-models.ru/images/models-pub/wopt/box/008-1.jpg"),
            new Model ("Т-34-85", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.medium, FileLink: "https://content-wg.gcdn.co/wot/files/028_kv-122_hangar_v10.pdf", PictureLink: "https://only-paper.ru/_ld/201/53504644.png"),
            new Model ("КВ-122", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf", PictureLink: "https://only-paper.ru/_ld/73/45551584.png"),
            new Model ("КВ-122 простой", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.heavy, FileLink: "https://content-wg.gcdn.co/wot/files/004_simple_kv-122_v10.pdf", PictureLink: "https://only-paper.ru/_ld/58/19767664.jpg"),
            new Model ("«Панцер 50» трофейный", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.medium, FileLink: "https://content-wg.gcdn.co/wot/files/009_second_e-50m_v10.pdf", PictureLink: "https://only-paper.ru/_ld/51/51368067.jpg"),
            new Model ("Бронешары", Model.Nations.USSR, Model.Difficulties.light, Model.Class.light, FileLink: "https://content-wg.gcdn.co/wot/files/997_spheroids_v10.pdf", PictureLink: "https://only-paper.ru/_ld/40/81730495.jpg"),
            new Model ("Т-54", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.medium, FileLink: "https://tanki.su/dcont/fb/file/025_t-54_v10.pdf", PictureLink: "https://only-paper.ru/_ld/97/40879991.jpg"),
            new Model ("Т-70 (Т-80)", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.light, FileLink: "https://tanki.su/dcont/fb/file/0243_t_70_80_v10.pdf", PictureLink: "https://only-paper.ru/_ld/40/56053320.jpg"),
            new Model ("ИСУ-152", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.PT, FileLink: "https://tanki.su/dcont/fb/document/023_isu-152_v10.pdf", PictureLink: "https://paper-models.ru/cache_images/f7/f783e5b71a2191190043afa49c6f8cb8_optimize.jpg"),
            new Model ("ИС-3-БН", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://tanki.su/dcont/fb/file/998_is-3-bn_v10_2.pdf", PictureLink: "https://paper-models.ru/images/models-pub/wopt/maket/998-1.jpg"),
            new Model ("ИС-7", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/029_second_kv-1e.pdf", PictureLink: "https://only-paper.ru/_ld/103/29558080.jpg"),
            new Model ("БТ-2", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.light, FileLink: "https://tanki.su/dcont/fb/media/models/bt_2/016_bt-2_v01.pdf", PictureLink: "https://only-paper.ru/_ld/39/48536091.png"),
            new Model ("Т-35А", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://tanki.su/dcont/fb/media/models/t35a/015_t-35_v1_0.pdf?MEDIA_PREFIX=/dcont/fb/", PictureLink: "https://paper-models.ru/cache_images/ba/ba8eb66139f3412191b9813d3f57e8bd_optimize.jpg"),
            new Model ("Т-34", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.medium, FileLink: "https://tanki.su/dcont/fb/media/models/013_1_t-34_v1_0_new.pdf", PictureLink: "https://paper-models.ru/images/models-pub/wopt/maket/013-1-1.jpg"),
            new Model ("КВ-5", Model.Nations.USSR, Model.Difficulties.medium, Model.Class.heavy, FileLink: "https://tanki.su/dcont/fb/media/models/kv5/011_kv-5_v1_0.pdf", PictureLink: "https://only-paper.ru/_ld/39/24567515.jpg"),
            new Model ("ИС-3", Model.Nations.USSR, Model.Difficulties.difficult, Model.Class.heavy, FileLink: "https://tanki.su/dcont/fb/media/is-3/010_is-3_v1_0.pdf", PictureLink: "https://paper-models.ru/cache_images/63/63d7f97cf1061421cf0e393b5ac5e02c_optimize.jpg"),
            new Model ("КВ-2", Model.Nations.USSR, Model.Difficulties.light, Model.Class.heavy, FileLink: "https://tanki.su/dcont/fb/media/models/008_kv-2/008_kv-2_v1_0.pdf", PictureLink: "https://only-paper.ru/_ld/74/61452991.jpg"),
            new Model ("СУ-26", Model.Nations.USSR, Model.Difficulties.light, Model.Class.SAU, FileLink: "https://tanki.su/dcont/fb/media/models/004_su-26/004_su-26_v1_1.pdf", PictureLink: "https://paper-models.ru/cache_images/8f/8fc558bc913f0f72f2741b1e96d1ba84_optimize.jpg"),
            new Model ("МС-1", Model.Nations.USSR, Model.Difficulties.light, Model.Class.light, FileLink: "https://tanki.su/dcont/fb/media/models/001_ms-1/001_ms-1_v1_1.pdf", PictureLink: "https://only-paper.ru/_ld/11/35235470.jpg"),
            new Model ("Пушка-гаубица МЛ-20", Model.Nations.USSR, Model.Difficulties.light, Model.Class.PT, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/992_ml-20(152-mm)_v10.pdf", PictureLink: "https://only-paper.ru/_ld/157/45020462.png"),
            new Model ("Т-54 простой", Model.Nations.USSR, Model.Difficulties.light, Model.Class.medium, FileLink: "https://tanki.su/dcont/fb/file/001_simple_t-54_v10.pdf", PictureLink: "https://only-paper.ru/_ld/97/23888357.jpg"),
            new Model ("Т29", Model.Nations.USA, Model.Difficulties.light, Model.Class.heavy, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/056_simple_t29_v10.pdf", PictureLink: "https://only-paper.ru/_ld/187/80076400.jpg"),
            new Model ("Waffenträger auf E 100 простая", Model.Nations.Germany, Model.Difficulties.medium, Model.Class.PT, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/054_waffentrager_auf_e100_v10.pdf", PictureLink: "https://only-paper.ru/_ld/192/08412371.png"),
            new Model ("M3 Lee", Model.Nations.USA, Model.Difficulties.medium, Model.Class.medium, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/047_m3_lee_v10.pdf", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/image/047_m3_lee_(684x243)_01.jpg"),
            new Model ("M3 Lee простой", Model.Nations.USA, Model.Difficulties.light, Model.Class.medium, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/053_simple_m3_lee_v10.pdf", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/image/053_simple_m3_lee_(684x243)_01.jpg"),
            new Model ("G.W. Panther", Model.Nations.Germany, Model.Difficulties.difficult, Model.Class.SAU, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/image/046_g_w_panther_(684x243)_01.jpg", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/file/046_g_w_panther_v10.pdf"),
            new Model ("G.W. Panther простая", Model.Nations.Germany, Model.Difficulties.medium, Model.Class.SAU, FileLink: "https://ru-wotp.lesta.ru/dcont/fb/file/052_simple_g_w_panther_v10.pdf", PictureLink: "https://ru-wotp.lesta.ru/dcont/fb/image/052_simple_g_w_panther_(684x243)_01.jpg"),
         /*   new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
             new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
             new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
             new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),
            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),


            new Model ("", Model.Nations., Model.Difficulties., Model.Class.PT, FileLink: "", PictureLink: ""),*/

        };

    }

    
   
       
        

    



}