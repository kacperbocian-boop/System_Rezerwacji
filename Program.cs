namespace System_rezerwacji_miejsc;

// punkt wejscia aplikacji
internal static class Program
{
    [STAThread]
    static void Main()
    {
        // inicjalizacja konfiguracji winforms
        ApplicationConfiguration.Initialize();
        // uruchamiamy glowny formularz
        Application.Run(new Form1());
    }
}
