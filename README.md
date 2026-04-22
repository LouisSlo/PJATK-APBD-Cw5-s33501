# Training Center API 🏢

Proste REST API stworzone w ASP.NET Core do zarządzania salami dydaktycznymi i ich rezerwacjami. Projekt jest wynikiem ćwiczeń akademickich – korzysta z danych przechowywanych w pamięci (in-memory), bez podłączenia do zewnętrznej bazy danych SQL.

## Technologie
* C#
* .NET 10
* ASP.NET Core Web API
* Swagger (do dokumentacji i testów)

## Jak uruchomić i przetestować?
1. Otwórz projekt w swoim IDE (np. JetBrains Rider).
2. Uruchom aplikację (przycisk **Run**).
3. Aby przetestować API w przeglądarce, przejdź pod adres wygenerowany w konsoli z dopiskiem `/swagger` (np. `http://localhost:5207/swagger`).
4. Możesz również użyć programu **Postman**, wysyłając żądania GET, POST, PUT, DELETE bezpośrednio na endpointy takie jak `/api/rooms` czy `/api/reservations`.

## Główne funkcje
* **Sale (Rooms):** Pobieranie, dodawanie, aktualizacja i usuwanie sal. Możliwość filtrowania (np. po pojemności czy dostępności projektora).
* **Rezerwacje (Reservations):** Zarządzanie rezerwacjami z podstawową logiką biznesową (np. walidacja dat, blokowanie nakładających się na siebie rezerwacji).
