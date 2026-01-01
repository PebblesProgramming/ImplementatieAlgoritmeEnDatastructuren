Deze README vergelijkt verschillende ADT's(Advanced Data Types) en algoritmes en probeert daar een conclusie uit te trekken. Dit is voor het vak ASD op de HBO-ict opleiding op de HAN in deeltijd 2026
Zoals een **DynamicArray** en een **SinglyLinkedList** op basis van **time complexity**, **praktische performance**, en mogelijke **verbeteringen**.

---



# Analyse: DynamicArray vs SinglyLinkedList

## 1.Time Complexity

### Overzicht per operatie

| Operatie            | DynamicArray (Best / Worst) | SinglyLinkedList (Best / Worst)     |
| ------------------- | --------------------------- | ----------------------------------- |
| Toevoegen aan begin | `O(n)` / `O(n)` (schuiven)  | `O(1)` / `O(1)` (pointer verzetten) |
| Toevoegen aan eind  | `O(1)` / `O(n)` (resize)    | `O(n)` / `O(n)` (geen tail pointer) |
| Toegang via index   | `O(1)` / `O(1)`             | `O(1)` / `O(n)` (traverseren)       |
| Verwijderen         | `O(1)` / `O(n)`             | `O(1)` / `O(n)`                     |

---

### Toelichting Best / Worst Case

#### DynamicArray

* **Best case (`O(1)`)** bij toevoegen aan het eind wanneer er nog vrije capaciteit is.
* **Worst case (`O(n)`)** wanneer de interne array vol is en een `Resize()` nodig is, waarbij alle elementen worden gekopieerd.
* Toevoegen aan het begin is altijd `O(n)` vanwege het verschuiven van bestaande elementen.

#### SinglyLinkedList

* **Toevoegen aan het begin** is altijd `O(1)` omdat alleen de `head`-pointer wordt aangepast.
* **Toevoegen aan het eind** is zonder `tail`-pointer altijd `O(n)` omdat de hele lijst doorlopen moet worden.
* Toegang via index vereist traverseren en is daarom `O(n)` in het worst case scenario.

---

## 2.Execution Time (Performance Test)

Bij een performance test met **N = 50.000 items** kwamen de volgende resultaten naar voren:

* `LinkedList.AddFirst`
  ➜ **< 10 ms**
* `DynamicArray.Insert(0)`
  ➜ **> 500 ms**

### Conclusie

De `LinkedList` is aanzienlijk sneller bij toevoegingen aan het begin.
Dit komt doordat een `DynamicArray` bij elke insert op index `0` **alle bestaande elementen één positie moet verschuiven in het geheugen**.

---

## 3.Verbetervoorstellen

### DynamicArray

* **Implementeer een `Shrink()` methode**

  * Halveer de capaciteit wanneer de array minder dan **25% gevuld** is.
  * Bespaart geheugen bij langdurig verwijderen van elementen.

### SinglyLinkedList

* **Voeg een `_tail` pointer toe**

  * Maakt `AddLast` een **`O(1)` operatie** in plaats van `O(n)`.

### Algemeen

* **Generics & Iteratie**

  * Implementeer `IEnumerator<T>` zodat beide datastructuren gebruikt kunnen worden in een `foreach` loop.
