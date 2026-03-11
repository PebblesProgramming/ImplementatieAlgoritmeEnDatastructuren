# Algoritmen en Datastructuren — ASD (HAN HBO-ICT deeltijd 2026)

Deze README onderbouwt de implementaties van de datastructuren en algoritmen in dit project. Per onderdeel wordt de werking, performance (time complexity en execution time) en mogelijke verbeteringen besproken.

---

## Overzicht beoordelingscriteria

| Onderdeel | Criterium | Behandeld in |
|---|---|---|
| Lijsten | Vergelijking time complexity + execution time best/worst case | [Lijsten → Vergelijking](#vergelijking-time-complexity) |
| Lijsten | Verbetervoorstel | [Lijsten → Verbetervoorstel](#verbetervoorstel) |
| Priority Queue | Implementatie uitleg + keuze onderbouwing | [Priority Queue → Hoe de priority werkt](#hoe-de-priority-werkt-en-waarom-deze-keuze) |
| Priority Queue | Performance best/worst case | [Priority Queue → Performance](#performance--bestworst-case-1) |
| Priority Queue | Verbetervoorstel | [Priority Queue → Verbetervoorstel](#verbetervoorstel-1) |
| Sorteeralgoritmes | Vergelijking time + space + execution time best/worst case | [Sorteeralgoritmes → Vergelijking](#vergelijking-time-complexity-space-complexity-en-execution-time) |
| Sorteeralgoritmes | Verbetervoorstel | [Sorteeralgoritmes → Verbetervoorstel](#verbetervoorstel-2) |
| Hash Table | Hashing-algoritme uitleg + keuze onderbouwing | [Hash Table → Hoe het werkt](#hoe-het-hashing-algoritme-werkt) |
| Hash Table | Verbetervoorstel | [Hash Table → Verbetervoorstel](#verbetervoorstel-3) |
| Binary Search | Performance best/worst case | [Binary Search → Performance](#performance--bestworst-case-3) |
| Binary Search | Verbetervoorstel | [Binary Search → Verbetervoorstel](#verbetervoorstel-4) |
| Dijkstra | Performance graaf-implementatie | [Dijkstra → Graaf performance](#performance-van-de-graaf-implementatie) |
| Dijkstra | Performance Dijkstra-algoritme best/worst case | [Dijkstra → Dijkstra performance](#performance-van-dijkstras-algoritme) |
| Dijkstra | Verbetervoorstel | [Dijkstra → Verbetervoorstel](#verbetervoorstel-5) |

---

## Lijsten

Geïmplementeerd: `DynamicArray<T>` en `SinglyLinkedList<T>`.

### Vergelijking time complexity

| Operatie           | DynamicArray          | SinglyLinkedList      |
|--------------------|-----------------------|-----------------------|
| Lezen op index     | O(1)                  | O(n)                  |
| Toevoegen vooraan  | O(n)                  | O(1)                  |
| Toevoegen achteraan| O(1) amortized        | O(n)                  |
| Verwijderen        | O(n)                  | O(n)                  |

**DynamicArray — best/worst case:**
- `Add` (achteraan): best case O(1) — er is ruimte in de array, het element wordt direct op positie `_count` geplaatst. Worst case O(n) — de array zit vol en alle elementen moeten worden gekopieerd naar een nieuw array van dubbele grootte. Over veel toevoegingen gemiddeld is dit O(1) amortized omdat het kopiëren steeds minder vaak voorkomt.
- `Insert` of `RemoveAt`: altijd O(n) — alle elementen na de gekozen index moeten verschuiven.
- `this[index]` (lezen/schrijven): altijd O(1) — het geheugenadres is direct te berekenen: `startadres + (index × elementgrootte)`.

**SinglyLinkedList — best/worst case:**
- `AddFirst`: altijd O(1) — alleen de head-pointer wordt aangepast, geen iteratie.
- `AddLast`: altijd O(n) — er is geen tail-pointer, dus de lijst moet volledig doorlopen worden om het einde te vinden.
- `Remove`: best case O(1) als het het eerste element is. Worst case O(n) als het element helemaal achteraan staat of niet aanwezig is.

### Execution time

De test `Performance_Comparison_AddAtStart` in `ListTests.cs` demonstreert het verschil concreet: bij 50.000 keer vooraan invoegen is de `SinglyLinkedList` significant sneller dan de `DynamicArray`. Dit is te verwachten: `AddFirst` op de linked list is O(1), terwijl `Insert(0, ...)` op de dynamic array O(n) is — elk element moet één positie opschuiven. Over 50.000 operaties resulteert dat in O(n²) totaal werk voor de array.

### Verbetervoorstel

`AddLast` in de `SinglyLinkedList` is O(n) omdat de implementatie geen tail-pointer bijhoudt. Door een `_tail` veld toe te voegen dat altijd naar het laatste knooppunt wijst, wordt `AddLast` O(1). Dit heeft directe impact omdat `AddLast` ook intern gebruikt wordt in andere datastructuren (zoals queues).

---

## Priority Queue

Geïmplementeerd: `PriorityQueue<TElement, TPriority>` in `PriorityQueue.cs`.

### Hoe de priority werkt en waarom deze keuze

De priority queue is geïmplementeerd met een **ongesorteerde lijst** (`List<(TElement, TPriority)>`). Bij `Enqueue` wordt het element direct achteraan de lijst toegevoegd zonder enige sortering. Bij `Dequeue` wordt de volledige lijst één keer doorlopen om het element met de laagste prioriteitswaarde te vinden, dat element wordt teruggegeven en verwijderd.

De keuze voor deze aanpak is bewust: de implementatie is eenvoudig en transparant. Bovendien past het goed bij Dijkstra's algoritme, waarbij correctheid van de prioriteitsvolgorde belangrijker is dan de snelheid van enqueue. De logica (laagste getal = hoogste prioriteit) is direct zichtbaar in `FindMinIndex`.

### Performance — best/worst case

| Operatie  | Time complexity | Toelichting                                              |
|-----------|-----------------|----------------------------------------------------------|
| Enqueue   | O(1)            | Element wordt direct achteraan toegevoegd                |
| Dequeue   | O(n)            | Hele lijst doorlopen om minimum te vinden                |
| Peek      | O(n)            | Zelfde als Dequeue maar zonder verwijdering              |

**Best case Dequeue:** O(n) — ook als het element met de laagste prioriteit toevallig vooraan staat, moet `FindMinIndex` de hele lijst doorlopen om te bevestigen dat er geen lager element achteraan zit.

**Worst case Dequeue:** O(n) — het element met de laagste prioriteit staat achteraan. Er is geen verschil met best case in termen van complexiteit; de constante factor kan wel iets hoger liggen.

De test `ManyItems_ShouldComeOutByPriority` in `PriorityQueueTests.cs` bewijst dat 1000 elementen in de juiste volgorde uit de queue komen.

### Verbetervoorstel

De bottleneck is `Dequeue` met O(n). Dit kan verbeterd worden door de ongesorteerde lijst te vervangen door een **binary min-heap**. Een binary min-heap is een binaire boom waarbij de ouder altijd een lagere of gelijke prioriteit heeft dan zijn kinderen. Bij `Enqueue` wordt het nieuwe element achteraan de heap geplaatst en "omhoog" gezet (bubble up) totdat de heap-eigenschap hersteld is: O(log n). Bij `Dequeue` wordt de root (het minimum) verwijderd, het laatste element naar de root verplaatst, en "omlaag" gezet (bubble down): O(log n). Dit is een significante verbetering voor grote datasets.

---

## Sorteeralgoritmes

Geïmplementeerd: `InsertionSorter<T>` en `MergeSorter<T>`.

### Vergelijking time complexity, space complexity en execution time

| Eigenschap        | Insertion Sort      | Merge Sort          |
|-------------------|---------------------|---------------------|
| Best case time    | O(n)                | O(n log n)          |
| Worst case time   | O(n²)               | O(n log n)          |
| Space complexity  | O(1)                | O(n)                |

**Insertion Sort — best/worst case:**
- Best case O(n): de array is al gesorteerd. De binnenste while-lus wordt nooit uitgevoerd; er is alleen één vergelijking per element.
- Worst case O(n²): de array is omgekeerd gesorteerd. Voor elk element moeten alle voorgaande elementen één positie opschuiven. Bij n = 20.000 zijn dat ca. 200.000.000 operaties.
- Space complexity O(1): er wordt geen extra array aangemaakt. De variabele `key` is de enige extra ruimte.

**Merge Sort — best/worst case:**
- Best én worst case zijn beide O(n log n): de array wordt altijd in twee helften gesplitst (log n niveaus), en op elk niveau worden alle n elementen verwerkt in de merge-stap. De volgorde van de input maakt geen verschil.
- Space complexity O(n): de hulparray `aux` in `MergeSorter.cs:16` heeft dezelfde grootte als de input. Dit is extra geheugengebruik dat Insertion Sort niet heeft.

**Execution time:**
De test `Compare_Sorting_Performance` in `SortingTests.cs` sorteert n = 20.000 willekeurige integers. Merge Sort is significant sneller dan Insertion Sort op deze grootte, wat overeenkomt met de verwachting: O(n log n) vs O(n²).

### Verbetervoorstel

De hulparray `aux` in `MergeSorter.cs:16` wordt één keer aangemaakt bij de aanroep van `Sort` en doorgegeven aan alle recursieve aanroepen. Dit is al een verbetering ten opzichte van elke merge-stap een nieuwe array aanmaken. Een verdere verbetering is **bottom-up merge sort**: in plaats van recursief splitsen begin je met subarrays van grootte 1 en verdubbel je de grootte iteratief (1, 2, 4, 8, ...). Dit elimineert de recursie-overhead (geen call stack) maar behoudt dezelfde O(n log n) time complexity en O(n) space complexity. De `aux`-array zelf weggooien vereist een volledig andere aanpak (in-place merge) die O(n log n) space terugbrengt naar O(1) maar de implementatie aanzienlijk complexer maakt.

---

## Hash Table

Geïmplementeerd: `HashTable<K, V>` in `HashTable.cs`.

### Hoe het hashing-algoritme werkt

De hash-functie in `GetHash` werkt in twee stappen:

1. `key.GetHashCode()` — roept de ingebouwde .NET methode aan die voor elk type een integer teruggeeft. Voor strings is dit gebaseerd op de inhoud van de string; voor integers is de waarde zelf de hash.
2. `Math.Abs(hash % _size)` — de modulo-bewerking mapt de integer naar een geldige index in de interne array (0 tot `_size - 1`). `Math.Abs` zorgt dat negatieve hashcodes geen negatieve index opleveren.

Collisions — twee verschillende keys die dezelfde index krijgen — worden opgelost met **separate chaining**: elke positie in de array bevat een `LinkedList<HashEntry<K, V>>`. Bij een collision worden beide entries in dezelfde lijst opgeslagen. Bij `Get` en `Put` wordt de lijst van die bucket lineair doorzocht op de juiste key.

De keuze voor `GetHashCode` is pragmatisch: het is de standaard .NET aanpak die goed werkt voor alle types die `Equals` correct implementeren, en sluit aan bij hoe C# intern ook werkt (o.a. `Dictionary<K, V>`).

### Performance — best/worst case

| Operatie | Best case | Worst case |
|----------|-----------|------------|
| Put      | O(1)      | O(n)       |
| Get      | O(1)      | O(n)       |

**Best case O(1):** de hash-functie verdeelt keys gelijkmatig over de buckets. Elke bucket heeft maximaal één entry; de lookup is direct.

**Worst case O(n):** alle keys mappen naar dezelfde bucket (dit is te reproduceren door `size = 1` te gebruiken, zoals in de test `Collision_ShouldStillStoreBothItems`). De hele lijst moet lineair doorlopen worden.

### Verbetervoorstel

De huidige implementatie heeft een vaste `_size` en houdt geen **load factor** bij. Als er veel entries worden toegevoegd ten opzichte van de tabelgrootte, worden de buckets langer en verslechtert de gemiddelde performance richting O(n). De verbetering is een **dynamische resize**: bijhoud hoeveel entries er zijn, bereken de load factor (`Count / _size`), en als die boven een drempel (bijv. 0.75) komt, maak dan een nieuwe tabel van dubbele grootte aan en her-hash alle entries. Dit houdt de gemiddelde ketenlengte constant, waardoor `Get` en `Put` amortized O(1) blijven — ongeacht hoeveel entries er worden toegevoegd.

---

## Binary Search

Geïmplementeerd: `BinarySearch<T>` in `BinarySearch.cs`.

### Hoe het werkt

Binary search vereist een **gesorteerde array**. Het algoritme houdt twee grenzen bij: `low` (begin) en `high` (eind). In elke iteratie wordt het middelste element `mid` berekend en vergeleken met het target:
- Als `array[mid] == target`: gevonden, return `mid`.
- Als `array[mid] < target`: het target zit in de rechterhelft → `low = mid + 1`.
- Als `array[mid] > target`: het target zit in de linkerhelft → `high = mid - 1`.

Na elke stap is het zoekgebied gehalveerd. Dit gaat door totdat het element gevonden is of `low > high` (niet gevonden, return -1).

### Performance — best/worst case

| Case       | Time complexity | Toelichting                                                        |
|------------|-----------------|--------------------------------------------------------------------|
| Best case  | O(1)            | Het target staat precies op de middelste positie bij de eerste stap|
| Worst case | O(log n)        | Het target staat helemaal links, rechts, of is niet aanwezig       |

Bij elke stap wordt het zoekgebied gehalveerd. Na k stappen is het zoekgebied nog n/2^k groot. Het algoritme stopt wanneer dat 1 is, dus bij k = log₂(n) stappen. Voor n = 1.000.000 zijn dat maximaal 20 vergelijkingen.

**Execution time:**
De test `Compare_Search_Performance` in `BinarySearchTests.cs` vergelijkt binary search met lineair zoeken op 1.000.000 elementen, waarbij het target het allerlaatste element is (worst case voor lineair zoeken). Binary search heeft hier een drastisch lagere executietijd, gemeten in ticks omdat milliseconden te grof zijn voor de snelheid van binary search.

### Verbetervoorstel

**Beperking:** binary search vereist dat de array gesorteerd is. Als de array niet gesorteerd is, moet hij eerst gesorteerd worden (minimaal O(n log n)), wat het voordeel tenietdoet voor eenmalige zoekopdrachten.

**Interpolation search** is een verbetering voor arrays met **uniform verdeelde waarden**. In plaats van altijd het midden te nemen, schat het algoritme de positie van het target op basis van de waarde: `pos = low + ((target - array[low]) * (high - low)) / (array[high] - array[low])`. Bij uniforme verdeling leidt dit tot O(log log n) gemiddelde complexiteit in plaats van O(log n). Bij niet-uniforme verdeling kan de worst case O(n) zijn, dus deze verbetering is alleen zinvol als de gegevensstructuur bekend is.

---

## Dijkstra's Algoritme

Geïmplementeerd: `Graph`, `Node`, `Edge` en `Dijkstra` in de `Graph`-map.

### Performance van de graaf-implementatie

De graaf gebruikt een **adjacency list**: elke `Node` heeft een lijst `Neighbors` van `Edge`-objecten. Dit is een bewuste keuze ten opzichte van een adjacency matrix.

| Operatie    | Adjacency List   | Adjacency Matrix |
|-------------|------------------|------------------|
| AddNode     | O(1)             | O(V²) (resize)   |
| AddEdge     | O(1)             | O(1)             |
| GetNode     | O(V)             | O(1)             |
| Ruimte      | O(V + E)         | O(V²)            |

`AddNode` en `AddEdge` zijn O(1): een node wordt achteraan een `List<Node>` toegevoegd, een edge achteraan de `Neighbors`-lijst van de bronnode.

`GetNode` is O(V): de lijst `_nodes` wordt lineair doorzocht op naam. Voor Dijkstra zelf is dit niet kritisch omdat nodes direct als objectreferenties worden meegegeven.

De **ruimte-efficiency** is het grootste voordeel van de adjacency list: O(V + E). Een adjacency matrix gebruikt O(V²) ruimte, wat bij dunne grafen (weinig edges) veel verspilling is.

### Performance van Dijkstra's algoritme

Dijkstra's algoritme berekent de kortste paden vanuit een startknoop naar alle andere knopen. De implementatie gebruikt de `PriorityQueue<Node, double>` uit dit project, die intern een **ongesorteerde lijst** gebruikt.

**Stappen:**
1. Start met `start.Distance = 0`, alle andere nodes op `double.MaxValue`.
2. Haal steeds de node met de laagste afstand uit de priority queue (`Dequeue`).
3. Markeer de node als bezocht. Verwerk alle buren: als een kortere route gevonden wordt, update de afstand en voeg de buur opnieuw toe aan de queue.

**Time complexity met ongesorteerde priority queue:**

| Stap                         | Kosten per stap | Totaal         |
|------------------------------|-----------------|----------------|
| Dequeue (min zoeken)         | O(V)            | O(V²)          |
| Enqueue (toevoegen)          | O(1)            | O(E)           |
| **Totaal**                   |                 | **O(V² + E)**  |

Omdat E ≤ V², is de totale complexiteit **O(V²)**.

**Best case:** een dunne graaf met weinig edges. De priority queue blijft klein, maar `Dequeue` kost nog steeds O(V) omdat de ongesorteerde lijst volledig gescand moet worden.

**Worst case:** een dichte graaf waarbij elke node verbonden is met alle andere nodes (E = V²). Dan worden er veel herhaalde enqueues gedaan (omdat er geen decrease-key operatie is), wat de queue groter maakt en de kosten per `Dequeue` verhoogt.

De test `Dijkstra_ShouldFindShortestPath` in `DijkstraTests.cs` bewijst correcte werking: A→D via C (afstand 5) wordt correct gevonden boven A→B→D (afstand 20).

### Verbetervoorstel

De bottleneck is `Dequeue` in de priority queue: O(V) per aanroep, waardoor Dijkstra uitkomt op O(V²). Door de ongesorteerde lijst te vervangen door een **binary min-heap** wordt `Dequeue` O(log V) en daalt de totale complexiteit naar **O((V + E) log V)**. Voor dunne grafen (E ≈ V) is dit een grote verbetering. Voor dichte grafen (E ≈ V²) is het verschil kleiner.

Een tweede verbeterpunt is het ontbreken van een **decrease-key** operatie. Nu wordt een node opnieuw in de queue gezet met een nieuwe prioriteit als een kortere route gevonden wordt, maar de oude entry blijft ook in de queue. Dit wordt afgevangen door de `visited`-check, maar het vergroot de queue onnodig. Met decrease-key zou de bestaande entry direct bijgewerkt worden, wat de queue kleiner houdt.
