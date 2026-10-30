# Проверки

Сначала соберите основное решение Lab1_AIS.sln.
В терминале из папки проекта:
dotnet run --project Tests/Tests.csproj -- EF
dotnet run --project Tests/Tests.csproj -- Dapper

Используется отдельная база Lab_AIS_Test_20261030.
Товары основной базы GnomeMarket не изменяются.
Проверяются парсер цены, CRUD, правила модели и обработчики формы.
Создаются снимки формы EF-form.png и Dapper-form.png.
