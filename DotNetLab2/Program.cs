
public static class Extensions
{
    public static string Reverse(this string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static int CountOccurrences(this string input, char target)
    {
        return input.Count(c => c == target);
    }

    public static int CountOccurrences<T>(this T[] array, T target)
    {
        int count = 0;
        foreach (var element in array)
        {
            if (EqualityComparer<T>.Default.Equals(element, target))
            {
                count++;
            }
        }
        return count;
    }

    public static T[] RemoveDuplicates<T>(this T[] array)
    {
        var uniqueElements = new List<T>();
        foreach (var element in array)
        {
            if (!uniqueElements.Contains(element))
            {
                uniqueElements.Add(element);
            }
        }
        return uniqueElements.ToArray();
    }
}

public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; set; }
    public U Value1 { get; set; }
    public V Value2 { get; set; }
}

public class ExtendedDictionary<T, U, V>
{
    private List<ExtendedDictionaryElement<T, U, V>> elements = new List<ExtendedDictionaryElement<T, U, V>>();

    public void AddElement(T key, U value1, V value2)
    {
        elements.Add(new ExtendedDictionaryElement<T, U, V> { Key = key, Value1 = value1, Value2 = value2 });
    }

    public void RemoveElement(T key)
    {
        var itemToRemove = elements.FirstOrDefault(t => t.Key.Equals(key));
        if (itemToRemove != null)
        {
            elements.Remove(itemToRemove);
        }
    }

    public bool ContainsKey(T key)
    {
        return elements.Any(t => t.Key.Equals(key));
    }

    public bool ContainsValues(U value1, V value2)
    {
        return elements.Any(t => EqualityComparer<U>.Default.Equals(t.Value1, value1) && EqualityComparer<V>.Default.Equals(t.Value2, value2));
    }

    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get { return elements.FirstOrDefault(t => t.Key.Equals(key)); }
    }

    public int Count
    {
        get { return elements.Count; }
    }

    public IEnumerable<ExtendedDictionaryElement<T, U, V>> Elements
    {
        get { return elements; }
    }
}

class Program
{
        static void Main(string[] args)
        {
            // Реалізація розширених методів для класу String та одновимірних масивів

            string sampleString = "Fomenko Viacheslav";
            DisplayOriginalAndReversedString(sampleString);
            CountAndDisplayOccurrences(sampleString, 'a');

            int[] numbers = { 1, 2, 3, 4, 2, 5, 6, 1, 3 };
            DisplayOriginalAndUniqueNumbers(numbers, 3);

            // Реалізація узагальненого класу ExtendedDictionary

            ExtendedDictionary<string, int, double> dictionary = new ExtendedDictionary<string, int, double>();
            dictionary.AddElement("four", 4, 4.4);
            dictionary.AddElement("five", 5, 5.5);
            dictionary.AddElement("six", 6, 6.6);


        DisplayExtendedDictionaryElements(dictionary);

            string searchKey = "six";
            DisplaySearchResult(dictionary, searchKey);
        }

        static void DisplayOriginalAndReversedString(string input)
        {
            Console.WriteLine("default " + input);
            Console.WriteLine("Reverse() " + input.Reverse());
        }

        static void CountAndDisplayOccurrences(string input, char target)
        {
            Console.WriteLine("Пiдстрка '{0}' кiлькiсть: {1}", target, input.CountOccurrences(target));
        }

        static void DisplayOriginalAndUniqueNumbers(int[] numbers, int target)
        {
            Console.WriteLine("numbers = " + string.Join(", ", numbers));
            Console.WriteLine("Число {0}' кiлькiсть: {1}", target, numbers.CountOccurrences(target));

            numbers = numbers.RemoveDuplicates();
            Console.WriteLine("RemoveDuplicates() - видалення дублiкатiв з numbers " + string.Join(", ", numbers));
        }

        static void DisplayExtendedDictionaryElements(ExtendedDictionary<string, int, double> dictionary)
        {
            Console.WriteLine("\nЕлементи розширеного словника:");
            foreach (var element in dictionary.Elements)
            {
                Console.WriteLine($"Ключ: {element.Key}, Значення 1: {element.Value1}, Значення2: {element.Value2}");
            }
        }

        static void DisplaySearchResult(ExtendedDictionary<string, int, double> dictionary, string searchKey)
        {
            if (dictionary.ContainsKey(searchKey))
            {
                var result = dictionary[searchKey];
                Console.WriteLine($"Елемент ключа '{searchKey}' Знайти значення1 - {result.Value1}, значення2 - {result.Value2}");
            }
            else
            {
                Console.WriteLine($"Елемент ключа '{searchKey}' не знайдений.");
            }
        }
    }
