# Magic-cube
creates a magic cube according to the pattern

Wypełnij kostkę sześcienną o rozmiarze n * n * n tekstem T (podanym przez użytkownika) w następujący sposób.
Sześcian składa się z n tablic o rozmiarze n * n, ustawionych jedna za drugą. Każda tablica stanowi magiczny
kwadrat wypełniony (w odpowiedni sposób) literami tekstu T. Tekst T jest powielany, tzn. dla T = ABCDEFGH!
generowany jest ciąg:


T = ABCDEFGH!ABCDEFGH!ABCDEFGH!...,


którego kolejne znaki są wykorzystywane w nieprzerwany sposób do wypełniania wszystkich (od 0 do (n–1)-szej) tablic sześcianu. 
Tablice o numerach parzystych (dla i = 0, 2, 4, 6, ...) uzupełniane są literami ciągu T
zgodnie z podanym w przykładzie poniżej wzorcem (tj. po każdorazowym rekurencyjnym wywołaniu funkcji
wypełniającej ramkę, elementy ciągu T powinny być umieszczone na obwodzie kwadratu w następującym
porządku: górna krawędź (od lewej do prawej), prawa krawędź (od góry do dołu), dolna krawędź (od prawej
do lewej), lewa krawędź (od dołu do góry)). Tablice o numerach nieparzystych (tj. dla i = 1, 3, 5, ...) są
wypełniane przy wykorzystaniu tej samej drogi, ale kolejne litery ciągu umieszczane są na niej w odwrotnej
kolejności, tj. zaczynamy wypełnianie kwadratu od jego środka (patrz przykład poniżej). Wszystkie kwadraty
należy wypełnić literami w taki sposób, aby ciąg T nie został przerwany w żadnym miejscu (tj. jeśli generując
tablicę 0, zakończyliśmy na znaku G, to tablicę 1 rozpoczynamy zapełniać od znaku H oraz litery H i G muszą
sąsiadować ze sobą w sześcianie).

Program made: 02/11/2022
