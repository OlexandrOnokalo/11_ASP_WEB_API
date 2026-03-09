using Books.DAL.Entities;

namespace Books.DAL.Seeding
{
    //public static class SeedData
    //{
    //    public static void SeedDatabase(AppDbContext context)
    //    {
    //        if (context.Authors.Any())
    //            return;

    //        var genres = GenerateGenres();
    //        context.Genres.AddRange(genres);
    //        context.SaveChanges(); 

    //        var authors = GenerateAuthors();
    //        context.Authors.AddRange(authors);
    //        context.SaveChanges();

    //        var books = GenerateBooks(authors, genres);
    //        context.Books.AddRange(books);
    //        context.SaveChanges();

    //        var booksGenres = GenerateBooksGenres(books, genres);
    //        context.BooksGenres.AddRange(booksGenres);
    //        context.SaveChanges();
    //    }

    //    private static List<GenreEntity> GenerateGenres()
    //    {
    //        return new List<GenreEntity>
    //        {
    //            new() { Name = "Антиутопія" },
    //            new() { Name = "Психологічний роман" },
    //            new() { Name = "Художня література" },
    //            new() { Name = "Драма" },
    //            new() { Name = "Філософія" },
    //            new() { Name = "Готика" },
    //            new() { Name = "Комедія" },
    //            new() { Name = "Поезія" },
    //            new() { Name = "Трагедія" },
    //            new() { Name = "Відвертість" }
    //        };
    //    }

    //    private static List<AuthorEntity> GenerateAuthors()
    //    {
    //        return new List<AuthorEntity>
    //        {
    //            new() { Name = "Федір Достоєвський", BirthDate = DateTime.SpecifyKind(new DateTime(1821, 11, 11), DateTimeKind.Utc) },
    //            new() { Name = "Лев Толстой", BirthDate = DateTime.SpecifyKind(new DateTime(1828, 9, 9), DateTimeKind.Utc) },
    //            new() { Name = "Іван Тургенєв", BirthDate = DateTime.SpecifyKind(new DateTime(1818, 11, 9), DateTimeKind.Utc) },
    //            new() { Name = "Микола Гоголь", BirthDate = DateTime.SpecifyKind(new DateTime(1809, 3, 31), DateTimeKind.Utc) },
    //            new() { Name = "Олександр Пушкін", BirthDate = DateTime.SpecifyKind(new DateTime(1799, 6, 6), DateTimeKind.Utc) },
    //            new() { Name = "Марія Шеллі", BirthDate = DateTime.SpecifyKind(new DateTime(1797, 8, 30), DateTimeKind.Utc) },
    //            new() { Name = "Джейн Остен", BirthDate = DateTime.SpecifyKind(new DateTime(1775, 12, 16), DateTimeKind.Utc) },
    //            new() { Name = "Чарльз Діккенс", BirthDate = DateTime.SpecifyKind(new DateTime(1812, 2, 7), DateTimeKind.Utc) },
    //            new() { Name = "Джордж Оруелл", BirthDate = DateTime.SpecifyKind(new DateTime(1903, 6, 25), DateTimeKind.Utc) },
    //            new() { Name = "Олдос Гакслі", BirthDate = DateTime.SpecifyKind(new DateTime(1894, 7, 26), DateTimeKind.Utc) },
    //            new() { Name = "Франц Кафка", BirthDate = DateTime.SpecifyKind(new DateTime(1883, 7, 3), DateTimeKind.Utc) },
    //            new() { Name = "Томас Манн", BirthDate = DateTime.SpecifyKind(new DateTime(1875, 6, 6), DateTimeKind.Utc) },
    //            new() { Name = "Марсель Пруст", BirthDate = DateTime.SpecifyKind(new DateTime(1871, 7, 10), DateTimeKind.Utc) },
    //            new() { Name = "Джеймс Джойс", BirthDate = DateTime.SpecifyKind(new DateTime(1882, 2, 2), DateTimeKind.Utc) },
    //            new() { Name = "Вільям Фолкнер", BirthDate = DateTime.SpecifyKind(new DateTime(1897, 9, 25), DateTimeKind.Utc) },
    //            new() { Name = "Хемінгвей Ернест", BirthDate = DateTime.SpecifyKind(new DateTime(1899, 7, 21), DateTimeKind.Utc) },
    //            new() { Name = "Набоков Володимир", BirthDate = DateTime.SpecifyKind(new DateTime(1899, 4, 22), DateTimeKind.Utc) },
    //            new() { Name = "Беккет Семюел", BirthDate = DateTime.SpecifyKind(new DateTime(1906, 4, 13), DateTimeKind.Utc) },
    //            new() { Name = "Борхес Хорхе Луїс", BirthDate = DateTime.SpecifyKind(new DateTime(1899, 8, 24), DateTimeKind.Utc) },
    //            new() { Name = "Сартр Жан-Поль", BirthDate = DateTime.SpecifyKind(new DateTime(1905, 6, 21), DateTimeKind.Utc) }
    //        };
    //    }

    //    private static List<BookEntity> GenerateBooks(List<AuthorEntity> authors, List<GenreEntity> genres)
    //    {
    //        var books = new List<BookEntity>();
    //        var random = new Random(42);

    //        var bookTitles = new Dictionary<int, List<string>>
    //        {
    //            { 0, new List<string> { "Злочин і покарання", "Демони", "Ідіот", "Бідні люди", "Подвійник", "Братя Карамазови", "Записки з підземелля", "Белые ночи", "Игрок", "Унизо и оскорбленные" } },
    //            { 1, new List<string> { "Война и мир", "Анна Каренина", "Война и мир (том 2)", "Воскресение", "Казаки", "Севастопольские рассказы", "Власть тьмы", "Плоды просвещения", "Крейцерова соната", "Что такое искусство?" } },
    //            { 2, new List<string> { "Отцы и дети", "Рудин", "Дворянское гнездо", "Накануне", "Месяц в деревне", "Первая любовь", "Призраки", "Странная история", "Ася", "Вешние воды" } },
    //            { 3, new List<string> { "Мертвые души", "Ревизор", "Шинель", "Нос", "Вий", "Женитьба", "Тарас Бульба", "Записки сумасшедшего", "Портрет", "Коляска" } },
    //            { 4, new List<string> { "Евгений Онегин", "Мертвые души", "Капитанская дочка", "Пиковая дама", "Дубровский", "Полтава", "Медный всадник", "Руслан и Людмила", "Борис Годунов", "Кавказский пленник" } },
    //            { 5, new List<string> { "Франкенштейн", "Последний человек", "Матильда", "Валперга", "Фалькнер", "Забытая роман", "Жизнь Мэри Вульстонкрафт Шелли", "Путешествие по морям", "Возрождение", "Перси Биши Шелли" } },
    //            { 6, new List<string> { "Гордость и предубеждение", "Чувство и чувствительность", "Эмма", "Нортенгерское аббатство", "Леди Сьюзен", "Вотон", "Отрывки", "Семья Дарси", "Персуазия", "Красавица и чудовище" } },
    //            { 7, new List<string> { "Оливер Твист", "Большие надежды", "Карета миссис Харрис", "Дэвид Копперфилд", "Барнаби Радж", "Холодный дом", "Колокол-узник", "Крошка Доррит", "Сказка двух городов", "Наш общий друг" } },
    //            { 8, new List<string> { "1984", "Скотский хутор", "Дни в Бирме", "Восхождение на восток", "Дорога к Виган-Пире", "Политика и английский язык", "Животные и люди", "Революция Октябрь", "Золотой век", "Война без войны" } },
    //            { 9, new List<string> { "О дивный новый мир", "Остров", "Обезьяны и сущность", "Контропункт", "Двое или трое вместе", "Желтый хромосома", "Музыка в ночи", "После много летних дождей", "Безумие во благо", "Летаргический сон" } },
    //            { 10, new List<string> { "Процесс", "Замок", "Америка", "Превращение", "Голод артиста", "Приговор", "В исправительной колонии", "Описание одной борьбы", "Большой журнал", "Охотник Грациус" } },
    //            { 11, new List<string> { "Волшебная гора", "Будденброки", "Доктор Фаустус", "Смерть в Венеции", "Иосиф и его братья", "Марио и чародей", "Избранные рассказы", "Королевское величество", "Молодой Иосиф", "Последние годы" } },
    //            { 12, new List<string> { "В поисках утраченного времени", "По направлению Свана", "В тени девушек в цвету", "Плаж Гернманта", "Пленница", "Беглянка", "Найденное время", "Время регенерации", "Мадлен воспоминаний", "Запахи и вкусы" } },
    //            { 13, new List<string> { "Улисс", "Портрет художника в молодости", "Поминки по Финнегану", "Дублинцы", "Изгнанник", "Люсинда", "Стефан Герой", "Грань вечности", "Писем Джойса", "Неопубликованные работы" } },
    //            { 14, new List<string> { "Шум и ярость", "Свет в августе", "Авессалом, Авессалом!", "Пока я умирал", "Святилище", "Отпущенные рабы", "Деньги", "Деревня", "Взлет жесткий", "Речная земля" } },
    //            { 15, new List<string> { "Старик и море", "По ком звонит колокол", "Прощай, оружие", "Иметь и не иметь", "Снеги Килиманджаро", "Острова в потоке", "Триумф вопреки", "Путешествие опасное", "Франция в войне", "Портрет художника" } },
    //            { 16, new List<string> { "Защита Лужина", "Приглашение на казнь", "Отчаяние", "Король, дама, валет", "Камера темная", "Бледный огонь", "Ада или радость", "Машина будущего", "Огонь синего", "Русские красавицы" } },
    //            { 17, new List<string> { "В ожидании Годо", "Конец игры", "Счастливые дни", "Молл Мэлоун умирает", "Неназванный", "Как есть", "Игра", "Пьесы", "Проза", "Стихотворения" } },
    //            { 18, new List<string> { "Вавилонская библиотека", "Сад расходящихся тропок", "Том Кэротт", "Бартельмес и компания", "Иммортель", "История вечности", "Формы времени", "Лотерея в Вавилоне", "Зеркало и маска", "Часовая башня" } },
    //            { 19, new List<string> { "Бытие і нічого", "Ось і все", "Екзистенціалізм це гуманізм", "Мухи", "На брудних руках", "Диявол і добрий Бог", "Свобода як втеча", "Відповідальність", "Проект Фауста", "Картезіанська думка" } }
    //        };

    //        for (int i = 0; i < authors.Count; i++)
    //        {
    //            var authorBooks = bookTitles[i];
    //            for (int j = 0; j < 10; j++)
    //            {
    //                books.Add(new BookEntity
    //                {
    //                    Title = authorBooks[j],
    //                    Description = $"Класична праця від {authors[i].Name}. Книга {j + 1} з творчого доробку автора.",
    //                    Pages = random.Next(150, 600),
    //                    Rating = (float)Math.Round(random.NextDouble() * 5, 1),
    //                    PublishYear = random.Next(1800, 2024),
    //                    AuthorId = authors[i].Id
    //                });
    //            }
    //        }

    //        return books;
    //    }

    //    private static List<BooksGenreEntity> GenerateBooksGenres(List<BookEntity> books, List<GenreEntity> genres)
    //    {
    //        var booksGenres = new List<BooksGenreEntity>();
    //        var random = new Random(42);

    //        foreach (var book in books)
    //        {
    //            var selectedGenres = genres.OrderBy(x => random.Next()).Take(2).ToList();
                
    //            foreach (var genre in selectedGenres)
    //            {
    //                booksGenres.Add(new BooksGenreEntity
    //                {
    //                    BookId = book.Id,
    //                    GenreId = genre.Id
    //                });
    //            }
    //        }

    //        return booksGenres;
    //    }
    //}
}
